using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulseEnemigo : MonoBehaviour
{
    private Vector3 startingSize;
    private float growth = 0.03f;
    private Transform assignEnemy;


    void Start()
    {
        startingSize = transform.localScale;
        StartCoroutine(Reset());
    }


    void LateUpdate()
    {
        transform.localScale = new Vector3(transform.localScale.x + growth, transform.localScale.y + growth, 1);
    }


    private IEnumerator Reset()
    {
        if(assignEnemy) transform.position = assignEnemy.position;
        yield return new WaitForSeconds(2f);
        transform.localScale = startingSize;
        StartCoroutine(Reset());
    }

    public void SetEnemy(Transform enemy)
    {
        assignEnemy = enemy;
        transform.position = assignEnemy.position;
    }
}
