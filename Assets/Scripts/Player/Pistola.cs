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

    LineRenderer line_;


    private void Start()
    {
        line_ = pistola.GetComponent<LineRenderer>();
    }
    public void Shoot()
    {
        if (enabled && !GameManager.gmInstance_.EmptyGun())
        {
            GameManager.gmInstance_.Shoot();
            Instantiate(bala, pistola.position, Quaternion.Euler(transform.localEulerAngles)).GetComponent<Bullet>().SetBounce(1);
            SoundManager.smInstance_.PlaySound(SoundManager.FXSounds.PLAYERSHOT);
        }
    }
    public void Laser(bool laser)
    {
        if (enabled)
        {
            line_.enabled = laser;
            laser_ = laser;
        }

    }

    private void Update()
    {
        if (laser_ && enabled)
        {
            RaycastHit2D hit;
            Vector3[] posHit = new Vector3[3];

            posHit[0] = transform.position;

            hit = Physics2D.Raycast(transform.position, transform.up);
            posHit[1] = hit.point;

            Vector3 angleHit = Vector3.Reflect(transform.up, hit.normal);

            hit = Physics2D.Raycast(hit.point, angleHit);
            posHit[2] = hit.point;

            line_.SetPositions(posHit);
        }
    }

    private void OnDisable()
    {
        if (line_)
        {
            line_.enabled = false;
        }
    }
}
