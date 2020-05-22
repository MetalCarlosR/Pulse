using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disparar : MonoBehaviour
{
    public GameObject Proyectil;
    public Transform Canon;

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
            Instantiate(Proyectil, Canon.position, Quaternion.Euler(transform.localEulerAngles));
    }
}
