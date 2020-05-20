using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Muebles : MonoBehaviour
{

    void Start()
    {
        if (GameManager.gmInstance_) GameManager.gmInstance_.addMuebles(gameObject);
    }
}
