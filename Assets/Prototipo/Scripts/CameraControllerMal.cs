﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControllerMal : MonoBehaviour
{
    public Transform player;
    Vector3 pos;
    void Update()
    {
        pos.Set(player.position.x, player.position.y , -10);
        transform.position = pos;
    }
}
