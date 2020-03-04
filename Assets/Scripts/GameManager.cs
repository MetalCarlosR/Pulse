using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class GameManager : MonoBehaviour
{
    private GameObject player_;
    [SerializeField]
    private FieldOfView fovPrefab;
    [SerializeField]
    private PulseEnemigo pulsePrefab;

    private UIManager UIManager_;

    private GameObject FieldOfViewPool;
    private GameObject PulsePool;

    private Camera camara;
    public static GameManager gmInstance_;

    public enum Escenas
    {
        Prototipo,
        Menu
    }


    private void Awake()
    {
        
        if (gmInstance_ == null)
        {
            gmInstance_ = this;
            FieldOfViewPool = new GameObject();
            FieldOfViewPool.name = "FieldOfViewPool";
            PulsePool = new GameObject();
            PulsePool.name = "PulsePool";
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

    public FieldOfView createFieldofView()
    {
        FieldOfView newFov;
        newFov = Instantiate(fovPrefab.gameObject, FieldOfViewPool.transform).GetComponent<FieldOfView>();
        return newFov;
    }

    public PulseEnemigo createPulse()
    {
        PulseEnemigo newPulse;
        newPulse = Instantiate(pulsePrefab.gameObject, PulsePool.transform).GetComponent<PulseEnemigo>();
        return newPulse;
    }
    public void ChangeScene(string scene)
    {
        Escenas s;
        
        //Meter código defensivo
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
    }
    public void ExitGame()
    {
        Debug.Log("A la verga");
        Application.Quit();
    }
}