using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineaDeVison : MonoBehaviour
{
    public float radioVision;

    [Range(0,360)]
    public float anguloVision;

    public LayerMask jugador;
    public LayerMask obstaculo;

    private void Start()
    {
        StartCoroutine(CheckTimeFix(0.1f));
    }


    public Vector3 ApuntarAngulo(float angulo)
    {
        angulo += transform.eulerAngles.z;
        return new Vector3(Mathf.Sin(angulo * Mathf.Deg2Rad), Mathf.Cos(angulo * Mathf.Deg2Rad), 0);
    }

    IEnumerator CheckTimeFix(float timeFix)
    {
        while (true)
        {
            yield return new WaitForSeconds(timeFix);
            CheckLineaDeVison();
        }
    }

    void CheckLineaDeVison()
    {
        Collider2D target = Physics2D.OverlapCircle(transform.position, radioVision,jugador);
                
        
        if (target != null)
        {
            Transform targetPos = target.transform;
            Vector3 dir = (targetPos.position - transform.position).normalized;

            if(Vector3.Angle(transform.up , dir)  < anguloVision / 2){
                transform.up = dir;
            }

        }
       
    }
}
