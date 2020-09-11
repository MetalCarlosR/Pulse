using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanzaPared : MonoBehaviour
{
    [SerializeField]
    private string layer_ = "";
    [SerializeField]
    private float intermitencia = 2f;
    [SerializeField]
    private GameObject bala = null;
    private AudioSource fire;

    void Start()
    {
        fire = GetComponent<AudioSource>();
        gameObject.layer = LayerMask.NameToLayer(layer_);
        StartCoroutine(LanzaIntermitente());
    }

    IEnumerator LanzaIntermitente()
    {
        Instantiate(bala, transform.position, Quaternion.Euler(transform.localEulerAngles)).GetComponent<Bullet>().SetBounce(0);
        fire.Play();
        yield return new WaitForSeconds(intermitencia);
        StartCoroutine(LanzaIntermitente());
    }
}
