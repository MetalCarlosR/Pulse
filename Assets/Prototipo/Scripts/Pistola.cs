using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistola : MonoBehaviour
{
    public Transform pistola;
    public GameObject bala;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(bala, pistola.position , Quaternion.Euler(transform.localEulerAngles));
        }
    }
}
