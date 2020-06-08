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
            Debug.Log("SoundManager Set");
        }
        else if (smInstance_ != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this);

    }


    public void Start()
    {
        SetFXVolume(SettingsManager.smInstance_.GetSettings().fxVolume_);
        SetMusicVolume(SettingsManager.smInstance_.GetSettings().musicVolume_);
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
        else SetFXVolume(SettingsManager.smInstance_.GetSettings().fxVolume_);
    }
    public void SetFXVolume(float volume)
    {
        if (volume == -30)
        {
            volume = -80;
            fxSoundAM.SetFloat("FXVolume", volume);
        }
        else
        {
            SettingsManager.smInstance_.GetSettings().fxVolume_ = volume;
            fxSoundAM.SetFloat("FXVolume", SettingsManager.smInstance_.GetSettings().fxVolume_);
        }
        PlayerPrefs.SetFloat("FXVolume", SettingsManager.smInstance_.GetSettings().fxVolume_);
    }

    public void SetMusicVolume(float volume)
    {
        if (volume == -30) volume = -80;
        musicAM.SetFloat("MusicVolume", volume);
        SettingsManager.smInstance_.GetSettings().musicVolume_ = volume;
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }
    public float GetVolume(bool music_fx)
    {
        if (music_fx) return SettingsManager.smInstance_.GetSettings().musicVolume_;
        else return SettingsManager.smInstance_.GetSettings().fxVolume_;
    }
}