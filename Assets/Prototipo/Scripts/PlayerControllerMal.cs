using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerMal : MonoBehaviour
{
    Rigidbody2D rb;
    Vector2 MouseDir;
    public int speed;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Debug.DrawRay(transform.position, transform.up * 2, Color.red);
        LookAtMouse();
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
