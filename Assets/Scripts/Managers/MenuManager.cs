using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    GameObject initialButtons = null, settings = null, howToPlay = null, backgroundGroup = null, back = null, audioPanel = null, save = null, continueB = null; /*exit, , audioSettings, back;*/
    [SerializeField]
    private Slider fxSlider = null, musicSlider = null;
    private bool ini;
    private float fxVolume;
    private void Start()
    {
        ini = true;
        if (!GameManager.gmInstance_.IsGameLoaded())
        {
            continueB.GetComponent<Button>().enabled = false;
            continueB.GetComponentInChildren<Text>().color = Color.gray;
        }
        else
        {
            continueB.GetComponent<Button>().onClick.AddListener(delegate { GameManager.gmInstance_.Continue(); });
        }
        if (SoundManager.smInstance_)
        {
            fxSlider.value = SoundManager.smInstance_.soundSettings.fxVolume_;
            musicSlider.value = SoundManager.smInstance_.soundSettings.musicVolume_;

            fxSlider.onValueChanged.AddListener(delegate { SoundManager.smInstance_.SetFXVolume(fxSlider.value); });
            musicSlider.onValueChanged.AddListener(delegate { SoundManager.smInstance_.SetMusicVolume(musicSlider.value); });
        }
        save.GetComponent<Button>().onClick.AddListener(delegate { SoundManager.smInstance_.Save(); });
    }
    public void Settings()
    {
        initialButtons.SetActive(false);
        settings.SetActive(true);
        back.SetActive(true);
    }

    public void Back()
    {
        back.SetActive(false);
        howToPlay.SetActive(false);
        audioPanel.SetActive(false);
        if (ini)
        {
            settings.SetActive(false);
            initialButtons.SetActive(true);
            back.SetActive(false);
        }
        else
        {
            settings.SetActive(true);
            back.SetActive(true);
            backgroundGroup.SetActive(true);
            ini = true;
        }
    }

    public void HowToPlay()
    {
        howToPlay.SetActive(true);
        settings.SetActive(false);
        back.SetActive(true);
        backgroundGroup.SetActive(false);
        ini = false;
    }

    public void Audio()
    {
        audioPanel.SetActive(true);
        ini = false;
        save.SetActive(true);
        settings.SetActive(false);
        backgroundGroup.SetActive(false);
    }
}
