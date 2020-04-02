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
    [SerializeField]
    private AudioSource laser;

    void Start()
    {
        laserRenderer = GetComponent<SpriteRenderer>();
        coll = GetComponent<BoxCollider2D>();
        laser = GetComponent<AudioSource>();
        gameObject.layer = LayerMask.NameToLayer(layer_);
        if (intermitencia) StartCoroutine(LaserIntermitente());
        else laser.Play();
    }

    IEnumerator LaserIntermitente()
    {
        coll.enabled = true;
        laserRenderer.enabled = true;
        laser.Play();
        yield return new WaitForSeconds(ratio);
        coll.enabled = false;
        laserRenderer.enabled = false;
        laser.Stop();
        yield return new WaitForSeconds(ratio);
        StartCoroutine(LaserIntermitente());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController detectorJugador = collision.GetComponent<PlayerController>();
        if (detectorJugador)
            laser.Stop();
            detectorJugador.Die();
    }
}
