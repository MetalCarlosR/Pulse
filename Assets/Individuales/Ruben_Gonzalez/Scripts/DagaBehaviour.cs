using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DagaBehaviour : MonoBehaviour
{
    BoxCollider2D colliderDaga;
    private bool atacando = false;
    private void Start()
    {
        colliderDaga = GetComponent<BoxCollider2D>();
        colliderDaga.enabled = !enabled;
    }
    void Update()
    {
        if (Input.GetKeyDown("space") && !atacando)
        {
            transform.position += transform.right * 0.5f;
            colliderDaga.enabled = enabled;
            atacando = true;
            
            StartCoroutine("RegresarDaga");
        }
        /*
        else if (Input.GetKeyUp("space"))
        {
            transform.position -= transform.right * 0.2f;
            colliderDaga.enabled = !enabled;
        }
        */
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(collision.gameObject);
    }

    private IEnumerator RegresarDaga()
    {
        yield return new WaitForSeconds(0.25f);
        transform.position -= transform.right * 0.5f;
        colliderDaga.enabled = !enabled;
        atacando = false;

    }

}
