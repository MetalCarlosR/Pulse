using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoPared : MonoBehaviour
{
    [SerializeField]
    private Transform pared = null;
    [SerializeField]
    private GameObject bala = null;
    private AudioSource fire;
    private void Start()
    {
        fire = pared.GetComponent<AudioSource>();
    }

    public void Shoot()
    {
        Instantiate(bala, pared.position, Quaternion.Euler(transform.localEulerAngles)).GetComponent<Bullet>().SetBounce(0);
        fire.Play();
    }
}