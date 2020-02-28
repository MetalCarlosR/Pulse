
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    private static GameObject player;
    private Vector3 startingPosition;
    public GameObject gameOverMenu;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        startingPosition = player.transform.position;
        

    }

    // Update is called once per frame
    void Update()
    {

    }


    public void Die()
    {

        player.GetComponent<PlayerControllerMal2>().Die();
        Time.timeScale = 0;
        gameOverMenu.SetActive(true);
        
    }

    public void Respawnear()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Respawn");
        /*
        player.transform.position = startingPosition;
        gameOverMenu.SetActive(false);
        */
    }



}