using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SoundManager : MonoBehaviour
{

    public static SoundManager smInstance_;

    [SerializeField]
    private List<AudioSource> FXSoundsPool = new List<AudioSource>();



    public enum FXSounds
    {
        PLAYERSHOT, ENEMYSHOT, DOOR, ENEMYDEATH,
        PLAYERDEATH, WALKING, LASER, LASERSWITCH,
        DAGA, PULSESTART, PULSEMID, PULSEEND
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

    public void PlaySound(FXSounds audio)
    { 
        FXSoundsPool[Convert.ToInt32(audio)].Play();
    }

    public void StopSound(FXSounds audio)
    {
        FXSoundsPool[Convert.ToInt32(audio)].Stop();
    }

    public void PlayWalking()
    {
        AudioSource walking = FXSoundsPool[Convert.ToInt32(FXSounds.WALKING)];
        if (!walking.isPlaying)
        {
            walking.Play();
        }
    }

    public void StopWalking()
    {
        FXSoundsPool[Convert.ToInt32(FXSounds.WALKING)].Stop();
    }


    public void OnDeathReset()
    {
        StopWalking();
    }

}