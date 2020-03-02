using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoBala : MonoBehaviour
{
    Rigidbody2D rb;
    public float velocidadBala;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(transform.right * velocidadBala, ForceMode2D.Impulse);
           
        
    }
}
