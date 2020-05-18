﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject fovPrefab = null, pulsePrefab = null, EnemigoPrefab = null, PlayerPrefab = null;

    private UIManager UIManager_;

    private GameObject FieldOfViewPool, PulsePool, player_, mueblesPadre;

    private SaveManager.GameSave saveGame = null;

    List<EnemigoManager> enemies;

    private Camera camara;
    public static GameManager gmInstance_;

    private int ammo_, startAmmo_ = 5;

    public bool paused = false, continueG = false, game = false, dead = false;

    private void Awake()
    {
        if (gmInstance_ == null)
        {
            gmInstance_ = this;
            Load();
            Debug.Log("GameManager Set");
        }
        else if (gmInstance_ != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this);
    }

    void Update()
    {
        if (game)
        {
            if (Input.GetKeyDown(KeyCode.Escape)) GameManager.gmInstance_.TogglePause();
            if (Input.GetKeyDown(KeyCode.L)) GameManager.gmInstance_.ReloadScene();
        }
    }

    public bool IsGameLoaded()
    {
        return saveGame != null;
    }
    void Load()
    {
        string loadString = SaveManager.Load();

        if (loadString != null)
        {
            Debug.Log("GameLoaded " + loadString);

            saveGame = JsonUtility.FromJson<SaveManager.GameSave>(loadString);
        }
    }

    void Save()
    {
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Nivel 1")) SaveManager.Save(1, ammo_, enemies, player_.transform.position);
        else SaveManager.Save(2, ammo_, enemies, player_.transform.position);
    }

    void loadFromSave()
    {
        ammo_ = saveGame.ammo_;
        Instantiate(PlayerPrefab, saveGame.playerPos_, Quaternion.identity);

        foreach (SaveManager.EnemySettings e in saveGame.enemies_)
        {
            EnemigoManager enemy = Instantiate(EnemigoPrefab, e.tr_, Quaternion.identity).GetComponent<EnemigoManager>();
            enemy.LoadEnemy(e.nodes_.nodes, e.nodes_.count, e.state, e.prevstate);

        }
    }

    public void Continue()
    {
        if (saveGame != null)
        {
            continueG = true;
            if (saveGame.level_ == 1) SceneManager.LoadScene("Nivel 1");
            else SceneManager.LoadScene("Nivel 2");
        }
    }
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        game = false;
        if (scene.name == "Menu")
        {
            Load();
            continueG = false;
        }
        else
        {
            game = true;
            dead = false;
            paused = false;
            enemies = new List<EnemigoManager>();
            FieldOfViewPool = new GameObject();
            PulsePool = new GameObject();
            FieldOfViewPool.name = "FieldOfViewPool";
            PulsePool.name = "PulsePool";
            if (continueG)
            {
                loadFromSave();
            }

            else ammo_ = startAmmo_;
        }

    }
    public void PlayerDeath()
    {
        Time.timeScale = 0;
        if (UIManager_ != null)
        {
            UIManager_.RespawnMenu();
            dead = true;
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
        SoundManager.smInstance_.PublicSetFxVolume(true);
    }

    public Transform GetPlayerTransform()
    {
        if (player_ != null)
        {
            return player_.transform;
        }
        else
        {
            Debug.Log("PlayerNull");
            return null;
        }
    }
    public void SetPlayer(GameObject player)
    {
        if (player.GetComponent<PlayerController>())
        {
            Debug.Log("PlayerSet");
            player_ = player;
        }
        else
        {
            Debug.LogWarning("No PlayerController Found on " + this);
        }
    }

    public Transform SetCamera(Camera cam)
    {
        camara = cam;
        if (player_) return player_.transform;
        else return null;
    }

    public Camera GetCamera()
    {
        return camara;
    }

    public void SetUImanager(UIManager UImanager)
    {
        UIManager_ = UImanager;
        gmInstance_.SetAmmunition();
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

    public void AddEnemy(EnemigoManager enemy)
    {
        enemies.Add(enemy);
    }
    public void RemoveEnemy(EnemigoManager enemy)
    {
        enemies.Remove(enemy);
    }

    public void setMuebles(GameObject muebles)
    {
        mueblesPadre = muebles;
        mueblesPadre.layer = LayerMask.NameToLayer("Muebles");
        foreach (Transform child in mueblesPadre.transform)
        {
            child.gameObject.layer = LayerMask.NameToLayer("Muebles");
        }
    }
    public void Shoot()
    {
        ammo_--;
        gmInstance_.SetAmmunition();
    }

    public void AddAmmo(int ammo)
    {
        ammo_ += ammo;
        gmInstance_.SetAmmunition();
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
            UIManager_.OnPause();
            paused = true;
            Time.timeScale = 0;
        }
    }

    public void ResumeGame()
    {
        if (paused)
        {
            UIManager_.OnResume();
            paused = false;
            Time.timeScale = 1;
        }
    }

    public void ChangeScene(string scene)
    {
        Time.timeScale = 1;
        if (scene == "Menu" && !dead) Save();
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

    public void SetAmmunition()
    {
        UIManager_.SetAmmunition(ammo_);
    }
}