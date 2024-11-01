using Mirror;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInventory : NetworkBehaviour
{
    private PlayerInventory _instance;
    private PlayerRayCast _player;
    [SerializeField] private List<Entity> itemList = new List<Entity> { null, null, null, null };
    [SerializeField, SyncVar] private int currentCellNumber;

    private void Start()
    {
        _instance = this;
        _player = GetComponent<PlayerRayCast>();
        if (!isLocalPlayer)
        {
            return;
        }
        HUD_Switcher.Instance.ChangeCellItem += CmdSetCurrentCell;
        HUD_Switcher.Instance.ChangeCellItem += CmdChangePickUpItem;
        Debug.Log("itemList.COunt = " + itemList.Count);
    }

    private void LateUpdate()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        Interact();
        if(Input.GetKeyDown(KeyCode.G))
        {
            CmdDropItem(currentCellNumber);
        }
        if(Input.GetMouseButtonDown(0))
        {
            CmdUseItem(currentCellNumber);
            CmdDropItem(currentCellNumber);
        }
    }

    private void Interact()
    {
        if (Input.GetKeyDown(KeyCode.E) && _player.selectedEntity != null && _player.selectedEntity is IGrabbing)
        {
            IGrabbing item = _player.selectedEntity as IGrabbing;
            item.GrabItem(ref _instance);
        }
        else if (Input.GetKeyDown(KeyCode.E) && _player.selectedEntity != null)
        {
            _player.selectedEntity.Use();
        }
    }

    public void RefreshCellsInHUD()
    {
        if (isLocalPlayer)
        {
            CellsItemsHUD.Instance.RefreshInventoryCells(this);
        }
    }

    [Command]
    private void CmdUseItem(int cellNumber)
    {
        RPC_UseItem(cellNumber);
    }

    [ClientRpc]
    private void RPC_UseItem(int cellNumber)
    {

        if (itemList[cellNumber] != null)
        {
            itemList[cellNumber].Use();
        }
    }

    [Command]
    private void CmdChangePickUpItem(int cellNumber)
    {
        RPC_ChangePickUpItem(cellNumber);
    }

    [ClientRpc]
    private void RPC_ChangePickUpItem(int cellNumber)
    {
        for(int i = 0; i < itemList.Count; i++)
        {
            if (itemList[i] != null)
            {
                if (i != currentCellNumber)
                {
                    itemList[i].gameObject.SetActive(false);
                }
                else
                {
                    itemList[i].gameObject.SetActive(true);
                }
            }
        }
    }


    [Command]
    private void CmdDropItem(int cellNumber)
    {
        if (itemList[cellNumber] != null)
        {
            RPC_DropItem(cellNumber);
        }
    }

    [ClientRpc]
    private void RPC_DropItem(int cellNumber)
    {
        itemList[cellNumber].Collider.enabled = true;
        itemList[cellNumber].Rigidbody.isKinematic = false;
        itemList[cellNumber].transform.parent = null;
        itemList[cellNumber] = null;
        RefreshCellsInHUD();
    }

    [Command]
    public void CmdSetCurrentCell(int cellNumber)
    {
        RPC_SetCurrentCell(cellNumber);
    }

    [ClientRpc]
    public void RPC_SetCurrentCell(int cellNumber)
    {
        currentCellNumber = cellNumber;
        if (itemList[currentCellNumber] == null)
        {
            return;
        }
    }


    [Command]
    public void CmdAddItem(Entity item)
    {
        RPC_AddItem(item);
    }

    [ClientRpc]
    public void RPC_AddItem(Entity item)
    {
        CanvasManager.Instance.SetServerMessage("Player " + gameObject.name + " grab " + item.Name);
        Debug.Log("Player " + gameObject.name + " grab item");
        itemList[currentCellNumber] = item;
        item.transform.parent = gameObject.transform;
        item.transform.localPosition = item.Offset;
        item.transform.localEulerAngles = Vector3.zero;
        item.Collider.enabled = false;
        item.Rigidbody.isKinematic = true;
        RefreshCellsInHUD();
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
