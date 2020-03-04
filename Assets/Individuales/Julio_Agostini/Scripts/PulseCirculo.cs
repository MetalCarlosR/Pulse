using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulseCirculo : MonoBehaviour
{
    private Vector3 startingSize;
    private Vector3 scale;
    private float growth = 0.005f;
    // Start is called before the first frame update
    void Start()
    {
        scale = transform.localScale;
        StartCoroutine(Reset());
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = new Vector3(transform.localScale.x + growth, transform.localScale.y + growth, 1);
    }


    private IEnumerator Reset()
    {
        Debug.Log("got here");
        yield return new WaitForSeconds(2);
        transform.localScale = startingSize;
        StartCoroutine(Reset());
    }
}
