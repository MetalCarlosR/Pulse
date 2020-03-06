using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MunicionRecoger : MonoBehaviour
{
    int municion = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(other.gameObject);
        municion++;
        Debug.Log("Munición: " + municion);
    }
}
