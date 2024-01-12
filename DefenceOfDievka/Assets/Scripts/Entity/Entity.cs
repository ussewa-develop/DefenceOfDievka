using Mirror;
using UnityEngine;

public class Entity : NetworkBehaviour, IGrabbing
{
    [SerializeField] string itemName;
    [SerializeField] Sprite itemSprite;
    public Sprite ItemSprite { get { return itemSprite; } }
    public string ItemName { get { return itemName; } }

    public void GrabItem(ref PlayerInventory playerInventory)
    {
        bool isServer = playerInventory.gameObject.GetComponent<NetworkBehaviour>().isServer;
        bool isLocalPlayer = playerInventory.gameObject.GetComponent<NetworkBehaviour>().isLocalPlayer;
        if (isServer)
        {
            playerInventory.RPC_AddItem(this);
        }
        else
        {
            playerInventory.CmdAddItem(this);
        }
        
    }

    public virtual string Tip()
    {
        return "Навел на банан";
    }

    public virtual void Use()
    {

    }
}
