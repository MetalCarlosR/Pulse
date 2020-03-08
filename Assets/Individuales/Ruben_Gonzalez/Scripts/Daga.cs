using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Daga : MonoBehaviour
{
    BoxCollider2D dagaCollider;
    private bool atacando = false;
    private void Start()
    {

        dagaCollider = GetComponent<BoxCollider2D>();
        dagaCollider.enabled = !enabled;

    }
    public void Attack()
    {
            transform.position += transform.up * 0.1f;
            dagaCollider.enabled = enabled;
            atacando = true;
            StartCoroutine(RegresarDaga());
    }

    private IEnumerator RegresarDaga()
    {
        yield return new WaitForSeconds(0.25f);
        transform.position -= transform.up * 0.1f;
        dagaCollider.enabled = !enabled;
        atacando = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy") Destroy(collision.gameObject);
    }

}