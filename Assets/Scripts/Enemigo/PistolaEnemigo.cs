using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolaEnemigo : MonoBehaviour
{
    [SerializeField]
    private Transform pistola = null;
    [SerializeField]
    private GameObject bala = null;
    private AudioSource fire;
    private void Start()
    {
        fire = pistola.GetComponent<AudioSource>();
    }

    public void Shoot()
    {
        Instantiate(bala, pistola.position, Quaternion.Euler(transform.localEulerAngles)).GetComponent<Bullet>().SetBounce(0);
        fire.Play();
    }
}
