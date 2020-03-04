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

    private Transform player;
    private Pistola gun;
    private Rigidbody2D rb;

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
        gun = GetComponent<Pistola>();
        rb = GetComponent<Rigidbody2D>();
        pulse = Instantiate(pulse.gameObject, GameManager.gmInstance_.GetPool(pulse).transform).GetComponent<PulseEnemigo>();
        // CAMBIAR INSTANTIATE A METODOS DELEGADOS
        pulse.SetEnemy(transform);
        pulse.name = "Pulse" + this.name;
        fov = Instantiate(fov.gameObject, GameManager.gmInstance_.GetPool(fov).transform).GetComponent<FieldOfView>();
        fov.name = "FieldOfView" + this.name;
        fov.SetInstance(limit, fovSet);
        fov.gameObject.layer = this.gameObject.layer;
        SetState(State.Patrolling);
        SetPulseState(false);
        if (GameManager.gmInstance_ != null) player = GameManager.gmInstance_.GetPlayerTransform();
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
        pulse.enabled = state;
    }
    void FindPlayer()
    {
        if (Vector3.Distance(transform.position, player.position) < limit)
        {
            Vector3 direction = (player.position - transform.position);
            if (Vector3.Angle(transform.up, direction) < fovSet / 2)
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, limit);
                Debug.DrawRay(transform.position, direction, Color.green);
                if (hit.collider != null && hit.collider.tag == "Player")
                {
                    transform.up = direction;
                    if (state_ != State.Atacking)
                    {
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

    private void OnDestroy()
    {
        if (fov) Destroy(fov.gameObject);
        if (pulse) Destroy(pulse.gameObject);
    }

    IEnumerator AttackPlayer()
    {
        while (true)
        {
            yield return new WaitForSeconds(rateOfFire);
            gun.Shoot(0);
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
}
