using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SoundManager : MonoBehaviour
{

    public static SoundManager smInstance_;

    [SerializeField]
    private List<AudioSource> FXSoundsPool = new List<AudioSource>();
    [SerializeField]
    private List<AudioSource> EnemyVoicePool = new List<AudioSource>();


    public enum FXSounds
    {
        PLAYERSHOT, ENEMYSHOT, DOOR, ENEMYDEATH,
        PLAYERDEATH, WALKING, LASER, LASERSWITCH
    }
    public enum EnemyVoice { VOICE_1, VOICE_2, VOICE_3 }

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
    //public void PlayEnemyVoice(EnemyVoice enemy)
    //{
    //    EnemyVoicePool[Convert.ToInt32(enemy)].Play();
    //}

    public bool EnemyVoicePlaying()
    {
        int i = 0;
        while (i < EnemyVoicePool.Count && !EnemyVoicePool[i].isPlaying)
            i++;
        Debug.Log(i);
        Debug.Log("ec=" + EnemyVoicePool.Count);
        if (i == EnemyVoicePool.Count) return false;
        else return true;
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

    public void PlayLaser()
    {
        AudioSource walking = FXSoundsPool[Convert.ToInt32(FXSounds.LASER)];
        if (!walking.isPlaying)
        {
            walking.Play();
        }
    }

    public void StopWalking()
    {
        FXSoundsPool[Convert.ToInt32(FXSounds.WALKING)].Stop();
    }
}