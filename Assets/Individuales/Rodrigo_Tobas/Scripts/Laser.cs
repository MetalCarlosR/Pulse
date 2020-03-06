using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private string layer_ = "";
    [SerializeField]
    private bool intermitencia = false;
    [SerializeField]
    private float ratio = 2f;
    [SerializeField]
    private SpriteRenderer renderer;
    [SerializeField]
    private BoxCollider2D coll;
    
    void Start()
    {
        gameObject.layer = LayerMask.NameToLayer(layer_);
        if (intermitencia) StartCoroutine(LaserIntermitente());
        
    }
   
   
    void Update()
    {

    }
    IEnumerator LaserIntermitente()
    {
        while (true)
        {
            coll.enabled = !(coll.enabled);
            renderer.enabled = !(renderer.enabled);
            yield return new WaitForSeconds(ratio);
            Debug.Log("a");
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController detectorJugador = collision.GetComponent<PlayerController>();
        if (detectorJugador)
            detectorJugador.Die();
    }
}
