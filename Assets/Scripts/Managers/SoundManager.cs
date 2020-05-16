using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class SoundManager : MonoBehaviour
{

    public static SoundManager smInstance_;

    [SerializeField]
    private List<AudioClip> FXSoundsPool = new List<AudioClip>();
    [SerializeField]
    private AudioMixer musicAM, fxSoundAM;
    private float fxVolume;


    public enum FXSounds
    {
        ENEMY_VOICE0, ENEMY_VOICE1, ENEMY_VOICE2,
        ENEMY_VOICE3, ENEMY_DEATH,
        DOOR, PULSE_START,
        PULSE_MID, PULSE_END
    }

    void Awake()
    {
        if (smInstance_ == null)
        {
            smInstance_ = this;
        }
        else if (smInstance_ != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this);
    }

    public AudioClip GetClip(FXSounds sound)
    {
        return FXSoundsPool[Convert.ToInt32(sound)];
    }

    public void PublicSetFxVolume(bool on)
    {
        if (!on) SetFXVolume(-20);
        else SetFXVolume(fxVolume);
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
            fxVolume = volume;
            fxSoundAM.SetFloat("FXVolume", fxVolume);
        }
    }

    public void SetMusicVolume(float volume)
    {
        if (volume == -30) volume = -80;
        musicAM.SetFloat("MusicVolume", volume);
    }

}