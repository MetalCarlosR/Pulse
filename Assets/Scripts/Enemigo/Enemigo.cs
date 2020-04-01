﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class Enemigo : MonoBehaviour
{
    [SerializeField]
    private FieldOfView fov = null;
    [SerializeField]
    private float fovSet = 90f, limit = 5f, rateOfFire = 0.5f;
    [SerializeField]
    private Material fovMatPat = null, fovMatAlerted = null, fovMatAtacking = null;
    [SerializeField]
    private string layer_ = "";
    [SerializeField]
    private PulseEnemigo pulse;
    private bool pause_ = false;
    private Transform player;
    private PistolaEnemigo gun;
    private Rigidbody2D rb;
    [SerializeField]
    List<AudioSource> EnemyVoicePool;
    public enum EnemyVoice { VOICE_1, VOICE_2, VOICE_3 }

    public enum State
    {
        Patrolling,
        Alerted,
        Lost,
        Atacking
    }
    private State state_ = State.Alerted;
    void Start()
    {
        gameObject.layer = LayerMask.NameToLayer(layer_);
        gun = GetComponent<PistolaEnemigo>();
        rb = GetComponent<Rigidbody2D>();
        if (GameManager.gmInstance_ != null)
        {
            player = GameManager.gmInstance_.GetPlayerTransform();
            GameManager.gmInstance_.AddEntity(gameObject);
            pulse = GameManager.gmInstance_.createPulse();
            pulse.SetEnemy(transform);
            pulse.name = "Pulse" + this.name;
            fov = GameManager.gmInstance_.createFieldofView();
            fov.name = "FieldOfView" + this.name;
            fov.SetInstance(limit, fovSet);
            fov.gameObject.layer = gameObject.layer;
            fov.gameObject.layer = this.gameObject.layer;

            SetPulseState(false);
        }
        SetState(State.Patrolling);
    }


    private void Update()
    {
        if (fov != null)
        {
            if (fovSet != 360)
            {
                fov.SetAngle(-transform.right);
            }
            fov.SetOrigin(transform.position);
        }
        if (player != null)
        {
            FindPlayer();
        }
        else
        {
            player = GameManager.gmInstance_.GetPlayerTransform();
        }
    }

    public void SetPulseState(bool state)
    {
        if (!state) pulse.ResetAll();
        pulse.enabled = state;
    }
    void FindPlayer()
    {
        if (!pause_)
        {
            if (Vector3.Distance(transform.position, player.position) < limit)
            {
                Vector3 direction = (player.position - transform.position);
                if (Vector3.Angle(transform.up, direction) < fovSet / 2)
                {
                    RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, limit);
                    //Debug.DrawRay(transform.position, direction, Color.green);
                    if (hit.collider != null && hit.collider.tag == "Player")
                    {
                        transform.up = direction;
                        if (state_ != State.Atacking)
                        {
                            PlayEnemyVoice();
                            SetState(State.Atacking);
                        }
                    }
                    else if (state_ == State.Atacking)
                    {
                        SetState(State.Alerted);
                    }
                }
                else if (state_ == State.Atacking)
                {
                    SetState(State.Alerted);
                }
            }
            else if (state_ == State.Atacking)
            {
                SetState(State.Alerted);
            }
        }
    }

    private void OnDestroy()
    {
        if (SoundManager.smInstance_) SoundManager.smInstance_.PlaySound(SoundManager.FXSounds.ENEMYDEATH);
        if (fov) Destroy(fov.gameObject);
        if (pulse) Destroy(pulse.gameObject);
    }
    private void OnPause()
    {
        pause_ = true;
    }
    private void OnResume()
    {
        pause_ = false;
    }

    IEnumerator AttackPlayer()
    {
        while (true)
        {
            yield return new WaitForSeconds(rateOfFire);
            gun.Shoot();
        }

    }

    void ChasePlayer()
    {
        // PATHFINDING NAVMESH O A*
        SetState(State.Lost);
    }

    IEnumerator LostPlayer()
    {
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(5f);
        SetState(State.Patrolling);
    }

    public void SetState(State state)
    {
        if (state_ != state)
        {
            StopAllCoroutines();
            switch (state)
            {
                case State.Patrolling:
                    fov.setMaterial(fovMatPat);
                    break;
                case State.Alerted:
                    ChasePlayer();
                    fov.setMaterial(fovMatAlerted);
                    break;
                case State.Lost:
                    StartCoroutine(LostPlayer());
                    break;
                case State.Atacking:
                    StartCoroutine(AttackPlayer());
                    fov.setMaterial(fovMatAtacking);
                    break;

            }
            state_ = state;
        }
    }
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
    public void PlayEnemyVoice()
    {
        Random r = new Random();
        int i = r.Next(0, EnemyVoicePool.Count);
        if (!EnemyVoicePlaying())
            EnemyVoicePool[i].Play();
    }
}