using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private string layer_ = "";
    [SerializeField]
    private bool intermitencia;
    void Start()
    {
        gameObject.layer = LayerMask.NameToLayer(layer_);
        if (intermitencia == true)
            StartCoroutine(LaserIntermitente(this.gameObject.GetComponent<SpriteRenderer>()));
    }
   
   
    void Update()
    {
        
    }
    IEnumerator LaserIntermitente(SpriteRenderer visible)
    {
        visible.enabled = !visible.enabled;
        yield return new WaitForSeconds(2);
        visible.enabled = !visible.enabled;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController detectorJugador = collision.gameObject.GetComponent<PlayerController>();
        if (detectorJugador)
            detectorJugador.Die();
    }
}
