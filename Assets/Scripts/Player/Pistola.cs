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
    private Animator animator;
    private AudioSource fire;

    private int layerMask ;

    private void Start()
    {
        animator = GetComponent<Animator>();
        line_ = pistola.GetComponent<LineRenderer>();
        fire = pistola.GetComponent<AudioSource>();
        layerMask = ~(LayerMask.GetMask("Muebles") | LayerMask.GetMask("Laser") | LayerMask.GetMask("Ignore Raycast"));
    }
    public void Shoot()
    {
        if (enabled && (!GameManager.gmInstance_.EmptyGun() || GameManager.gmInstance_.Uammo ) )
        {
            animator.SetTrigger("Ataque");
            GameManager.gmInstance_.Shoot();
            Instantiate(bala, pistola.position, Quaternion.Euler(transform.localEulerAngles)).GetComponent<Bullet>().SetBounce(1);
            fire.Play();
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

            posHit[0] = pistola.position;
            hit = Physics2D.Raycast(pistola.position, pistola.up , 100, layerMask);
            posHit[1] = hit.point;
            
            Vector3 angleHit = Vector3.Reflect(pistola.up, hit.normal);
            
            hit = Physics2D.Raycast(hit.point, angleHit , 100, layerMask);
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
