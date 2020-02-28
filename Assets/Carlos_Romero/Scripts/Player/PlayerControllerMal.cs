using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerMal : MonoBehaviour
{
    Rigidbody2D rb;
    Vector2 MouseDir;

    public int speed;

    //private GameObject FOV;
    [SerializeField]
    private FieldOfView fov;
    [SerializeField]
    private float fovSet = 0, limit = 0;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        fov = Instantiate(fov.gameObject, Vector3.zero, Quaternion.identity).GetComponent<FieldOfView>();
        fov.name = "FieldOfView" + this.name;
        fov.SetInstance(limit, fovSet, false);         
    }

    void Update()
    {
        Debug.DrawRay(transform.position, transform.up * 2, Color.red);
        LookAtMouse();
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
}
