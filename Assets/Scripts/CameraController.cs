using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform player = null;
    Vector3 pos;


    private void Start()
    {
        if (GameManager.gmInstance_)
        {
            player = GameManager.gmInstance_.SetCamera(GetComponent<Camera>());
        }
    }

    void Update()
    {
        if (player)
        {
            pos.Set(player.position.x, player.position.y, -10);
            transform.position = pos;
        }
        else player = GameManager.gmInstance_.SetCamera(GetComponent<Camera>());
    }
}
