using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD : MonoBehaviour
{
    [SerializeField] private string hudName;
    [SerializeField] private CursorLockMode cursorLockMode;
    public CursorLockMode CursorLockMode
    {
        get
        { 
            return cursorLockMode; 
        }
    }
    public string HudName 
    {
        get
        {
            return hudName;
        }
    }

    public void Init()
    {
        hudName = gameObject.name;
    }

}
