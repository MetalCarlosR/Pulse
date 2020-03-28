using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public static SoundManager smInstance_;

    [SerializeField]
    private List<AudioSource> audioPool = new List<AudioSource>();

    public enum Audio
    {
        SHOOT,
        SHOOTENEMY,
        DOOR,
        ENEMYDEATH,
        PLAYERDEATH,
        WALKING,
        LASER,
        LASERSWITCH
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

    public void PlaySound(Audio audio)
    {
        audioPool[Convert.ToInt32(audio)].Play();
    }

    public void StopSound(Audio audio)
    {
        audioPool[Convert.ToInt32(audio)].Stop();
    }

    public void PlayWalking()
    {
        AudioSource walking = audioPool[Convert.ToInt32(Audio.WALKING)];
        if (!walking.isPlaying)
        {
            walking.Play();
        }
    }

    public void PlayLaser()
    {
        AudioSource walking = audioPool[Convert.ToInt32(Audio.LASER)];
        if (!walking.isPlaying)
        {
            walking.Play();
        }
    }

    public void StopWalking()
    {
        audioPool[Convert.ToInt32(Audio.WALKING)].Stop();
    }
}