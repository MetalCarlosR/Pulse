using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject fovPrefab = null, pulsePrefab = null;

    private UIManager UIManager_;

    private GameObject FieldOfViewPool, PulsePool, player_;

    List<GameObject> entities;
    private Camera camara;
    public static GameManager gmInstance_;

    private int ammo_ = 5, startAmmo_;

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
        entities = new List<GameObject>();
        FieldOfViewPool = new GameObject();
        PulsePool = new GameObject();
        FieldOfViewPool.name = "FieldOfViewPool";
        PulsePool.name = "PulsePool";
        startAmmo_ = ammo_;
       
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
        ammo_ = startAmmo_;
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
        gmInstance_.SetAmmunition(ammo_);
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

    public void SetEscenario(Transform escenario)
    {
        FieldOfViewPool.transform.parent = escenario;
        PulsePool.transform.parent = escenario;
    }

    public void AddEntity(GameObject entity)
    {
        entities.Add(entity);
    }
    public void RemoveEntity(GameObject entity)
    {
        entities.Remove(entity);
    }

    public void Shoot()
    {
        ammo_--;
        gmInstance_.SetAmmunition(ammo_);
    }

    public bool EmptyGun()
    {
        return ammo_ <= 0;
    }
    public void TogglePause()
    {
        if (paused) ResumeGame();
        else PauseGame();
    }
    public void PauseGame()
    {
        if (!paused)
        {
            paused = true;
            Time.timeScale = 0;
            foreach (GameObject obj in entities)
            {
                obj.SendMessage("OnPause");
            }            
        }
    }

    public void ResumeGame()
    {
        if (paused)
        {
            paused = false;
            Time.timeScale = 1;
            Debug.Log("B");
            foreach (GameObject obj in entities)
            {
                obj.SendMessage("OnResume");
            }
        }
    }

    public void ChangeScene(string scene)
    {
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void SetWeapon(bool activado)
    {
        UIManager_.SetWeapon(activado);
    }

    public void ActivatePulse(bool activado)
    {
        UIManager_.ActivatePulse(activado);
    }

    public void TurnInterface(bool on)
    {
        UIManager_.TurnInterface(on);
    }

    public void SetAmmunition(int num)
    {
        UIManager_.SetAmmunition(num);
    }
}