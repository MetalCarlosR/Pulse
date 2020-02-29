using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PulseMal : MonoBehaviour
{
    private PlayerControllerMal player;
    private int speed;
    private float ortSize;
    private PostProcessLayer post;

    [SerializeField]
    private Camera cam = null;

    void Start()
    {
        if (GetComponent<PlayerControllerMal>() && cam != null)
        {
            player = GetComponent<PlayerControllerMal>();
            speed = player.speed;
            ortSize = cam.orthographicSize;
            if (cam.GetComponent<PostProcessLayer>())
                post = cam.GetComponent<PostProcessLayer>();
            else
            {
                Debug.LogError("Warning no PostProcessLayer found in " + cam);
                post = null;
            }  
        }
        else
        {
            Debug.LogError("Warning non PlayerController or Camera found on " + this);
            GetComponent<PulseMal>().enabled = false;
        }
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            player.speed = 0;
            StopAllCoroutines();
            StartCoroutine(CameraChange(cam.orthographicSize, ortSize * 2, ((ortSize * 2) - cam.orthographicSize) / 5));
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            player.speed = speed;
            StopAllCoroutines();
            StartCoroutine(CameraChange(cam.orthographicSize, ortSize, (cam.orthographicSize - ortSize) / 5));
        }
    }

    IEnumerator CameraChange(float begin, float end, float duration)
    {
        float time = 0;
        if (post != null)
        {
            post.enabled = !post.enabled;
        }
        while (time < duration) {
            cam.orthographicSize = Mathf.Lerp(begin, end, time/duration);
            time += Time.deltaTime;
            yield return null;
        }
        cam.orthographicSize = end;
    }
}
