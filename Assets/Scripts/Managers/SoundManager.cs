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
    private AudioMixer musicAM, fxSoundAM;
    private float fxVolume, musicVolume;
    [SerializeField]
    private Slider fxSlider, musicSlider; 


    public enum FXSounds
    {
        ENEMY_VOICE0, ENEMY_VOICE1, ENEMY_VOICE2,
        ENEMY_VOICE3, ENEMY_VOICE4, ENEMYVOICE_5,
        ENEMY_VOICE6, ENEMY_DEATH, PLAYER_DEATH,
        DOOR, PULSE_START,
        PULSE_MID, PULSE_END
    }

    void Awake()
    {
        smInstance_ = this;
       
    }
    private void Start()
    {
        if (fxSlider && musicSlider)
        {
            fxVolume = PlayerPrefs.GetFloat("FXVolume", 4);
            musicVolume = PlayerPrefs.GetFloat("MusicVolume", 0);
            fxSoundAM.SetFloat("FXVolume", fxVolume);
            musicAM.SetFloat("MusicVolume", musicVolume);
            fxSlider.value = fxVolume;
            musicSlider.value = musicVolume;
        }
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
        PlayerPrefs.SetFloat("FXVolume", fxVolume);
    }

    public void SetMusicVolume(float volume)
    {
        if (volume == -30) volume = -80;
        musicAM.SetFloat("MusicVolume", volume);
        musicVolume = volume;
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }
    public float GetVolume(bool music_fx)
    {
        if (music_fx) return musicVolume;
        else return fxVolume;  
    }
}