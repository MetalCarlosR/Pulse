using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControllerMal : MonoBehaviour
{
    [SerializeField]
    private Transform player = null;
    Vector3 pos;


    void Update()
    {
        pos.Set(player.position.x, player.position.y , -10);
        transform.position = pos;
    }
}
