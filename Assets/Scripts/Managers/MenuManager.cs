using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    GameObject initialButtons = null, settings = null , howToPlay = null, backgroundGroup = null, back = null, audioPanel = null; /*exit, , audioSettings, back;*/
    [SerializeField]
    AudioMixer musicAM = null, fxSoundAM = null;
    [SerializeField]
    SoundManager soundManager;
    private bool ini;
    private float fxVolume;
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
            audioPanel.SetActive(false);
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
        settings.SetActive(false);
        backgroundGroup.SetActive(false);
    }

    public void SetMusicVolume(float volume)
    {
        if (volume == -30) volume = -80;
        musicAM.SetFloat("MusicVolume", volume);

    }
    public void SetFXVolume(float volume)
    {
        if (volume == -20) volume = -80;
        fxSoundAM.SetFloat("FXVolume", volume);
        soundManager.GetFXVolume(volume);
        fxVolume = volume;
    }
   
}
