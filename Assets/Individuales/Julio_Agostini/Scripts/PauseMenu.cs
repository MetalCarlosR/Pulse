﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{

    [SerializeField]
    private GameObject pauseMenu;
    [SerializeField]
    private GameObject pauseButton;
    [SerializeField]
    private GameObject player;


    public void PauseGame()
    {
        Time.timeScale = 0;
        pauseButton.SetActive(false);
        pauseMenu.SetActive(true);
        Pistola pistola = player.GetComponent<Pistola>();
        if (pistola)
        {
            pistola.enabled = false;
        }
        player.GetComponent<PlayerController>().enabled = false;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        pauseButton.SetActive(true);
        pauseMenu.SetActive(false);
        Pistola pistola = player.GetComponent<Pistola>();
        if (pistola)
        {
            pistola.enabled = true;
        }
        player.GetComponent<PlayerController>().enabled = true;
    }
}
