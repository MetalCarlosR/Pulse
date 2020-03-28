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

    IEnumerator LaserIntermitente()
    {
        coll.enabled = true;
        laserRenderer.enabled = true;
        SoundManager.smInstance_.PlaySound(SoundManager.Audio.LASER);
        yield return new WaitForSeconds(ratio);
        coll.enabled = false;
        laserRenderer.enabled = false;
        SoundManager.smInstance_.StopSound(SoundManager.Audio.LASER);
        yield return new WaitForSeconds(ratio);
        StartCoroutine(LaserIntermitente());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController detectorJugador = collision.GetComponent<PlayerController>();
        if (detectorJugador)
            detectorJugador.Die();
    }
}
