using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SoundManager : MonoBehaviour
{

    public static SoundManager smInstance_;

    [SerializeField]
    private List<AudioClip> FXSoundsPool = new List<AudioClip>();



    public enum FXSounds
    {
        ENEMY_VOICE1, ENEMY_VOICE2,
        ENEMY_VOICE3, ENEMY_DEATH, 
        PULSE_START,
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
}