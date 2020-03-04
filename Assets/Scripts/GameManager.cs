using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private GameObject player_;

    private UIManager UIManager_;

    private Camera camara;
    public static GameManager gmInstance_;



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

    public void PauseGame()
    {
        Debug.Log("Pausing");
        Time.timeScale = 0;
        UIManager_.Pause();
        player_.GetComponent<PlayerController>().Activate(false);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        UIManager_.Resume();
        player_.GetComponent<PlayerController>().Activate(true);
    }
}