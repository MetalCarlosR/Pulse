using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    GameObject RespawnUI = null;
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
}
