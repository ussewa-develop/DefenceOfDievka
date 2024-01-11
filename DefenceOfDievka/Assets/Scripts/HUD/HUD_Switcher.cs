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
    public event Action<int> ChangeCellItem;

    private bool gameIsPaused = false;

    private void Start()
    {
        _instance = this;
        GamePaused += PauseGame;
        CellsItemsHUD.Instance.Inst();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            InvokeGamePaused();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            InvokeChangeCellItem(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            InvokeChangeCellItem(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            InvokeChangeCellItem(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            InvokeChangeCellItem(3);
        }

    }

    public void InvokeGamePaused()
    {
        GamePaused?.Invoke();
    }

    public void InvokeChangeCellItem(int cellNumber)
    {
        ChangeCellItem?.Invoke(cellNumber);
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
