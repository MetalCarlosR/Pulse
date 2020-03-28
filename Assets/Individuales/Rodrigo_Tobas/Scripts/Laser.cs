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
    private SpriteRenderer laserRenderer;
    [SerializeField]
    private BoxCollider2D coll;
    [SerializeField]
    private PlayerController player;

    void Start()
    {
        laserRenderer = GetComponent<SpriteRenderer>();
        coll = GetComponent<BoxCollider2D>();
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
            laserRenderer.enabled = !(laserRenderer.enabled);
            yield return new WaitForSeconds(ratio);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController detectorJugador = collision.GetComponent<PlayerController>();
        if (detectorJugador)
            detectorJugador.Die();
    }
}
