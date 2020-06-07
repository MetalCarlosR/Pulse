using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camara : MonoBehaviour
{
    [SerializeField]
    private FieldOfView fov = null;
    [SerializeField]
    private float fovSet = 90f, limit = 5f;
    [SerializeField]
    private Material fovMatPat = null, fovMatAlerted = null;
    [SerializeField]
    private string layer_ = "";
    private AudioSource camaraFX;

    private Transform player;

    private enum State
    {
        Patrolling,
        Alerted
    }
    private State state_ = State.Alerted;
    void Start()
    {
        gameObject.layer = LayerMask.NameToLayer(layer_);

        if (GameManager.gmInstance_ != null)
        {
            player = GameManager.gmInstance_.GetPlayerTransform();
            //GameManager.gmInstance_.AddEntity(gameObject);
            fov = GameManager.gmInstance_.createFieldofView();
            fov.name = "FieldOfView" + this.name;
            fov.SetInstance(limit, fovSet);
            fov.gameObject.layer = gameObject.layer;
            fov.gameObject.layer = this.gameObject.layer;
        }
        SetState(State.Patrolling);
        camaraFX = GetComponent<AudioSource>();
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

    void FindPlayer()
    {
        if (!GameManager.gmInstance_.IsGamePaused())
        {
            if (Vector3.Distance(transform.position, player.position) < limit)
            {
                Vector3 direction = (player.position - transform.position);
                if (Vector3.Angle(transform.up, direction) < fovSet / 2)
                {
                    int jugador = LayerMask.GetMask("Jugador");
                    RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, limit, jugador);
                    Debug.DrawRay(transform.position, direction, Color.green);
                    if (hit.collider != null && hit.collider.tag == "Player")
                    {
                        transform.up = direction;
                        if (state_ != State.Alerted) SetState(State.Alerted);
                    }
                    else if (state_ == State.Alerted) SetState(State.Patrolling);
                }
                else if (state_ == State.Alerted) SetState(State.Patrolling);
            }
            else if (state_ == State.Alerted) SetState(State.Patrolling);
        }
    }

    void FoundEnemy()
    {
        Collider2D[] coll = Physics2D.OverlapCircleAll(transform.position, 100);

        foreach (Collider2D col in coll)
        {
            Enemigo enemy = col.GetComponent<Enemigo>();
            if (enemy)
            {
                enemy.SetState(Enemigo.State.Alerted);
            }
        }
    }
    private void SetState(State state)
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
                    FoundEnemy();
                    fov.setMaterial(fovMatAlerted);
                    camaraFX.Play();
                    break;
            }
            state_ = state;
        }
    }
}
