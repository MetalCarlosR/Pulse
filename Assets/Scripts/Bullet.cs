using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
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
        else if (collision.gameObject.tag == "Player")
        {
            Destroy(this.gameObject);
            collision.gameObject.GetComponent<PlayerControllerMal2>().Die();
        }
        if (bounces == 0)   Destroy(this.gameObject);
    
        bounces--;
    }
}
