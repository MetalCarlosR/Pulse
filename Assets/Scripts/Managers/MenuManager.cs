using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    GameObject initialButtons = null, settings = null, howToPlay = null, back = null, audioPanel = null, Background = null;
    [SerializeField]
    private Slider fxSlider = null, musicSlider = null;
    [SerializeField]
    private Toggle cheat = null, controller = null;
    [SerializeField]
    Button Exit = null, continueB = null, save = null, newGame = null;
    [SerializeField]
    EventSystem eventSystem = null;

    StandaloneInputModule inputModule;
    private bool ini;
    private float fxVolume;
    private GameObject lastSelected = null;
    private void Start()
    {
        ini = true;

        if (!GameManager.gmInstance_.IsGameLoaded())
        {
            continueB.enabled = false;
            continueB.GetComponentInChildren<Text>().color = Color.gray;
        }
        else
        {
            continueB.onClick.AddListener(delegate { GameManager.gmInstance_.Continue(); });
        }
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
        newGame.onClick.AddListener(delegate { GameManager.gmInstance_.ChangeScene("Nivel 1"); });
        Exit.onClick.AddListener(delegate { GameManager.gmInstance_.ExitGame(); });

        inputModule = eventSystem.GetComponent<StandaloneInputModule>();
        SetController(controller.isOn);
        if (!controller.isOn) eventSystem.SetSelectedGameObject(null);
        else eventSystem.SetSelectedGameObject(newGame.gameObject);
    }

    void Update()
    {
        if (Input.GetButtonDown("Cancel")) Back();
        if (SettingsManager.smInstance_.GetSettings().controller_ && eventSystem.currentSelectedGameObject == null && Input.GetAxisRaw("VerticalMando") != 0)
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
                if (SettingsManager.smInstance_.GetSettings().controller_) eventSystem.SetSelectedGameObject(newGame.gameObject);
            }
            else
            {
                settings.SetActive(true);
                back.SetActive(true);
                ini = true;
            }
        }
    }

    public void HowToPlay()
    {
        Clean();
        howToPlay.SetActive(true);
        back.SetActive(true);
        ini = true;
    }

    public void Audio()
    {
        Clean();
        audioPanel.SetActive(true);
        ini = false;
        back.SetActive(true);
    }

    void Clean()
    {
        settings.SetActive(false);
        Background.SetActive(false);
        initialButtons.SetActive(false);
        back.SetActive(false);
        howToPlay.SetActive(false);
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
