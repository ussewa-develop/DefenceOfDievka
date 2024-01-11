using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : NetworkBehaviour
{
    private PlayerRayCast _player;
    [SerializeField] private List<Entity> itemList;
    [SerializeField] private int currentCellNumber;

    private void Start()
    {
        itemList = new List<Entity>(4);
        if (!isLocalPlayer)
        {
            return;
        }
        HUD_Switcher.Instance.ChangeCellItem += SetCurrentCell;
    }

    private void LateUpdate()
    {
        if(!isLocalPlayer)
        {
            return;
        }
        Interact();
    }

    private void Interact()
    {
        if (Input.GetKeyDown(KeyCode.E) && _player.selectedEntity != null)
        {
            _player.selectedEntity.Use();
        }
    }

    public void SetCurrentCell(int cellNumber)
    {
        currentCellNumber = cellNumber;
    }

    public void AddItem(Entity item)
    {
        itemList.Add(item);
    }

    public void RemoveItem(Entity item)
    {
        itemList.Remove(item);
    }
}
