using Mirror;
using UnityEngine;

public class Entity : NetworkBehaviour, IGrabbing
{
    [SerializeField] Sprite itemSprite;
    public Sprite ItemSprite { get { return itemSprite; } }

    public void GrabItem(ref PlayerInventory playerInventory)
    {
        bool isServer = playerInventory.gameObject.GetComponent<NetworkBehaviour>().isServer;
        if (isServer)
        {
            playerInventory.RPC_AddItem(this);
            
        }
        else
        {
            playerInventory.CmdAddItem(this);
        }
        playerInventory.AddItem(this);
        CellsItemsHUD.Instance.UpdateInventoryCells(playerInventory);
    }

    public virtual string Tip()
    {
        return "Навел на банан";
    }

    public virtual void Use()
    {

    }
}
