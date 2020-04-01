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
    public void Attack()
    {
        if (canAttack && enabled)
        {
            canAttack = false;
            SoundManager.smInstance_.PlaySound(SoundManager.FXSounds.DAGA, transform.position);
            StopAllCoroutines();
            StartCoroutine(DagaAttack(daga, rotation, rotation + offset, 0));
            
        }
    }

    IEnumerator DagaAttack(BoxCollider2D daga, float begin, float end, float time)
    {
        daga.enabled = true;
        while (time < 1f)
        {
            transform.localRotation = Quaternion.Euler(0, 0, Mathf.Lerp(begin, end, time));
            time += 5f * Time.deltaTime;
            yield return null;
        }
        daga.enabled = false;
        canAttack = true;
        offset = -offset;
        rotation = -rotation;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
            Destroy(collision.gameObject);
    }
}