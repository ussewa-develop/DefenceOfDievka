using Mirror;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInventory : NetworkBehaviour
{
    private PlayerInventory _instance;
    private PlayerRayCast _player;
    [SerializeField] private List<Entity> itemList = new List<Entity> {null, null, null, null};
    [SerializeField] private int currentCellNumber;

    private void Start()
    {
        _instance = this;
        _player = GetComponent<PlayerRayCast>();
        if (!isLocalPlayer)
        {
            return;
        }
        HUD_Switcher.Instance.ChangeCellItem += SetCurrentCell;
        Debug.Log("itemList.COunt = " + itemList.Count);
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
        if (Input.GetKeyDown(KeyCode.E) && _player.selectedEntity != null && _player.selectedEntity is IGrabbing)
        {
            IGrabbing item = _player.selectedEntity as IGrabbing;
            item.GrabItem(ref _instance);
        }
        else if(Input.GetKeyDown(KeyCode.E) && _player.selectedEntity != null)
        {
            _player.selectedEntity.Use();
        }
    }

    public void SetCurrentCell(int cellNumber)
    {
        currentCellNumber = cellNumber;
    }

    [Command]
    public void CmdAddItem(Entity item)
    {
        CanvasManager.Instance.serverText.text = "CmdAddItem";
        AddItem(item);
    }

    [ClientRpc]
    public void RPC_AddItem(Entity item)
    {
        CanvasManager.Instance.serverText.text = "RPC_AddItem";
        AddItem(item);
    }

    public void AddItem(Entity item)
    {
        Debug.Log("Player "+ gameObject.name + " grab item");
        itemList[currentCellNumber] = item;
    }

    public void RemoveItem(Entity item)
    {
        itemList.Remove(item);
    }

    public List<Entity> GetInventory()
    {
        return itemList;
    }
}
