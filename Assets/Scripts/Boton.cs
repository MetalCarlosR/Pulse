using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boton: MonoBehaviour
{
    [SerializeField]
    private List<GameObject> laser = new List<GameObject>();
    private void OnTriggerStay2D(Collider2D collision)
    {
        PlayerController detectorJugador = collision.GetComponent<PlayerController>();
        if (detectorJugador && Input.GetKeyDown(KeyCode.E))
        {
            SoundManager.smInstance_.PlaySound(SoundManager.FXSounds.LASERSWITCH , transform.position);
            foreach (GameObject obj in laser)
            {
                obj.SetActive(false);
            }
        }

    }
}
