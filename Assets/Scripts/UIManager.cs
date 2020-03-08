using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    GameObject RespawnUI = null, pauseMenu = null;
    private void Start()
    {
        if (GameManager.gmInstance_ != null)
        {
            GameManager.gmInstance_.SetUImanager(this);
            GameManager.gmInstance_.AddEntity(gameObject);
        }
        else
        {
            Debug.LogError("Warnign GameManager was null when trying to access it from " + this);
        }
        if (RespawnUI  && pauseMenu)
        {
            RespawnUI.SetActive(false);
            pauseMenu.SetActive(false);
        }
        else
        {
            Debug.LogWarning("Warning RespawnUI or Pause is not set on " + this);
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

    public void OnPause()
    {
        Debug.Log("UI Pause menu");
        pauseMenu.SetActive(true);
    }

    public void OnResume()
    {
        pauseMenu.SetActive(false);
    }
}
