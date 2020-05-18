using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    GameObject initialButtons = null, settings = null, howToPlay = null, backgroundGroup = null, back = null, audioPanel = null; /*exit, , audioSettings, back;*/
    [SerializeField]
    private Slider fxSlider = null, musicSlider = null;
    [SerializeField]
    private Toggle cheat = null;
    [SerializeField]
    Button Exit = null, continueB = null, save = null;
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
        if (SoundManager.smInstance_)
        {
            fxSlider.value = SoundManager.smInstance_.soundSettings.fxVolume_;
            musicSlider.value = SoundManager.smInstance_.soundSettings.musicVolume_;

            fxSlider.onValueChanged.AddListener(delegate { SoundManager.smInstance_.SetFXVolume(fxSlider.value); });
            musicSlider.onValueChanged.AddListener(delegate { SoundManager.smInstance_.SetMusicVolume(musicSlider.value); });
        }
        save.onClick.AddListener(delegate { SoundManager.smInstance_.Save(); });
        cheat.onValueChanged.AddListener(delegate { GameManager.gmInstance_.ActivateCheats(cheat.isOn); });
        Exit.onClick.AddListener(delegate { GameManager.gmInstance_.ExitGame(); });
    }
    public void Settings()
    {
        Clean();
        backgroundGroup.SetActive(true);
        settings.SetActive(true);
        back.SetActive(true);
    }

    public void Back()
    {
        Clean();
        backgroundGroup.SetActive(true);
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
        backgroundGroup.SetActive(false);
        initialButtons.SetActive(false);
        back.SetActive(false);
        howToPlay.SetActive(false);
        audioPanel.SetActive(false);
    }
}
