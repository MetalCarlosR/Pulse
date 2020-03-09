using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CambioArma : MonoBehaviour
{
    public Daga daga;
    public Pistola pistola;
    // Start is called before the first frame update
    void Start()
    {
        daga = GetComponent<Daga>();
        pistola = GetComponent<Pistola>();
        pistola.enabled = !pistola.enabled;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C) && daga.enabled)
        {
            daga.enabled = !daga.enabled;
            pistola.enabled = !pistola.enabled;

        }
        else if (Input.GetKeyDown(KeyCode.C) && !daga.enabled)
        {
            daga.enabled = !daga.enabled;
            pistola.enabled = !pistola.enabled;
        }
    }
}
