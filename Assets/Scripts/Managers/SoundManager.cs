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

    // CREAR LOS MÉTODOS PARA CAMBIAR EL VOLUMEN
    [SerializeField]
    private AudioMixer musicAM = null, fxSoundAM = null;
    private float fxVolume = 0;



    public enum FXSounds
    {
        ENEMY_VOICE0, ENEMY_VOICE1, ENEMY_VOICE2,
        ENEMY_VOICE3, ENEMY_DEATH,
        DOOR, PULSE_START,
        PULSE_MID, PULSE_END
    }
    //private void Start()
    //{
    //    menuManager = gameObject.GetComponent<MenuManager>();
    //}

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

    public void SetFXVolume(bool on) //Configurar el sonido de la muerte del jugador
    {
        //menuManager = gameObject.GetComponent<MenuManager>();
        if (on) fxSoundAM.SetFloat("FXVolume", fxVolume);
        else fxSoundAM.SetFloat("FXVolume", -80);
    }
    public void GetFXVolume(float volume) { fxVolume = volume; }
}