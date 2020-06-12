using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Creditos : MonoBehaviour
{
    [SerializeField]
    private Text creditos;
    [SerializeField]
    private GameObject menu;
    private float posY, posYEnd;
    bool submit = false;
    private void Start()
    {
        posY = creditos.rectTransform.anchoredPosition.y;
        posYEnd = -posY;
        Debug.Log(posYEnd);
    }
    private void Update()
    {
        if (Input.GetButtonDown("Fire1") || Input.GetButtonDown("Submit") || posY == posYEnd)
        {
            submit = true;
            creditos.rectTransform.anchoredPosition = new Vector2(0, posYEnd);
            menu.SetActive(true);
            if (Input.GetButtonDown("Submit") && submit)
                GameManager.gmInstance_.ChangeScene("Menu");
        }
        else if (!submit)
        {
            creditos.rectTransform.anchoredPosition = new Vector2(0, posY);
            posY += 1f;
        }
    }
}
