using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CursorHUD : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI interactText;

    public TextMeshProUGUI InteractText
    {
        get
        {
            return interactText;
        }
    }
}
