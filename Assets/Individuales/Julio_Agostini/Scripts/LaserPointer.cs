using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPointer : MonoBehaviour
{

    private LineRenderer lr;
    [SerializeField]
    private Transform startPoint;

    void Start()
    {
        lr = GetComponent<LineRenderer>();
        lr.positionCount = 3;
    }


    // Update is called once per frame
    void Update()
    {
        
        RaycastHit2D hit;
        hit = Physics2D.Raycast(startPoint.position, startPoint.up);
        lr.SetPosition(0, startPoint.position);
        lr.SetPosition(1, hit.point);
        Vector3 angleHit = Vector3.Reflect(startPoint.up, hit.normal);
        hit = Physics2D.Raycast(hit.point, angleHit);
        lr.SetPosition(2, hit.point);
       
    }


    public void SetLaser(bool active)
    {
        lr.enabled = active;
    }
}
