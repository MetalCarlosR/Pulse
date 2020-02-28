using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Daga : MonoBehaviour
{
    private IEnumerator coroutine;
    private void Start()
    {
        
    }
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            transform.position += transform.right * 0.3f;
        }
        else if (Input.GetKeyUp("space"))
        {
            transform.position -= transform.right * 0.3f;
        }
    }

    //IEnumerator Apunalamiento(float begin, float end, float duration)
    //{
    //    float time = 0; 
    //    while (time<duration)
    //    {
    //        transform.position = transform.right* Mathf.Lerp(begin, end, time / duration);
    //        time += Time.deltaTime;
    //        yield return null;
    //    }        
    //}
}
