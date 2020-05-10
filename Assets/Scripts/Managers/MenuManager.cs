using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    GameObject initialButtons, settings, howToPlay, backgroundGroup, back, audio; /*exit, , audioSettings, back;*/
    [SerializeField]
    AudioMixer musicAM, fxSoundAM;
    private bool ini;
    private void Start()
    {
        ini = true;
    }
    public void Settings()
    {
        initialButtons.SetActive(false);
        settings.SetActive(true);
        back.SetActive(true);
    }

    public void Back()
    {
        if (ini)
        {
            settings.SetActive(false);
            initialButtons.SetActive(true);
            back.SetActive(false);
        }
        else
        {
            settings.SetActive(true);
            howToPlay.SetActive(false);
            backgroundGroup.SetActive(true);
            audio.SetActive(false);
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
        audio.SetActive(true);
        ini = false;
        settings.SetActive(false);
        backgroundGroup.SetActive(false);
    }

    public void SetMusicVolume(float volume)
    {
        musicAM.SetFloat("MusicVolume", volume);
       
    }
    public void SetFXVolume (float volume)
    {
        fxSoundAM.SetFloat("FXVolume",volume);
    }
}
