using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pulse : MonoBehaviour
{
    private PlayerController player;
    private Pistola pistola;
    private MeshRenderer mesh;
    private int speed;
    private float ortSize, beginSize, maxSize = 8.5f;
    private bool active = false;
    [SerializeField]
    private Camera cam;
    void Start()
    {
        player = GetComponentInParent<PlayerController>();
        pistola = GetComponentInParent<Pistola>();
        beginSize = transform.localScale.x;
        mesh = GetComponent<MeshRenderer>();
        if (GameManager.gmInstance_)
        {
            cam = GameManager.gmInstance_.GetCamera();
            if (cam) ortSize = cam.orthographicSize;
        }
        else
        {
            Debug.LogError("Warning no GameManager found"); enabled = false; return;
        }
        if (player && pistola && mesh)
        {
            speed = player.GetSpeed();
            mesh.enabled = false;
        }
        else
        {
            Debug.LogError("Warning no PlayerController or Gun found on " + this); enabled = false; return;
        }
    }

    void Update()
    {
        if (cam)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                player.UsePulse(true);
                SoundManager.smInstance_.PlaySound(SoundManager.FXSounds.PULSESTART);
                StopAllCoroutines();
                active = true;
                if (mesh.enabled == false) mesh.enabled = true;
                StartCoroutine(PulseCam(cam.orthographicSize, ortSize * 2f, transform.localScale.x, maxSize, (((ortSize * 2f) - cam.orthographicSize) / ortSize)));
            }
            else if (Input.GetKeyUp(KeyCode.Space))
            {
                StopAllCoroutines();
                SoundManager.smInstance_.StopSound(SoundManager.FXSounds.PULSESTART);
                float duration = (cam.orthographicSize - ortSize) / ortSize;
                SoundManager.smInstance_.StopPulseAtTime(1 - duration);
                active = false;
                StartCoroutine(PulseCam(cam.orthographicSize, ortSize, transform.localScale.x, beginSize, duration));
            }
        }
        else
        {
            cam = GameManager.gmInstance_.GetCamera();
            ortSize = cam.orthographicSize;
        }
    }

    IEnumerator PulseCam(float beginCam, float endCam, float beginSize, float endSize, float duration)
    {
        float time = 0;
        SoundManager.smInstance_.StopSound(SoundManager.FXSounds.PULSEMID);
        while (time < duration)
        {
            float size = Mathf.Lerp(beginSize, endSize, time / duration); ;
            cam.orthographicSize = Mathf.Lerp(beginCam, endCam, time / duration);
            transform.localScale = new Vector2(size, size);
            time += Time.deltaTime;
            yield return null;
        }
        if (endSize == maxSize) SoundManager.smInstance_.PlaySound(SoundManager.FXSounds.PULSEMID);
        if (endSize == this.beginSize)
        {
            mesh.enabled = false;
            player.UsePulse(false);
        }

        cam.orthographicSize = endCam;
        transform.localScale = new Vector2(endSize, endSize);


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemigo enemy = collision.GetComponent<Enemigo>();
        if (enemy && active) enemy.SetPulseState(true);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        Enemigo enemy = collision.GetComponent<Enemigo>();
        if (enemy) enemy.SetPulseState(false);
    }
}
