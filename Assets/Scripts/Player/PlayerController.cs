using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    Vector2 MouseDir;

    public int speed;

    //private GameObject FOV;
    [SerializeField]
    private FieldOfView fov;
    [SerializeField]
    private float fovSet = 0, limit = 0;

    private Pistola gun;
    void Start()
    {
        if (GameManager.gmInstance_ != null)
        {
            GameManager.gmInstance_.SetPlayer(gameObject);
        }
        else
        {
            Debug.LogError("Warnign GameManager was null when trying to access it from " + this);
        }
        gun = GetComponent<Pistola>();
        rb = GetComponent<Rigidbody2D>();
        fov = Instantiate(fov.gameObject, null).GetComponent<FieldOfView>();
        fov.name = "FieldOfView" + name;
        fov.SetInstance(limit, fovSet);
    }

    void Update()
    {
        Debug.DrawRay(transform.position, transform.up * 2, Color.red);
        LookAtMouse();
        if (fov != null)
        {
            if (fovSet != 360)
            {
                fov.SetAngle(-transform.right);
            }
            fov.SetOrigin(transform.position);
        }
        if (Input.GetButtonDown("Fire1"))
        {
            gun.Shoot(1);
        }
    }
    void FixedUpdate()
    {
        rb.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, Input.GetAxis("Vertical") * speed);
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
}
