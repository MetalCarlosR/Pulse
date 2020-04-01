using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) GameManager.gmInstance_.TogglePause();
        if (Input.GetKeyDown(KeyCode.L)) GameManager.gmInstance_.ReloadScene();
    }
}
