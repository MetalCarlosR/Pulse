using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private int bounces_ = 1;

    void Start()
    {
        GetComponent<Rigidbody2D>().AddForce(transform.up * 10, ForceMode2D.Impulse);
        Invoke("Destroy", 5f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<Enemigo>().Death();
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "Player" )
        {
            collision.gameObject.GetComponent<PlayerController>().Die();
            Destroy(gameObject);
        }
        if (bounces_ == 0) Destroy(gameObject);

        bounces_--;
    }

    public void SetBounce(int bounces)
    {
        bounces_ = bounces;
    }
}
