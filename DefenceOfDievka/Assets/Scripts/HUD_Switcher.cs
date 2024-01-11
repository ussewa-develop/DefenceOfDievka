using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HUD_Switcher : MonoBehaviour
{
    private static HUD_Switcher _instance;
    public static HUD_Switcher Instance
    { 
        get 
        { 
            return _instance; 
        } 
    }

    public event Action GamePaused;

    private bool gameIsPaused = false;

    private void Start()
    {
        _instance = this;
        GamePaused += PauseGame;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            InvokeGamePaused();
        }
    }

    public void InvokeGamePaused()
    {
        GamePaused?.Invoke();
    }

    public void PauseGame()
    {
        gameIsPaused = !gameIsPaused;
        if (gameIsPaused)
        {
            CanvasManager.Instance.OpenHUD("menu");
        }
        else
        {
            CanvasManager.Instance.OpenHUD("cursor");
        }
    }
}
