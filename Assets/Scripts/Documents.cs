using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Documents : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>())
            if (GameManager.gmInstance_ != null)
                GameManager.gmInstance_.ChangeScene("FinishScene");
    }
}
