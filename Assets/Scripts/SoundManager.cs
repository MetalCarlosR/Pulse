﻿using System;
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
}

//private AudioSource door, walking, enemyDeath;
//private List<AudioSource> shots;
//[SerializeField]
//private int numOfShots;
//[SerializeField]
//private GameObject shot;
//void Awake()
//{
//    {
//        if (smInstance_ == null)
//        {
//            smInstance_ = this;
//            shots = new List<AudioSource>(numOfShots);
//            for (int i = 0; i < numOfShots; i++)
//            {
//                CreateShot();
//            }
//            Debug.Log(shots);

//        }
//        else if (smInstance_ != this)
//        {
//            Destroy(gameObject);
//        }
//        DontDestroyOnLoad(this);
//    }
//}

//private void CreateShot()
//{
//    GameObject newShot;
//    newShot = Instantiate(shot);
//    shots.Add(newShot.GetComponent<AudioSource>());
//}

//public void PlayShooting()
//{
//    bool shot = false;
//    int i = 0;
//    while (!shot)
//    {
//        if (!shots[i].isPlaying)
//        {
//            shots[i].Play();
//            shot = true;
//        }
//        i++;
//    }
//}

//public void PlayDoor()
//{
//    door.Play();
//}
//public void Playwalking()
//{
//    walking.Play();
//}

//public void StopWalking()
//{
//    walking.Stop();
//}

//public void PlayEnemyDeath()
//{
//    enemyDeath.Play();
//}

//}