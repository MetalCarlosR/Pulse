using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MathFlerp : MonoBehaviour
{
    [SerializeField]
    private Transform Cube1;
    [SerializeField]
    private Transform Cube2;
    [Range(0f, 1f)]
    private float lerp=0;

    
    void Update()
    {
        transform.position = Vector3.Lerp(Cube1.position, Cube2.position, lerp);
        lerp += 0.5f * Time.deltaTime;
        transform.rotation = Quaternion.Lerp(Cube1.rotation, Cube2.rotation, lerp);
        if (lerp > 1)
        {
            Transform aux = Cube1;
            Cube1 = Cube2;
            Cube2 = aux;
            lerp = 0;
        }
    }
}
