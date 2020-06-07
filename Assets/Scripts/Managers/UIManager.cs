using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{
    [Header("Objetos")]
    [SerializeField]
    GameObject settings = null, Pause = null, initialButtons = null, back = null, audioPanel = null, Background = null, pulse = null, daga = null, pistola = null, interfaz = null;
    [Header("Botones")]
    [SerializeField]
    Button Menu = null, Resume = null, save = null, audioSettings = null , settingsB = null;
    [Header("Sliders")]
    [SerializeField]
    private Slider fxSlider = null, musicSlider = null;
    [Header("UI")]
    [SerializeField]
    private Image Pistola = null, Daga = null;
    [Header("Toggles")]
    [SerializeField]
    private Toggle cheat = null, controller = null;
    [SerializeField]
    Text Ammunition = null;
    [SerializeField]
    EventSystem eventSystem = null;

    StandaloneInputModule inputModule;
    private GameObject lastSelected = null;

    private bool ini = false;
    private void Start()
    {
        if (GameManager.gmInstance_ != null)
        {
            GameManager.gmInstance_.SetUImanager(this);
            Menu.onClick.AddListener(delegate { GameManager.gmInstance_.ChangeScene("Menu"); });
            Resume.onClick.AddListener(delegate { GameManager.gmInstance_.ResumeGame(); });
        }
        else Debug.LogError("Warnign GameManager was null when trying to access it from " + this);

        if (SettingsManager.smInstance_)
        {
            fxSlider.value = SettingsManager.smInstance_.GetSettings().fxVolume_;
            musicSlider.value = SettingsManager.smInstance_.GetSettings().musicVolume_;
            fxSlider.onValueChanged.AddListener(delegate { SoundManager.smInstance_.SetFXVolume(fxSlider.value); });
            musicSlider.onValueChanged.AddListener(delegate { SoundManager.smInstance_.SetMusicVolume(musicSlider.value); });

            cheat.isOn = SettingsManager.smInstance_.GetSettings().cheats_;
            controller.isOn = SettingsManager.smInstance_.GetSettings().controller_;
            cheat.onValueChanged.AddListener(delegate { SettingsManager.smInstance_.ActivateCheats(cheat.isOn); });
            controller.onValueChanged.AddListener(delegate
            {
                SettingsManager.smInstance_.ActivateController(controller.isOn);
                SetController(controller.isOn);
                if (controller.isOn) eventSystem.SetSelectedGameObject(this.controller.gameObject);
                else eventSystem.SetSelectedGameObject(null);
            });

            save.onClick.AddListener(delegate { SettingsManager.smInstance_.Save(); });
        }
        audioSettings.onClick.AddListener(delegate { Audio(); });
        inputModule = eventSystem.GetComponent<StandaloneInputModule>();
    }

    public void RespawnMenu()
    {
        if (Pause != null)
        {
            Pause.SetActive(true);
            Resume.gameObject.SetActive(false);
            settingsB.gameObject.SetActive(false);
            Back();
            Back();
            interfaz.SetActive(false);
            if (SettingsManager.smInstance_.GetSettings().controller_) eventSystem.SetSelectedGameObject(Menu.gameObject);
        }
        else
        {
            Debug.LogError("Warning RespawnUI is not set on " + this + "and can't be shown");
        }

    }
    public void OnPause()
    {
        ini = true;
        Pause.SetActive(true);
        Back();
        Back();
        interfaz.SetActive(false);
        if (SettingsManager.smInstance_.GetSettings().controller_) eventSystem.SetSelectedGameObject(Resume.gameObject);
    }

    public void OnResume()
    {
        Pause.SetActive(false);
        interfaz.SetActive(true);
        eventSystem.SetSelectedGameObject(null);
    }

    public void SetWeapon(bool activado)
    {
        if (daga && pistola)
        {
            daga.SetActive(activado);
            pistola.SetActive(!activado);
        }
        if (Pistola && Daga)
        {
            if (activado)
            {
                Pistola.color = new Color(1, 1, 1, 0.12f);
                Daga.color = new Color(1, 1, 1, 1);
            }
            else
            {
                Daga.color = new Color(1, 1, 1, 0.12f);
                Pistola.color = new Color(1, 1, 1, 1);
            }
        }
    }

    public void ActivatePulse(bool activado)
    {
        pulse.SetActive(activado);
    }

    public void TurnInterface(bool on)
    {
        interfaz.SetActive(on);
    }

    public void SetAmmunition(int num)
    {
        Ammunition.text = "x " + num;
    }


    // MANEJO INTERNO DEL MENU DE PAUSA


    private void Update()
    {
        if (Input.GetButtonDown("Cancel") && Pause.activeSelf) Back();
        if (SettingsManager.smInstance_.GetSettings().controller_ && eventSystem.currentSelectedGameObject == null && Input.GetAxisRaw("VerticalMando") != 0 && Pause.activeSelf)
        {
            eventSystem.SetSelectedGameObject(lastSelected);
        }
        else if (lastSelected != eventSystem.currentSelectedGameObject && eventSystem.currentSelectedGameObject != null)
        {
            lastSelected = eventSystem.currentSelectedGameObject;
        }
        if (audioPanel.activeSelf && SettingsManager.smInstance_.GetSettings().controller_)
        {
            musicSlider.value += Input.GetAxis("HorizontalMando") * 0.5f;
            fxSlider.value -= Input.GetAxis("HorizontalRight") * 0.5f;
        }
    }

    public void Settings()
    {
        Clean();
        Background.SetActive(true);
        settings.SetActive(true);
        back.SetActive(true);
        if (SettingsManager.smInstance_.GetSettings().controller_) eventSystem.SetSelectedGameObject(back);
    }

    public void Back()
    {
        if (back.activeSelf)
        {
            Clean();
            Background.SetActive(true);
            if (ini)
            {
                initialButtons.SetActive(true);
                if (SettingsManager.smInstance_.GetSettings().controller_) eventSystem.SetSelectedGameObject(Resume.gameObject);
            }
            else
            {
                settings.SetActive(true);
                back.SetActive(true);
                ini = true;
            }
        }
    }

    public void Audio()
    {
        Clean();
        audioPanel.SetActive(true);
        back.SetActive(true);
        ini = false;
    }

    void Clean()
    {
        settings.SetActive(false);
        Background.SetActive(false);
        initialButtons.SetActive(false);
        back.SetActive(false);
        audioPanel.SetActive(false);
    }

    void SetController(bool controller)
    {
        if (controller)
        {
            inputModule.horizontalAxis = "HorizontalMando";
            inputModule.verticalAxis = "VerticalMando";
        }
        else
        {
            inputModule.horizontalAxis = "Horizontal";
            inputModule.verticalAxis = "Vertical";
        }
    }
}
