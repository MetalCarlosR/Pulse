using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistola : MonoBehaviour
{
    [SerializeField]
    private Transform pistola = null;
    [SerializeField]
    private GameObject bala = null;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(bala, pistola.position , Quaternion.Euler(transform.localEulerAngles));
        }
    }
}
