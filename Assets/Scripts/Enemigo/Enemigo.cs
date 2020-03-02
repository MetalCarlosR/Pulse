using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    [SerializeField]
    private FieldOfView fov = null;
    [SerializeField]
    private float fovSet = 90f, limit = 5f;
    [SerializeField]
    private Material fovMatPat = null, fovMatAlerted = null, fovMatAtacking = null;
    [SerializeField]
    private string layer_ = "";

    private Transform player;

    public enum State
    {
        Patrolling,
        Alerted,
        Atacking
    };
    private State state_;
    void Start()
    {
        gameObject.layer = LayerMask.NameToLayer(layer_);
        fov = Instantiate(fov.gameObject, null).GetComponent<FieldOfView>();
        fov.name = "FieldOfView" + this.name;
        fov.SetInstance(limit, fovSet);
        fov.gameObject.layer = this.gameObject.layer;
        SetState(State.Patrolling);
        if (GameManager.gmInstance_ != null) player = GameManager.gmInstance_.GetPlayerTransform();
    }

    private void Update()
    {
        if (player == null)
        {
            player = GameManager.gmInstance_.GetPlayerTransform();
        }
        if (fov != null)
        {
            if (fovSet != 360)
            {
                fov.SetAngle(-transform.right);
            }
            fov.SetOrigin(transform.position);
        }
        FindPlayer();
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
                else if (state_ == State.Atacking){
                    SetState(State.Alerted);    
                }
            }
        }
        else if (state_ != State.Patrolling)
        {
            SetState(State.Patrolling);
        }
    }

    private void OnDestroy()
    {
        if (fov != null)
        {
            Destroy(fov.gameObject);
        }
    }

    private void AttackPlayer()
    {

    }

    public void SetState(State state)
    {
        state_ = state;
        switch (state)
        {
            case State.Patrolling:
                fov.setMaterial(fovMatPat);
                break;
            case State.Alerted:
                fov.setMaterial(fovMatAlerted);
                break;
            case State.Atacking:
                fov.setMaterial(fovMatAtacking);
                break;

        }
    }
}
