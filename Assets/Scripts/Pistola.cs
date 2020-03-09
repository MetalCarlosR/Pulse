using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistola : MonoBehaviour
{
    [SerializeField]
    private Transform pistola = null;
    [SerializeField]
    private GameObject bala = null;

    public void Shoot(int bounces)
    {
        if (enabled) Instantiate(bala, pistola.position, Quaternion.Euler(transform.localEulerAngles)).GetComponent<Bullet>().SetBounce(bounces);
    }
}
