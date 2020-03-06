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
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && !atacando)//atacando se tiene que borrar cuando se incluya en el player controler
        {
            transform.position += transform.right * 0.1f;
            dagaCollider.enabled = enabled;
            atacando = true;
            StartCoroutine(RegresarDaga());
        }
    }

    private IEnumerator RegresarDaga()
    {
        yield return new WaitForSeconds(0.25f);
        transform.position -= transform.right * 0.1f;
        dagaCollider.enabled = !enabled;
        atacando = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(collision.gameObject);
    }

}