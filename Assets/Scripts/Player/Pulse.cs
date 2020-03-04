using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pulse : MonoBehaviour
{
    private PlayerController player;
    private Pistola pistola;
    private CircleCollider2D coll;
    private int speed;
    private float ortSize, collSize;
    [SerializeField]
    private Camera cam;

    void Start()
    {
        player = GetComponent<PlayerController>();
        coll = GetComponent<CircleCollider2D>();
        pistola = GetComponent<Pistola>();
        collSize = coll.radius;
        if (GameManager.gmInstance_)
        {
            cam = GameManager.gmInstance_.GetCamera();
            ortSize = cam.orthographicSize;
        }
        else
        {
            Debug.LogError("Warning no GameManager found"); enabled = false; return;
        }
        if (player) speed = player.GetSpeed();
        else
        {
            Debug.LogError("Warning no PlayerController found on " + this); enabled = false; return;
        }
    }

    void Update()
    {
        if (cam)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                player.UsePulse(true);
                StopAllCoroutines();
                StartCoroutine(PulseCam(cam.orthographicSize, ortSize * 2f, coll.radius, 2f, (((ortSize * 2f) - cam.orthographicSize) / 5)));
            }
            else if (Input.GetKeyUp(KeyCode.Space))
            {
                player.UsePulse(false);
                StopAllCoroutines();
                StartCoroutine(PulseCam(cam.orthographicSize, ortSize, coll.radius, collSize, (cam.orthographicSize - ortSize) / 5));
            }
        }
        else
        {
            cam = GameManager.gmInstance_.GetCamera();
            ortSize = cam.orthographicSize;
        }
    }

    IEnumerator PulseCam(float beginCam, float endCam, float beginTrigger, float endTrigger, float duration)
    {
        float time = 0;
        while (time < duration)
        {
            cam.orthographicSize = Mathf.Lerp(beginCam, endCam, time / duration);
            coll.radius = Mathf.Lerp(beginTrigger, endTrigger, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        cam.orthographicSize = endCam;
        coll.radius = endTrigger;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemigo enemy = collision.GetComponent<Enemigo>();
        if (enemy) enemy.SetPulseState(true);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        Enemigo enemy = collision.GetComponent<Enemigo>();
        if (enemy) enemy.SetPulseState(false);
    }
}
