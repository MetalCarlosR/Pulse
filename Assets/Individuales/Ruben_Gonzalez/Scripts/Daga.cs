using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Daga : MonoBehaviour
{
    BoxCollider2D daga;
    float rotation;
    bool canAttack = true;
    int offset = 90;
    private void Start()
    {
        daga = GetComponent<BoxCollider2D>();
        daga.enabled = false;
        transform.rotation = Quaternion.identity;
        rotation = transform.rotation.z;
        rotation = -45;
        transform.rotation = Quaternion.Euler(0, 0, rotation);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && canAttack)
        {
            canAttack = false;
            StopAllCoroutines();
            StartCoroutine(Ataque(daga, rotation, rotation + offset, 0));
        }

    }

    IEnumerator Ataque(BoxCollider2D daga, float begin, float end, float time)
    {
        daga.enabled = true;
        while (time < 1f)
        {
            transform.localRotation = Quaternion.Euler(0, 0, Mathf.Lerp(begin, end, time));
            time += 2f * Time.deltaTime;
            yield return null;
        }
        daga.enabled = false;
        canAttack = true;
        offset = -offset;
        rotation = -rotation;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(collision.gameObject);
    }

}