using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPointer : MonoBehaviour
{

    private LineRenderer lr;

    void Start()
    {
        lr = GetComponent<LineRenderer>();
        lr.positionCount = 3;
        lr.enabled = false;
    }


    // Update is called once per frame
    void Update()
    {
        
        RaycastHit2D hit;
        hit = Physics2D.Raycast(transform.position, transform.up);
        lr.SetPosition(0, transform.position);
        lr.SetPosition(1, hit.point);
        Vector3 angleHit = Vector3.Reflect(transform.up, hit.normal);
        hit = Physics2D.Raycast(hit.point, angleHit);
        lr.SetPosition(2, hit.point);
       
    }



    public void SetLaser(bool active)
    {
        if(lr)
            lr.enabled = active;
    }
}
