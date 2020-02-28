using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControllerMal2 : MonoBehaviour
{
    public Transform player;
    Vector3 pos;
    void Update()
    {
        if (player != null)
        {
            pos.Set(player.position.x, player.position.y, -10);
            transform.position = pos;
        }
    }
}
