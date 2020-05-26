using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Documents : MonoBehaviour
{
    private GameObject text;

    private void Start()
    {
        text = transform.GetChild(0).gameObject;
        text.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>()) text.SetActive(true);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (GameManager.gmInstance_ != null && Input.GetButtonDown("Use") && collision.gameObject.GetComponent<PlayerController>())
        {
            GameManager.gmInstance_.ChangeScene("Nivel 2");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>()) text.SetActive(false);
    }
}
