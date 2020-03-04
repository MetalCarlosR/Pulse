﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject fovPrefab = null, pulsePrefab = null;

    private UIManager UIManager_;

    private GameObject FieldOfViewPool , PulsePool, player_;

    private Camera camara;
    public static GameManager gmInstance_;


    bool paused = false;

    private void Awake()
    {

        if (gmInstance_ == null)
        {
            gmInstance_ = this;
            Debug.Log("GameManager Set");
        }
        else if (gmInstance_ != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this);
    }
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        FieldOfViewPool = new GameObject();
        PulsePool = new GameObject();
        FieldOfViewPool.name = "FieldOfViewPool";
        PulsePool.name = "PulsePool";
    }
    public void PlayerDeath()
    {
        Time.timeScale = 0;
        if (UIManager_ != null)
        {
            UIManager_.RespawnMenu();
        }
        else
        {
            Debug.LogError("No UIManager_ set on the GameManager");
        }
    }

    public void ReloadScene()
    {
        Time.timeScale = 1;
        Debug.Log(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }

    public Transform GetPlayerTransform()
    {
        if (player_ != null)
        {
            return player_.transform;
        }
        else
        {
            return null;
        }

    }
    public void SetPlayer(GameObject player)
    {
        if (player.GetComponent<PlayerController>())
        {
            player_ = player;
        }
        else
        {
            Debug.LogWarning("No PlayerController Found on " + this);
        }
    }

    public void SetCamera(Camera cam)
    {
        camara = cam;
    }

    public Camera GetCamera()
    {
        return camara;
    }

    public void SetUImanager(UIManager UImanager)
    {
        UIManager_ = UImanager;
    }


    public FieldOfView createFieldofView()
    {
        FieldOfView newFov;
        newFov = Instantiate(fovPrefab, FieldOfViewPool.transform).GetComponent<FieldOfView>();
        return newFov;
    }

    public PulseEnemigo createPulse()
    {
        PulseEnemigo newPulse;
        newPulse = Instantiate(pulsePrefab, PulsePool.transform).GetComponent<PulseEnemigo>();
        return newPulse;
    }

    public void PauseGame()
    {
        if (!paused)
        {
            Time.timeScale = 0;
            UIManager_.Pause();
            player_.GetComponent<PlayerController>().CanShoot(false);
            paused = true;
        }

    }

    public void ResumeGame()
    {
        if (paused)
        {
            Time.timeScale = 1;
            UIManager_.Resume();
            player_.GetComponent<PlayerController>().CanShoot(true);
            paused = false;
        }
    }

    public void ChangeScene(string scene)
    {
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
    }
    public void ExitGame()
    {
        Debug.Log("A la verga");
        Application.Quit();
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}