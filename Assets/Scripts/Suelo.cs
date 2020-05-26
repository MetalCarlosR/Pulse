using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Suelo : MonoBehaviour
{
    void Start()
    {
        gameObject.layer = LayerMask.NameToLayer("Muebles");
    }
}
