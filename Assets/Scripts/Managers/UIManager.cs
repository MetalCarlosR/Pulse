using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    GameObject RespawnUI = null, pause = null, menu = null, interfaz = null, daga = null, pistola = null, pulse = null,
        background = null;
    [SerializeField]
    Text Ammunition = null;
 
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
        if (RespawnUI && pause)
        {

            interfaz.SetActive(true);
            RespawnUI.SetActive(false);
            pause.SetActive(false);
            menu.SetActive(false);
            background.SetActive(false);

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
            menu.SetActive(true);
            background.SetActive(true);
            interfaz.SetActive(false);
        }
        else
        {
            Debug.LogError("Warning RespawnUI is not set on " + this + "and can't be shown");
        }

    }
    public void OnPause()
    {
        Debug.Log("UI Pause menu");
        pause.SetActive(true);
        menu.SetActive(true);
        background.SetActive(true);
        interfaz.SetActive(false);
    }

    public void OnResume()
    {
        pause.SetActive(false);
        menu.SetActive(false);
        background.SetActive(false);
        interfaz.SetActive(true);
    }


    public void SetWeapon(bool activado)
    {
        if (daga && pistola)
        {
            daga.SetActive(activado);
            pistola.SetActive(!activado);
        }
    }

    public void ActivatePulse(bool activado)
    {
        pulse.SetActive(activado);
    }

    public void TurnInterface(bool on)
    {
        interfaz.SetActive(on);
    }

    public void SetAmmunition(int num)
    {
        Ammunition.text = "x " + num;
    }
}
