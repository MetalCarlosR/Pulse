using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puerta : MonoBehaviour
{
    bool jugador, open;

    private float rot;

    private void Start()
    {
        open = false;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKeyDown(KeyCode.E) && collision.GetComponent<PlayerController>() && !open)
        {
            MovPuerta(collision.transform);
        }
    }

    void MovPuerta(Transform player)
    {
        float rotationEnd = 0;
        if (transform.rotation.z == 0 || transform.rotation.z == 180)
        {
            if (transform.position.x > player.transform.position.x) rotationEnd = transform.rotation.z - 90;
            else rotationEnd = transform.rotation.z + 90;
        }
        else
        {
            if (transform.position.y > player.transform.position.y) rotationEnd = transform.rotation.z - 90;
            else rotationEnd = transform.rotation.z + 90;
        }

        if (!open) StartCoroutine(CameraChange(transform.rotation.z, rotationEnd, 1f));
    }

    IEnumerator CameraChange(float begin, float end, float duration)
    {
        float time = 0;
        open = !open;
        while (time < duration)
        {
            transform.rotation = Quaternion.Euler(0, 0, Mathf.Lerp(begin, end, time / duration));
            time += Time.deltaTime;

            yield return null;
        }
        transform.rotation = Quaternion.Euler(0, 0, end);
    }
}
