using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private MovEnemigo movEnemigo;
    private AudioSource voices;
    private Animator animator;
    [SerializeField]
    private AudioSource steps = null;
    private AudioClip[] EnemyVoicePool = new AudioClip[5];
    private bool started = false;


    public enum State
    {
        Patrolling,
        Alerted,
        Lost,
        Atacking
    }
    private State state_ = State.Atacking;
    private State prevState_ = State.Alerted;
    public void StartDelay()
    {
        started = true;
        gameObject.layer = LayerMask.NameToLayer(layer_);
        gun = GetComponent<PistolaEnemigo>();
        rb = GetComponent<Rigidbody2D>();
        voices = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        movEnemigo = GetComponent<MovEnemigo>();
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
        if (SoundManager.smInstance_)
        {
            EnemyVoicePool[0] = SoundManager.smInstance_.GetClip(SoundManager.FXSounds.ENEMY_VOICE0);
            EnemyVoicePool[1] = SoundManager.smInstance_.GetClip(SoundManager.FXSounds.ENEMY_VOICE2);
            EnemyVoicePool[2] = SoundManager.smInstance_.GetClip(SoundManager.FXSounds.ENEMY_VOICE3);
            EnemyVoicePool[2] = SoundManager.smInstance_.GetClip(SoundManager.FXSounds.ENEMY_VOICE3);
            EnemyVoicePool[4] = SoundManager.smInstance_.GetClip(SoundManager.FXSounds.ENEMY_DEATH);
        }
        SetState(State.Patrolling);
    }


    private void Update()
    {
        if (started)
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
            Vector3 direction = (player.position - transform.position);
            if (Vector3.Distance(transform.position, player.position) < limit)
            {
                if (Vector3.Angle(transform.up, direction) < fovSet / 2)
                {
                    RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, limit);
                    //Debug.DrawRay(transform.position, direction, Color.green);
                    if (hit.collider != null && hit.collider.tag == "Player")
                    {
                        transform.up = direction;
                        if (state_ != State.Atacking)
                        {
                            if (prevState_ == State.Alerted) movEnemigo.ChasePlayer(player.position);
                            SetState(State.Atacking);
                        }
                        else if (state_ == State.Atacking && prevState_ == State.Alerted) movEnemigo.ChasePlayer(player.position);
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
            if (state_ == State.Alerted)
            {
                movEnemigo.ChasePlayer(player.position);
            }
        }

    }

    public void Death()
    {
        steps.Stop();
        AudioSource.PlayClipAtPoint(EnemyVoicePool[4], transform.position);
        Destroy(gameObject);
    }
    private void OnDestroy()
    {
        if (started)
        {
            if (GameManager.gmInstance_) GameManager.gmInstance_.RemoveEntity(gameObject);
            if (fov) Destroy(fov.gameObject);
            if (pulse) Destroy(pulse.gameObject);
        }

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
        animator.SetBool("Disparo", true);
        while (true)
        {
            yield return new WaitForSeconds(rateOfFire);
            gun.Shoot();
        }
    }

    IEnumerator ChasePlayer()
    {
        Debug.Log("Chase");
        movEnemigo.ChasePlayer(player.position);
        yield return new WaitForSeconds(5f);
        SetState(State.Lost);
    }

    IEnumerator LostPlayer()
    {
        movEnemigo.ClearPath();
        yield return new WaitForSeconds(5f);
        movEnemigo.Patroll();
        SetState(State.Patrolling);
    }

    public void SetState(State state)
    {
        if (state_ != state)
        {
            prevState_ = state_;
            if (prevState_ == State.Lost && !voices.isPlaying)
            {
                PlayEnemyVoice();
            }
            else if(prevState_ == State.Atacking){
                animator.SetBool("Disparo", false);
            }
            //TO DO
            StopAllCoroutines();
            switch (state)
            {
                case State.Patrolling:
                    fov.setMaterial(fovMatPat);
                    break;
                case State.Alerted:
                    StartCoroutine(ChasePlayer());
                    fov.setMaterial(fovMatAlerted);
                    break;
                case State.Lost:
                    StartCoroutine(LostPlayer());
                    break;
                case State.Atacking:
                    if (state_ == State.Patrolling) PlayEnemyVoice();
                    StartCoroutine(AttackPlayer());
                    fov.setMaterial(fovMatAtacking);
                    break;
            }
            state_ = state;
        }
    }

    public void PlayEnemyVoice()
    {
        Debug.Log("e");
        voices.clip = EnemyVoicePool[Random.Range(0, 4)];
        voices.Play();
    }
}