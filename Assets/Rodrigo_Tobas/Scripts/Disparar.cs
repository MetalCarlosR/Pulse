using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disparar : MonoBehaviour
{
    public GameObject Proyectil;
    public Transform Canon;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
            Instantiate(Proyectil, Canon.position, Quaternion.Euler(transform.localEulerAngles));
    }
}
