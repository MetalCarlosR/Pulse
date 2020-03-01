using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DagaBehaviour : MonoBehaviour
{
    BoxCollider2D colliderDaga;
    private void Start()
    {
        colliderDaga = GetComponent<BoxCollider2D>();
        colliderDaga.enabled = !enabled;
    }
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            transform.position += transform.right * 0.2f;
            colliderDaga.enabled = enabled;
        }
        else if (Input.GetKeyUp("space"))
        {
            transform.position -= transform.right * 0.2f;
            colliderDaga.enabled = !enabled;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(collision.gameObject);
    }

}
