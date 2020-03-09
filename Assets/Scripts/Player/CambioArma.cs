using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CambioArma : MonoBehaviour
{
    [SerializeField]
    private Daga daga;
    [SerializeField]
    private Pistola pistola;
    void Start()
    {
        pistola.enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            daga.gameObject.SetActive(!daga.gameObject.activeSelf);
            daga.enabled = !daga.enabled;
            pistola.enabled = !pistola.enabled;

        }
    }
}
