using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{

    public static SoundManager smInstance_;

    [SerializeField]
    private List<AudioClip> FXSoundsPool = new List<AudioClip>();
    [SerializeField]
    private AudioMixer musicAM = null, fxSoundAM = null;


    public SaveManager.SettingsSave soundSettings;

    public enum FXSounds
    {
        ENEMY_VOICE0, ENEMY_VOICE1, ENEMY_VOICE2,
        ENEMY_VOICE3, ENEMY_VOICE4, ENEMY_VOICE5,
        ENEMY_VOICE6, ENEMY_DEATH, PLAYER_DEATH,
        DOOR, PULSE_START,
        PULSE_MID, PULSE_END
    }

    void Awake()
    {
        if (smInstance_ == null)
        {
            smInstance_ = this;
            Load();
            Debug.Log("SoundManager Set");
        }
        else if (smInstance_ != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this);

    }

    void Load()
    {
        string loadString = SaveManager.LoadSettings();

        if (loadString != null)
        {
            Debug.Log("SettingsLoaded " + loadString);

            soundSettings = JsonUtility.FromJson<SaveManager.SettingsSave>(loadString);
        }
        else
        {
            soundSettings = new SaveManager.SettingsSave
            {
                fxVolume_ = 5,
                musicVolume_ = 1
            };
        }
    }

    public void Save()
    {
        SaveManager.SaveSettings(soundSettings.fxVolume_, soundSettings.musicVolume_);
    }
    public void Start()
    {
        SetFXVolume(soundSettings.fxVolume_);
        SetMusicVolume(soundSettings.musicVolume_);
    }

    public void PlayCLip(FXSounds clip)
    {
        AudioSource.PlayClipAtPoint(FXSoundsPool[Convert.ToInt32(clip)], transform.position);
    }
    public AudioClip GetClip(FXSounds sound)
    {
        return FXSoundsPool[Convert.ToInt32(sound)];
    }

    public void PublicSetFxVolume(bool on)
    {
        if (!on) SetFXVolume(-20);
        else SetFXVolume(soundSettings.fxVolume_);
    }
    public void SetFXVolume(float volume)
    {
        if (volume == -20)
        {
            volume = -80;
            fxSoundAM.SetFloat("FXVolume", volume);
        }
        else
        {
            soundSettings.fxVolume_ = volume;
            fxSoundAM.SetFloat("FXVolume", soundSettings.fxVolume_);
        }
        PlayerPrefs.SetFloat("FXVolume", soundSettings.fxVolume_);
    }

    public void SetMusicVolume(float volume)
    {
        if (volume == -30) volume = -80;
        musicAM.SetFloat("MusicVolume", volume);
        soundSettings.musicVolume_ = volume;
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }
    public float GetVolume(bool music_fx)
    {
        if (music_fx) return soundSettings.musicVolume_;
        else return soundSettings.fxVolume_;
    }
}