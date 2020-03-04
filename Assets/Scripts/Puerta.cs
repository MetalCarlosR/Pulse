﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puerta : MonoBehaviour
{
    bool jugador, open;

    private float rot;

    private void Start()
    {
        open = false;
        jugador = false;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && jugador && !open)
        {
            MovPuerta();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>()) jugador = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>()) jugador = false;

    }

    void MovPuerta()
    {
        StopAllCoroutines();
        if (!open) StartCoroutine(CameraChange(transform.rotation.z, transform.rotation.z + 90, 1f));
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