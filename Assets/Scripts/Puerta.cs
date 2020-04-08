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
        float rotaionBegin = transform.localEulerAngles.z;
        if (rotaionBegin == 0)
        {
            if (transform.position.x > player.transform.position.x) rotationEnd = rotaionBegin - 90;
            else rotationEnd = rotaionBegin + 90;
        }
        else if (rotaionBegin == 180 || rotaionBegin == -180)
        {
            if (transform.position.x > player.transform.position.x) rotationEnd = rotaionBegin + 90;
            else rotationEnd = rotaionBegin - 90;
        }
        else if(rotaionBegin == 90)
        {
            if (transform.position.y > player.transform.position.y) rotationEnd = rotaionBegin - 90;
            else rotationEnd = rotaionBegin + 90;
        }
        else
        {
            if (transform.position.y > player.transform.position.y) rotationEnd = rotaionBegin + 90;
            else rotationEnd = rotaionBegin - 90;
        }

        if (!open)
        {
            SoundManager.smInstance_.PlaySound(SoundManager.FXSounds.DOOR);
            StartCoroutine(DoorRotation(rotaionBegin, rotationEnd, 1f));
        }
    }

    IEnumerator DoorRotation(float begin, float end, float duration)
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
