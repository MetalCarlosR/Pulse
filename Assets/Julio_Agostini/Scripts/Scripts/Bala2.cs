﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala2 : MonoBehaviour
{
    public int bounces = 1;

    void Start()
    {
        GetComponent<Rigidbody2D>().AddForce(transform.up * 10, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy" )
        {
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }
        else if (collision.gameObject.tag.Equals("Player"))
        {
            GameManager.Die();
            Destroy(this.gameObject);
        }
        if (bounces == 0)   Destroy(this.gameObject);
    
        bounces--;
    }
}
