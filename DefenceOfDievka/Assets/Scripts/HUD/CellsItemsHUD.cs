using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UI;

public class CellsItemsHUD : MonoBehaviour
{
    private static CellsItemsHUD _instance;
    public static CellsItemsHUD Instance
    {
        get 
        { 
            return _instance; 
        }
    }

    [SerializeField] private Sprite nullSprite;
    [SerializeField] private List<InventoryCell> cellsImages;

    private void Start()
    {
        _instance = this;
    }

    public void Inst()
    {
        SetInventoryCells();
        HUD_Switcher.Instance.ChangeCellItem += SetCurrentCell;
        SetCurrentCell(0);
    }

    public void SetInventoryCells()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            cellsImages.Add(transform.GetChild(i).GetComponent<InventoryCell>());
            cellsImages[i].SetImageInCell(nullSprite);
        }
    }

    public void SetCurrentCell(int cellNumber)
    {
        foreach (InventoryCell cell in cellsImages)
        {
            cell.SetActiveCell(false);
        }
        cellsImages[cellNumber].SetActiveCell(true);
    }
}
