using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistola : MonoBehaviour
{
    [SerializeField]
    private Transform pistola = null;
    [SerializeField]
    private GameObject bala = null;

    bool laser_ = false;

    [SerializeField]
    private LaserPointer line_;



    public void Shoot()
    {
        if (enabled) Instantiate(bala, pistola.position, Quaternion.Euler(transform.localEulerAngles)).GetComponent<Bullet>().SetBounce(1);
    }
    public void Laser(bool laser)
    {
        if (enabled)
        {
            line_.enabled = laser;
            laser_ = laser;
        }
    }

    private void OnDisable()
    {
        if (line_)
        {
            line_.SetLaser(false);
        }
    }

    private void OnEnable()
    {
        if (line_)
        {
            line_.SetLaser(true);
        }
    }
}
