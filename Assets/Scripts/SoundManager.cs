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
        WALKING
    }

    void Awake()
    {
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
    }

    public void PlaySound(Audio audio)
    {
        audioPool[Convert.ToInt32(audio)].Play();
    }
}
