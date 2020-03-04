using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    GameObject RespawnUI = null;
    [SerializeField]
    private GameObject pauseMenu;
    [SerializeField]
    private GameObject pauseButton;

    private void Start()
    {

        if (GameManager.gmInstance_ != null)
        {
            GameManager.gmInstance_.SetUImanager(this);
        }
        else
        {
            Debug.LogError("Warnign GameManager was null when trying to access it from " + this);
        }
        if (RespawnUI != null)
        {
            RespawnUI.SetActive(false);
        }
        else
        {
            Debug.LogWarning("Warning RespawnUI is not set on " + this);
        }

    }
    public void RespawnMenu()
    {
        if (RespawnUI != null)
        {
            RespawnUI.SetActive(true);
        }
        else
        {
            Debug.LogError("Warning RespawnUI is not set on " + this + "and can't be shown");
        }

    }

    public void Pause()
    {
        Debug.Log("UI Pause menu");
        pauseButton.SetActive(false);
        pauseMenu.SetActive(true);
    }

    public void Resume()
    {
        pauseButton.SetActive(true);
        pauseMenu.SetActive(false);
    }
}
