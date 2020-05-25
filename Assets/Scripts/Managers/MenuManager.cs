using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    GameObject initialButtons = null, settings = null, howToPlay = null, back = null, audioPanel = null, Background = null;
    [SerializeField]
    private Slider fxSlider = null, musicSlider = null;
    [SerializeField]
    private Toggle cheat = null, controller = null;
    [SerializeField]
    Button Exit = null, continueB = null, save = null , newGame = null;
    private bool ini;
    private float fxVolume;
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
            controller.onValueChanged.AddListener(delegate { SettingsManager.smInstance_.ActivateController(controller.isOn); });

            save.onClick.AddListener(delegate { SettingsManager.smInstance_.Save(); });
        }
        newGame.onClick.AddListener(delegate { GameManager.gmInstance_.ChangeScene("Nivel 1"); });
        Exit.onClick.AddListener(delegate { GameManager.gmInstance_.ExitGame(); });
    }
    public void Settings()
    {
        Clean();
        Background.SetActive(true);
        settings.SetActive(true);
        back.SetActive(true);
    }

    public void Back()
    {
        Clean();
        Background.SetActive(true);
        if (ini)
        {
            initialButtons.SetActive(true);
        }
        else
        {
            settings.SetActive(true);
            back.SetActive(true);
            ini = true;
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
}
