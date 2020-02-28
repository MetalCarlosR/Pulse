using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirar : MonoBehaviour
{
    Vector3 mouseposition;
    Vector2 direction;
    void Update()
    {
        mouseposition = Input.mousePosition;
        mouseposition = Camera.main.ScreenToWorldPoint(mouseposition);

        direction = new Vector2(mouseposition.x - transform.position.x, mouseposition.y - transform.position.y);

        transform.up = direction;
    }
}
