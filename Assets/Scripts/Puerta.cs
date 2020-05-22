using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puerta : MonoBehaviour
{
    bool jugador, open = false;
    private AudioSource doorSound;
    private Vector3 endRot;

    public bool GetOpen()
    {
        return open;
    }

    private void Start()
    {
        doorSound = GetComponent<AudioSource>();
        if (GameManager.gmInstance_)
        {
            GameManager.gmInstance_.AddDoor(this);
        }
    }

    public Vector3 GetEndRot()
    {
        return endRot;
    }
    public void SetPuerta(bool open_)
    {
        open = open_;
    }

    public void MovPuerta(Transform entity)
    {
        float rotationEnd = 0;
        float rotaionBegin = transform.localEulerAngles.z;
        if (rotaionBegin == 0)
        {
            if (transform.position.x > entity.transform.position.x) rotationEnd = rotaionBegin - 90;
            else rotationEnd = rotaionBegin + 90;
        }
        else if (rotaionBegin == 180 || rotaionBegin == -180)
        {
            if (transform.position.x > entity.transform.position.x) rotationEnd = rotaionBegin + 90;
            else rotationEnd = rotaionBegin - 90;
        }
        else if (rotaionBegin == 90)
        {
            if (transform.position.y > entity.transform.position.y) rotationEnd = rotaionBegin - 90;
            else rotationEnd = rotaionBegin + 90;
        }
        else
        {
            if (transform.position.y > entity.transform.position.y) rotationEnd = rotaionBegin + 90;
            else rotationEnd = rotaionBegin - 90;
        }

        if (!open)
        {
            doorSound.Play();
            StartCoroutine(DoorRotation(rotaionBegin, rotationEnd, 1f));
            endRot = new Vector3(0, 0, rotationEnd);
            open = !open;
        }
    }

    IEnumerator DoorRotation(float begin, float end, float duration)
    {
        float time = 0;
        
        while (time < duration)
        {
            transform.rotation = Quaternion.Euler(0, 0, Mathf.Lerp(begin, end, time / duration));
            time += Time.deltaTime;

            yield return null;
        }
        transform.rotation = Quaternion.Euler(0, 0, end);
    }

    private void OnDestroy()
    {
        GameManager.gmInstance_.RemoveDoor(this);
    }
}
