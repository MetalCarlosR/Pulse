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

    public static void Save(int level, int ammo, List<EnemigoManager> enemies, Vector3 playerPos)
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

        GameSave save = new GameSave
        {
            level_ = level,
            ammo_ = ammo,
            playerPos_ = playerPos,
            enemies_ = enemiesOut
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
    public class GameSave
    {
        public int level_;
        public int ammo_;
        public Vector3 playerPos_;
        public List<EnemySettings> enemies_;
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

    public static void SaveSettings(float fxVolume, float musicVolume)
    {
        SettingsSave settings = new SettingsSave
        {
            fxVolume_ = fxVolume,
            musicVolume_ = musicVolume
        };

        string settingsOut = JsonUtility.ToJson(settings);

        File.WriteAllText(SaveDirectory + "/saveSettings.txt", settingsOut);

        Debug.Log("Settings saved in " + SaveDirectory);
    }

    [Serializable]
    public class SettingsSave
    {
        public float fxVolume_;
        public float musicVolume_;
    }
}



