using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulseCirculo : MonoBehaviour
{


    private Transform circle;
    private Vector3 startingSize = new Vector3(0.25f, 0.25f, 1);
    private Vector3 scale;
    private float growth = 0.005f;
    // Start is called before the first frame update
    void Start()
    {
        circle = gameObject.transform;
        scale = new Vector3();
        StartCoroutine("Reset");
    }

    // Update is called once per frame
    void Update()
    {
        circle.localScale = new Vector3(circle.localScale.x + growth, circle.localScale.y + growth, 1);
    }


    private IEnumerator Reset()
    {
        Debug.Log("got here");
        yield return new WaitForSeconds(2);
        circle.localScale = startingSize;
        StartCoroutine("Reset");
    }
}
