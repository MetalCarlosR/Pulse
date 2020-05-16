using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recoger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(this.gameObject);
        GameManager.gmInstance_.AddAmmo(1);
        //Debug.Log("Munición +1");
    }
}
