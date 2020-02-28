
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private GameObject player_;

    [SerializeField]
    private GameObject gameOverMenu_;

    public static GameManager gmInstance_ { get; private set; }

    private void Awake()
    {
        if (gmInstance_ == null)
        {
            gmInstance_ = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Debug.LogWarning("More than one Instance of GameManager on Scene deleting " + this);
            Destroy(this.gameObject);
        }
    }

    public void PlayerDeath()
    {
        Time.timeScale = 0;
        gameOverMenu_.SetActive(true);
    }

    public void ReloadScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public Transform GetPlayerTransform()
    {
        return player_.transform;
    }
    public void SetPlayer(GameObject player) {
        if (player.GetComponent<PlayerControllerMal>())
        {
            player_ = player;
        }
        else
        {
            Debug.LogWarning("No PlayerController Found on " + this);
        }
    }
}