using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulseEnemigo : MonoBehaviour
{
    private Vector3 startingSize;
    private float growthPerSecond = 1.8f;
    private Transform assignEnemy;
    private AudioSource heartbeat;
    [SerializeField]
    private float waitTime = 2f;


    void Start()
    {
        startingSize = transform.localScale;
        heartbeat = GetComponent<AudioSource>();
        StartCoroutine(Reset());
        
    }


    void LateUpdate()
    {
        transform.localScale = new Vector3(transform.localScale.x + growthPerSecond *Time.deltaTime , transform.localScale.y + growthPerSecond * Time.deltaTime, 1);
    }


    private IEnumerator Reset()
    {
        ResetAll();
        if(enabled)heartbeat.Play();
        yield return new WaitForSeconds(waitTime);
        StartCoroutine(Reset());
    }
    public void ResetAll()
    {
        transform.localScale = startingSize;
        if (assignEnemy) transform.position = assignEnemy.position;
    }
    public void SetEnemy(Transform enemy)
    {
        assignEnemy = enemy;
        transform.position = assignEnemy.position;
    }
}
