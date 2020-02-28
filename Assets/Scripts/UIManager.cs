using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    GameObject RespawnUI;
    private void Start()
    {
        GameManager.gmInstance_.UIManager_ = this;
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
