using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPoint : MonoBehaviour
{
    void Start()
    {
        GameManager.gmInstance_.SetEnd(transform);
    }
}
