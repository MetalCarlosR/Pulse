﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadManager : MonoBehaviour
{
    [SerializeField]
    private Image LoadingBar = null, Bullet = null;
    [SerializeField]
    private Text Progress = null, Resumen = null, LevelName = null;
    [SerializeField]
    Image loaderImage = null;
    [SerializeField]
    Sprite lvl1 = null, lvl2 = null;
    [SerializeField]
    GameObject loading = null;

    private Button ContinueButton = null;

    private const int BulletStart = -640, BulletEnd = 685;

    private const string lvl1Description = "Has seguido a varios miembros de la mafia hasta un narcopiso. " +
        "Te has infiltrado dentro donde esperas encontrar algo de información que te ayude a encontrar a tu hija";
    private const string lvl2Description = "La información que conseguistes señala esta mansión como la residencia del jefe y el centro de operaciones de la mafia. " +
        "Te dispones a entrar, esperando encontrar a tu hija y hacer pagar al hombre que la secuestró";

    void Start()
    {
        if (GameManager.gmInstance_)
        {
            ContinueButton = loading.GetComponent<Button>();
            ContinueButton.onClick.AddListener(delegate { GameManager.gmInstance_.EndLevelChange(); });
            ContinueButton.enabled = false;
            SetLevel(GameManager.gmInstance_.SetLoadingManager(this));
        }
    }

    void SetLevel(string Level)
    {
        if (Level == "Nivel 1")
        {
            LevelName.text = "NarcoPiso";
            Resumen.text = lvl1Description;
            loaderImage.sprite = lvl1;
        }
        else
        {
            LevelName.text = "Mansión";
            Resumen.text = lvl2Description;
            Resumen.fontSize = 250;
            loaderImage.sprite = lvl2;
        }

    }

    public void ChangeProgress(float progress)
    {
        if (progress == 1)
        {
            ContinueButton.enabled = true;
            loading.GetComponent<Text>().text = "CONTINUE";
        } 
        LoadingBar.fillAmount = progress;
        Progress.text = (int)(progress * 100) + "%";
        float position = Mathf.Lerp(BulletStart, BulletEnd, progress);
        Bullet.rectTransform.anchoredPosition = new Vector2(position, -395);
    }
}
