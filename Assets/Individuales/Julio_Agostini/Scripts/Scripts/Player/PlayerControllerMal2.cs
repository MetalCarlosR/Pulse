using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerMal2 : MonoBehaviour
{
    Rigidbody2D rb;
    Vector2 MouseDir;
    private bool dead = false;

    public int speed;
    [SerializeField]
    private GameObject FOV;
    private FieldOfView2 fov;
    [SerializeField]
    private float fovSet = 0, limit = 0;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        fov = Instantiate(FOV, Vector3.zero, Quaternion.identity).GetComponent<FieldOfView2>();
        fov.name = "FieldOfView" + this.name;
        fov.SetFov(fovSet);
        fov.SetLimit(limit);
           
    }

    void Update()
    {
        Debug.DrawRay(transform.position, transform.up * 2, Color.red);
        if (!dead)
        {
            LookAtMouse();
        }
        if (fov != null) {
            if (fovSet != 360)
            {
                fov.SetAngle(-transform.right);
            }
            fov.SetOrigin(transform.position);
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
        this.gameObject.GetComponent<SpriteRenderer>().color = Color.gray;
        Destroy(fov);
        dead = true;
        
        //this.gameObject.SetActive(false);
    }

}
