using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boton: MonoBehaviour
{
    [SerializeField]
    private List<GameObject> laser = new List<GameObject>();

    private AudioSource source;
    private void Start()
    {
        source = GetComponent<AudioSource>();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        PlayerController detectorJugador = collision.GetComponent<PlayerController>();
        if (detectorJugador && Input.GetKeyDown(KeyCode.E))
        {
            source.Play();
            foreach (GameObject obj in laser)
            {
                obj.SetActive(false);
            }
        }

    }
}
