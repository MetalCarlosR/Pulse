using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pulse : MonoBehaviour
{
    private PlayerController player;
    private int speed;
    private float ortSize;
    private Camera cam;

    void Start()
    {
        player = GetComponent<PlayerController>();
        if (GameManager.gmInstance_) cam = GameManager.gmInstance_.GetCamera();
        else
        {
            Debug.LogError("Warning no GameManager found"); enabled = false; return;
        }
        if (player) speed = player.speed;
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
                player.speed = 0;
                StopAllCoroutines();
                StartCoroutine(PulseH(cam.orthographicSize, ortSize * 2, (((ortSize * 2) - cam.orthographicSize) / 5) * 3));
            }
            else if (Input.GetKeyUp(KeyCode.Space))
            {
                player.speed = speed;
                StopAllCoroutines();
                StartCoroutine(PulseH(cam.orthographicSize, ortSize, (cam.orthographicSize - ortSize) / 5));
            }
        }
        else
        {
            cam = GameManager.gmInstance_.GetCamera();
            //ortSize = cam.orthographicSize;
        }
    }

    IEnumerator PulseH(float begin, float end, float duration)
    {
        float time = 0;
        while (time < duration)
        {
            cam.orthographicSize = Mathf.Lerp(begin, end, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        cam.orthographicSize = end;
    }
}
