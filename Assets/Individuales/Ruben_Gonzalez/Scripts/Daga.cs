using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Daga : MonoBehaviour
{
    private float cd = 0.15f , timeCD;
    private AudioSource source;
    private Animator animator;
    private void Start()
    {
        source = GetComponent<AudioSource>();
        animator = GetComponentInParent<Animator>();
        timeCD = Time.time;
    }
    public void Attack()
    {
        if (enabled && Time.time > (timeCD + cd))
        {
            source.Play();
            animator.SetTrigger("Ataque");
            timeCD = Time.time + cd;
            //StopAllCoroutines();
            //StartCoroutine(DagaAttack(daga, rotation, rotation + offset, 0));

        }
    }

    //IEnumerator DagaAttack(BoxCollider2D daga, float begin, float end, float time)
    //{
    //    daga.enabled = true;
    //    while (time < 1f)
    //    {
    //        transform.localRotation = Quaternion.Euler(0, 0, Mathf.Lerp(begin, end, time));
    //        time += 5f * Time.deltaTime;
    //        yield return null;
    //    }
    //    daga.enabled = false;
    //    canAttack = true;
    //    offset = -offset;
    //    rotation = -rotation;
    //}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<Enemigo>().Death();
            Destroy(gameObject);
        }
    }
}