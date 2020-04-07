using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscenarioManager : MonoBehaviour
{
    void Start()
    {
        GameManager.gmInstance_.SetEscenario(transform);
    }
}
