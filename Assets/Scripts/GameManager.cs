using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private GameObject player_;

    public UIManager UIManager_
    {
        private get { return UIManager_; }
        set { UIManager_ = value; }
    }

    private Camera camara;
    public static GameManager gmInstance_ { get; private set; }

    private void Awake()
    {
        if (gmInstance_ == null)
        {
            gmInstance_ = this;
            DontDestroyOnLoad(this);
            Debug.Log("GameManager Set");
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

    public void SetCamera(Camera cam)
    {
        camara = cam;
    }

    public Camera GetCamera()
    {
        return camara;
    }
}