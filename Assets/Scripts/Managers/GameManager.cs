using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class GameManager : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField]
    private GameObject fovPrefab = null, pulsePrefab = null, EnemigoPrefab = null, PlayerPrefab = null, PuertaPrefab = null, BotonPrefab = null, LaserPrefab = null;

    private UIManager UIManager_;

    private LoadManager loadManager_;

    private GameObject FieldOfViewPool, PulsePool, player_, mueblesPadre, EnemiesPool, NodesPool, PuertaPool, BotonesPool, LaserPool;

    private Pistola scriptPistola;

    private PlayerController scriptPC;

    private SaveManager.GameSave saveGame = null;

    [Header("Save")]
    public bool save = false;
    [Header("Json")]
    [SerializeField]
    private TextAsset jsonlvl1 = null, jsonlvl2 = null;

    List<EnemigoManager> enemies = new List<EnemigoManager>();

    List<Puerta> puertas_ = new List<Puerta>();

    List<Boton> botones_ = new List<Boton>();

    private Camera camara;

    public static GameManager gmInstance_;

    private int ammo_, startAmmo_ = 5;

    private bool paused = false, continueG = false, game = false, dead = false , daga = true;

    private string currentScene , NexLevelLoading;

    [Header("Cheats")]
    [SerializeField]
    private Transform endPoint;
    public bool TGM = false, Uammo = false;
    private void Awake()
    {
        if (gmInstance_ == null)
        {
            gmInstance_ = this;
            LoadSave();
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
            if (Input.GetButtonDown("Pause")) GameManager.gmInstance_.TogglePause();
            if (Input.GetButtonDown("Reload")) GameManager.gmInstance_.ReloadScene();
            if (SettingsManager.smInstance_.GetSettings().cheats_)
            {
                if (Input.GetKeyDown(KeyCode.F1)) Cheats(1);
                if (Input.GetKeyDown(KeyCode.F2)) Cheats(2);
                if (Input.GetKeyDown(KeyCode.F3)) Cheats(3);
                if (Input.GetKeyDown(KeyCode.F4)) Cheats(4);
                if (Input.GetKeyDown(KeyCode.F5)) Cheats(5);
            }
        }
    }

    void Cheats(int code)
    {
        SoundManager.smInstance_.PlayCLip(SoundManager.FXSounds.PULSE_END);
        switch (code)
        {
            case 1:
                player_.transform.position = endPoint.position;
                break;
            case 2:
                foreach (EnemigoManager e in enemies) e.Death();
                break;
            case 3:
                Collider2D[] coll = Physics2D.OverlapCircleAll(player_.transform.position, 100);

                foreach (Collider2D col in coll)
                {
                    Enemigo enemy = col.GetComponent<Enemigo>();
                    if (enemy && enemy.GetState() != Enemigo.State.Atacking)
                    {
                        enemy.SetState(Enemigo.State.Alerted);
                    }
                }
                break;
            case 4:
                TGM = !TGM;
                break;
            case 5:
                Uammo = !Uammo;
                break;
        }

    }

    public void SetEnd(Transform end)
    {
        endPoint = end;
    }
    public bool IsGamePaused()
    {
        return paused;
    }
    public bool IsGameLoaded()
    {
        return saveGame != null;
    }


    // CARGA Y GUARDADO DEL JUEGO

    void LoadSave()
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
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Nivel 1")) SaveManager.Save(1, ammo_, enemies, player_.transform.position, puertas_, botones_);
        else SaveManager.Save(2, ammo_, enemies, player_.transform.position, puertas_, botones_);
    }

    void chooseLvl(string lvl)
    {
        SaveManager.GameSave lvlLoader = new SaveManager.GameSave();

        if (lvl == "Nivel 1") lvlLoader = JsonUtility.FromJson<SaveManager.GameSave>(jsonlvl1.text);
        else lvlLoader = JsonUtility.FromJson<SaveManager.GameSave>(jsonlvl2.text);

        loadLvl(lvlLoader);
    }

    void loadLvl(SaveManager.GameSave lvlLoader_)
    {

        ammo_ = lvlLoader_.ammo_;
        Instantiate(PlayerPrefab, lvlLoader_.playerPos_, Quaternion.identity);

        int i = 0;
        foreach (SaveManager.EnemySettings e in lvlLoader_.enemies_)
        {
            EnemigoManager enemy = Instantiate(EnemigoPrefab, e.tr_, Quaternion.identity).GetComponent<EnemigoManager>();
            enemy.transform.parent = EnemiesPool.transform;
            enemy.name = "Enemigo" + i;
            enemy.LoadEnemy(e.nodes_.nodes, e.nodes_.count, e.state, e.prevstate);
            i++;
        }
        i = 0;
        foreach (SaveManager.PuertasStates p in lvlLoader_.puertas_)
        {
            Puerta puerta = Instantiate(PuertaPrefab, p.position_, Quaternion.Euler(p.rotation_)).GetComponent<Puerta>();
            puerta.transform.parent = PuertaPool.transform;
            puerta.name = "Puerta" + i;
            puerta.SetPuerta(p.open_);
            i++;
        }
        i = 0;
        foreach (SaveManager.BotonStates b in lvlLoader_.botones_)
        {
            Boton boton = Instantiate(BotonPrefab, b.position_, Quaternion.identity).GetComponent<Boton>();
            boton.transform.parent = BotonesPool.transform;
            boton.name = "Boton" + i;
            i++;

            if (b.active)
            {
                List<GameObject> LaserIn = new List<GameObject>();
                foreach (SaveManager.LaserTr l in b.laser)
                {
                    GameObject laser = Instantiate(LaserPrefab, l.positon_, Quaternion.Euler(l.rotation_));
                    laser.GetComponent<Laser>().SetIntermitencia(l.intermitencia_);
                    LaserIn.Add(laser);
                }
                boton.SetButton(LaserIn, true);
            }
            else boton.SetButton(null, false);
        }
    }

    /* -------------------- */


    // MANEJO DE ESCENAS 

    public void Continue()
    {
        if (saveGame != null)
        {
            continueG = true;
            if (saveGame.level_ == 1) SceneManager.LoadScene("Nivel 1");
            else SceneManager.LoadScene("Nivel 2");
        }
    }

    public void ReloadScene()
    {
        Time.timeScale = 1;
        Debug.Log(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        SoundManager.smInstance_.PublicSetFxVolume(true);
    }

    void OnEnable()
    {
        SceneManager.activeSceneChanged += ChangedActiveScene;
    }


    private void ChangedActiveScene(Scene current, Scene next)
    {
        game = false;
        dead = true;
        Debug.Log(next.name);
        if (next.name == "Menu")
        {
            LoadSave();
            continueG = false;
        }
        else if (next.name == "Nivel 1" || next.name == "Nivel 2")
        {
            game = true;
            dead = false;
            paused = false;
            enemies = new List<EnemigoManager>();
            FieldOfViewPool = new GameObject();
            PulsePool = new GameObject();
            EnemiesPool = new GameObject();
            BotonesPool = new GameObject();
            NodesPool = new GameObject();
            PuertaPool = new GameObject();
            LaserPool = new GameObject();
            FieldOfViewPool.name = "FieldOfViewPool";
            PulsePool.name = "PulsePool";
            EnemiesPool.name = "EnemiesPool";
            NodesPool.name = "NodesPool";
            PuertaPool.name = "PuertaPool";
            BotonesPool.name = "BotonesPool";
            LaserPool.name = "LaserPool";
            ammo_ = startAmmo_;
            if (save)
            {
                if (continueG) loadLvl(saveGame);
                else chooseLvl(next.name);
            }

        }
        currentScene = next.name;
    }

    IEnumerator LoadLevel(string scene)
    {
        NexLevelLoading = scene;

        SceneManager.LoadScene("LoadingScreen");

        yield return new WaitForSeconds(1);

        float gameLoad = 0;
        while (gameLoad < 1)
        {
            loadManager_.ChangeProgress(gameLoad);
            yield return new WaitForEndOfFrame();
            gameLoad += 0.01f;
        }
        loadManager_.ChangeProgress(1);
    }

    public void EndLevelChange()
    {
        SceneManager.LoadSceneAsync(NexLevelLoading);
    }

    public void EndGame()
    {
        SceneManager.LoadSceneAsync("Menu");
    }

    public void ChangeScene(string scene)
    {
        Time.timeScale = 1;
        if (scene == "Menu" && !dead) Save();
        else if (scene == "Nivel 1" || scene == "Nivel 2")
        {
            StartCoroutine(LoadLevel(scene));
            return;
        }
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    private void OnDisable()
    {
        SceneManager.activeSceneChanged -= ChangedActiveScene;
    }

    /* -------------------- */


    // SETTERS Y GETTERS DE LOS OBJETOS EN ESCENA

    // ---- PLAYER
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
            Debug.Log("PlayerSet");
            player_ = player;
            scriptPistola = player_.GetComponent<Pistola>();
            scriptPC = player_.GetComponent<PlayerController>();
        }
        else
        {
            Debug.LogWarning("No PlayerController Found on " + this);
        }
    }

    /* -------------------- */


    // ---- CAMARA

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
    /* -------------------- */

    // ---- FOV Y PULSE

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

    /* -------------------- */

    // ---- LISTAS

    public void AddEnemy(EnemigoManager enemy)
    {
        enemies.Add(enemy);
    }
    public void RemoveEnemy(EnemigoManager enemy)
    {
        enemies.Remove(enemy);
    }

    public void AddDoor(Puerta puerta)
    {
        puertas_.Add(puerta);
    }

    public void RemoveDoor(Puerta puerta)
    {
        puertas_.Remove(puerta);
    }

    public void AddButton(Boton boton)
    {
        botones_.Add(boton);
    }

    public void RemoveButton(Boton boton)
    {
        botones_.Remove(boton);
    }

    public void AddNodes(List<Transform> nodes, string name)
    {
        GameObject nodeParent = new GameObject();
        nodeParent.transform.parent = NodesPool.transform;

        nodeParent.name = "NodePool" + name;
        foreach (Transform tr in nodes) tr.parent = nodeParent.transform;
    }
    public void addMuebles(GameObject muebles)
    {
        mueblesPadre = muebles;
        mueblesPadre.layer = LayerMask.NameToLayer("Muebles");
        foreach (Transform child in mueblesPadre.transform)
        {
            child.gameObject.layer = LayerMask.NameToLayer("Muebles");
        }
    }

    // ---- UI MANAGER

    public void SetUImanager(UIManager UImanager)
    {
        UIManager_ = UImanager;
        gmInstance_.SetAmmunition();
    }
    public void SetAmmunition()
    {
        UIManager_.SetAmmunition(ammo_);
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

    /* -------------------- */
    // ---- ARMA

    public bool EmptyGun()
    {
        return ammo_ <= 0;
    }

    public void Shoot()
    {
        if (!Uammo)
        {
            ammo_--;
            gmInstance_.SetAmmunition();
        }
    }

    public void AddAmmo(int ammo)
    {
        ammo_ += ammo;
        gmInstance_.SetAmmunition();
    }


    /* -------------------- */

    public void SetEscenario(Transform escenario)
    {
        FieldOfViewPool.transform.parent = escenario;
        PulsePool.transform.parent = escenario;
        NodesPool.transform.parent = escenario;
        EnemiesPool.transform.parent = escenario;
        PuertaPool.transform.parent = escenario;
        BotonesPool.transform.parent = escenario;
        LaserPool.transform.parent = escenario;
    }

    public string SetLoadingManager(LoadManager loadManager) {
        loadManager_ = loadManager;
        return NexLevelLoading;
    }


    //  MANEJO DEL TIEMPO

    public void TogglePause()
    {
        if (paused) ResumeGame();
        else PauseGame();
    }
    public void PauseGame()
    {
        if (!paused && !dead)
        {
            UIManager_.OnPause();
            scriptPistola.Laser(false);
            scriptPC.SetLaser(false);
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

    /* -------------------- */
}