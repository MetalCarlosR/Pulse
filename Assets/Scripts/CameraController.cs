using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Transform player = null;
    Vector3 pos;


    private void Start()
    {
        GameManager.gmInstance_.SetCamera(GetComponent<Camera>());  
    }

    void Update()
    {
        pos.Set(player.position.x, player.position.y , -10);
        transform.position = pos;
    }
}
