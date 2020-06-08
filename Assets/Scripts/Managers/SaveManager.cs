using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class SaveManager
{
    private static readonly string SaveDirectory = Application.persistentDataPath;


    public static string Load()
    {
        if (File.Exists(SaveDirectory + "/saveGame.txt"))
        {
            return File.ReadAllText(SaveDirectory + "/saveGame.txt");
        }
        else
        {
            Debug.Log("No SavegGame Found");
            return null;
        }
    }

    public static void Save(int level, int ammo, List<EnemigoManager> enemies, Vector3 playerPos, List<Puerta> puertas , List<Boton> botones)
    {
        List<EnemySettings> enemiesOut = new List<EnemySettings>();

        foreach (EnemigoManager e in enemies)
        {
            EnemySettings eSettings = new EnemySettings();
            eSettings.tr_ = e.transform.position;
            eSettings.state = e.getEnemy().GetState();
            eSettings.prevstate = e.getEnemy().GetPrevState();
            eSettings.nodes_.nodes = e.GetNodes();
            eSettings.nodes_.count = eSettings.nodes_.nodes.Count;
            enemiesOut.Add(eSettings);
        }
        List<PuertasStates> puertasOut = new List<PuertasStates>();

        foreach (Puerta p in puertas )
        {
            PuertasStates pOut = new PuertasStates();
            pOut.position_ = p.transform.position;
            if (p.GetOpen()) pOut.rotation_ = p.GetEndRot();
            else pOut.rotation_ = p.transform.localEulerAngles;
            pOut.open_ = p.GetOpen();
            puertasOut.Add(pOut);
        }

        List<BotonStates> botonOut = new List<BotonStates>();

        foreach(Boton b in botones)
        {
            BotonStates bOut = new BotonStates();
            bOut.position_ = b.transform.position;
            if (b.IsActive())
            {
                bOut.active = true;
                List<LaserTr> laserListOut = new List<LaserTr>();
                foreach (GameObject l in b.GetLaser())
                {
                    LaserTr laserOut = new LaserTr();
                    laserOut.positon_ = l.transform.position;
                    laserOut.rotation_ = l.transform.localEulerAngles;
                    laserOut.intermitencia_ = l.GetComponent<Laser>().GetIntermitencia();
                    laserListOut.Add(laserOut);
                }
                bOut.laser = laserListOut;
            }
            else {
                bOut.active = false;
                bOut.laser = null;
            }
            botonOut.Add(bOut);
        }

        GameSave save = new GameSave
        {
            level_ = level,
            ammo_ = ammo,
            playerPos_ = playerPos,
            enemies_ = enemiesOut,
            puertas_ = puertasOut,
            botones_ = botonOut
        };

        string jsonOut = JsonUtility.ToJson(save);

        File.WriteAllText(SaveDirectory + "/saveGame.txt", jsonOut);

        Debug.Log("GameFile saved in " + SaveDirectory);
    }

    [Serializable]
    public struct Nodes
    {
        public int count;
        public List<Vector3> nodes;
    }
    [Serializable]
    public struct EnemySettings
    {
        public Vector3 tr_;
        public Enemigo.State state;
        public Enemigo.State prevstate;
        public Nodes nodes_;
    }
    [Serializable]
    public struct PuertasStates
    {
        public Vector3 position_;
        public Vector3 rotation_;
        public bool open_;
    }
    [Serializable]
    public struct LaserTr
    {
        public Vector3 positon_;
        public Vector3 rotation_;
        public bool intermitencia_;
    }
    [Serializable]
    public struct BotonStates
    {
        public List<LaserTr> laser;
        public Vector3 position_;
        public bool active;
    }
    [Serializable]
    public class GameSave
    {
        public int level_;
        public int ammo_;
        public Vector3 playerPos_;
        public List<EnemySettings> enemies_;
        public List<PuertasStates> puertas_;
        public List<BotonStates> botones_;
    }

    public static string LoadSettings()
    {
        if (File.Exists(SaveDirectory + "/saveSettings.txt"))
        {
            return File.ReadAllText(SaveDirectory + "/saveSettings.txt");
        }
        else
        {
            Debug.Log("No SavegSettings Found");
            return null;
        }
    }

    public static void SaveSettings(SettingsSave settings_)
    {
        SettingsSave settings = new SettingsSave();
        settings = settings_;

        string settingsOut = JsonUtility.ToJson(settings);

        File.WriteAllText(SaveDirectory + "/saveSettings.txt", settingsOut);

        Debug.Log("Settings saved in " + SaveDirectory);
    }

    [Serializable]
    public class SettingsSave
    {
        public float fxVolume_;
        public float musicVolume_;
        public bool controller_;
        public bool cheats_;
    }
}



