using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    Vector2 MouseDir;

    private int speed_ = 10;

    private bool pause_ = false;
    private bool pulse_ = false;
    [SerializeField]
    private FieldOfView fov;
    [SerializeField]
    private float fovSet = 0, limit = 0;

    private Pistola gun;
    private Daga daga;
    void Start()
    {
        gun = GetComponent<Pistola>();
        rb = GetComponent<Rigidbody2D>();
        daga = GetComponentInChildren<Daga>();
        if (GameManager.gmInstance_ != null)
        {
            GameManager.gmInstance_.SetPlayer(gameObject);
            GameManager.gmInstance_.AddEntity(gameObject);
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
        if (!pause_)
        {
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
                SoundManager.smInstance_.PlayShooting();
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
    void FixedUpdate()
    {
        rb.velocity = new Vector2(Input.GetAxis("Horizontal") * speed_, Input.GetAxis("Vertical") * speed_);
    }

    void LookAtMouse()
    {
        MouseDir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        transform.up = MouseDir;
    }
    public void Die()
    {
        Pistola pistola = GetComponent<Pistola>();
        gameObject.GetComponent<SpriteRenderer>().color = Color.gray;
        if (pistola)
        {
            pistola.enabled = false;
        }
        enabled = false;
        GameManager.gmInstance_.PlayerDeath();
    }

    private void OnPause()
    {
        pause_ = true;
    }
    private void OnResume()
    {
        pause_ = false;
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
