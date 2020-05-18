﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    Vector2 MouseDir;

    private int speed_ = 10;

    private bool pulse_ = false;
    private FieldOfView fov;
    private float fovSet = 360, limit = 50;

    [SerializeField]
    private GameObject black = null;
    private Pistola gun;
    private Daga daga;
    private AudioSource walking;
    void Start()
    {
        gun = GetComponent<Pistola>();
        rb = GetComponent<Rigidbody2D>();
        daga = GetComponentInChildren<Daga>();
        walking = GetComponent<AudioSource>();
        black.SetActive(true);
        if (GameManager.gmInstance_ != null)
        {
            GameManager.gmInstance_.SetPlayer(gameObject);
            fov = GameManager.gmInstance_.createFieldofView();
            fov.name = "FieldOfView" + name;
            fov.SetInstance(limit, fovSet);
        }
        else
        {
            Debug.LogError("Warnign GameManager was null when trying to access it from " + this);
        }


    }
    void Update()
    {
        if (!GameManager.gmInstance_.IsGamePaused())
        {
            rb.velocity = new Vector2(Input.GetAxis("Horizontal") * speed_, Input.GetAxis("Vertical") * speed_);
            if (rb.velocity.y != 0 || rb.velocity.x != 0)
            {
                PlayWalking();
            }
            else
            {
                walking.Stop();
            }
            LookAtMouse();
            if (fov != null)
            {
                if (fovSet != 360)
                {
                    fov.SetAngle(-transform.right);
                }
                fov.SetOrigin(transform.position);
            }
            if (Input.GetButtonDown("Fire1") && !pulse_)
            {
                daga.Attack();
                gun.Shoot();
            }
            if (Input.GetButtonDown("Fire2") && !pulse_)
            {
                gun.Laser(true);
            }
            else if (Input.GetButtonUp("Fire2"))
            {
                gun.Laser(false);
            }
        }
    }

    private void PlayWalking()
    {
        if (!walking.isPlaying)
        {
            walking.Play();
        }
    }

    void LookAtMouse()
    {
        MouseDir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        transform.up = MouseDir;
    }
    public void Die()
    {
        if (!GameManager.gmInstance_.TGM)
        {
            Pistola pistola = GetComponent<Pistola>();
            gameObject.GetComponent<SpriteRenderer>().color = Color.gray;
            walking.Stop();
            if (pistola)
            {
                pistola.enabled = false;
            }
            enabled = false;
            walking.loop = false;
            walking.clip = SoundManager.smInstance_.GetClip(SoundManager.FXSounds.PLAYER_DEATH);
            StartCoroutine(PlayerDeathSound());
        }  
    }

    private IEnumerator PlayerDeathSound()
    {
        walking.Play();
        GameManager.gmInstance_.PlayerDeath();
        yield return new WaitForSeconds(3.5f);
        SoundManager.smInstance_.PublicSetFxVolume(false);

    }

    public void UsePulse(bool pulse)
    {
        pulse_ = pulse;
        if (!pulse) speed_ = 10;
        else speed_ = 0;
    }
    public int GetSpeed()
    {
        return speed_;
    }


}
