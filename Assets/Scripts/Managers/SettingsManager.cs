using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    public static SettingsManager smInstance_;

    private SaveManager.SettingsSave gameSettings;

    void Awake()
    {
        if (smInstance_ == null)
        {
            smInstance_ = this;
            Load();
            Debug.Log("SettingsManager Set");
        }
        else if (smInstance_ != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this);

    }

    void Load()
    {
        string loadString = SaveManager.LoadSettings();

        if (loadString != null)
        {
            Debug.Log("SettingsLoaded " + loadString);

            gameSettings = JsonUtility.FromJson<SaveManager.SettingsSave>(loadString);
        }
        else
        {
            gameSettings = new SaveManager.SettingsSave
            {
                fxVolume_ = 5,
                musicVolume_ = 1,
                controller_ = false,
                cheats_ = false
            };
        }
        ActivateController(gameSettings.controller_);
    }

    public SaveManager.SettingsSave GetSettings()
    {
        return gameSettings;
    }

    public void Save()
    {
        SaveManager.SaveSettings(gameSettings);
    }

    public void ActivateCheats(bool b)
    {
        gameSettings.cheats_ = b;
    }
    public void ActivateController(bool c)
    {
        gameSettings.controller_ = c;
        Cursor.visible = !c;
        if(c) Cursor.lockState = CursorLockMode.Locked;
        else Cursor.lockState = CursorLockMode.None;
    }
}
