using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DagaBehaviour : MonoBehaviour
{
    DagaAttack daga;
    BoxCollider2D dagaCollider;
    private bool atacando = false;
    private void Start()
    {
        daga = GetComponent<DagaAttack>();
        if (daga.GetComponent<BoxCollider2D>() != null)
        {
            dagaCollider = daga.GetComponent<BoxCollider2D>();
            dagaCollider.enabled = !enabled;
        }
        else Debug.LogError("There is no BoxCollider2D attached on " + this);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && !atacando)
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

}