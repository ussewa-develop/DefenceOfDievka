using System.Collections.Generic;
using UnityEngine;


public class CanvasManager : MonoBehaviour
{
    private static CanvasManager _instance;
    public static CanvasManager Instance { get { return _instance; } }

    [SerializeField] List<HUD> huds;
    [SerializeField] private CursorHUD cursorHud;

    void Start()
    {
        _instance = this;
        for (int i = 0; i < transform.childCount; i++)
        {
            if(transform.GetChild(i).gameObject.GetComponent<HUD>())
            {
                huds.Add(transform.GetChild(i).gameObject.GetComponent<HUD>());
                huds[i].Init();
            }
        }
        OpenHUD("null");
        DontDestroyOnLoad(this);
    }

    
    public void OpenHUD(string hudName)
    {
        foreach (HUD hud in huds)
        {
            if (hud.HudName.ToLower() == hudName.ToLower())
            {
                SetActiveCurrentHUD(hud, true);
            }
            else
            {
                SetActiveCurrentHUD(hud, false);
            }
        }
    }

    public void SetActiveCurrentHUD(HUD hud, bool value)
    {
        hud.gameObject.SetActive(value);
        if(value == true)
        {
            Cursor.lockState = hud.CursorLockMode;
            switch (hud.CursorLockMode)
            {
                case CursorLockMode.Locked:
                    Cursor.visible = false;
                    break;
                case CursorLockMode.Confined: 
                    Cursor.visible = true; 
                    break;
            }
        }
    }

    public void SetActiveInteractText(bool value)
    {
        cursorHud.InteractText.gameObject.SetActive(value);
    }

    public void ChangeInteractText(string text)
    {
        cursorHud.InteractText.text = text;
    }


}
