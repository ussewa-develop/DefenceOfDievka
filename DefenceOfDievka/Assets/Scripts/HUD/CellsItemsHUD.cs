using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CellsItemsHUD : MonoBehaviour, IStaticScript
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

    public void Inst()
    {
        _instance = this;
        SetInventoryCells();
        SetCurrentCell(0);
        ScriptOrderLoader.AllScriptsLoaded += AddSubscribe;
    }

    public void AddSubscribe()
    {
        HUD_Switcher.Instance.ChangeCellItem += SetCurrentCell;
    }

    public void UpdateInventoryCells(PlayerInventory playerInventory)
    {
        List<Entity> inventory = playerInventory.GetInventory();
        for (int i = 0; i < cellsImages.Count; i++)
        {
            if (inventory[i] == null)
            {
                cellsImages[i].SetImageInCell(nullSprite);
            }
            else
            {
                cellsImages[i].SetImageInCell(inventory[i].ItemSprite);
            }
        }
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
