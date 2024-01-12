using Mirror;
using System;
using UnityEngine;

public class Entity : NetworkBehaviour, IGrabbing
{

    [SerializeField] string itemName;
    [SerializeField] Collider itemCollider;
    [SerializeField] Rigidbody itemRigidbody;
    [SerializeField] Vector3 itemOffset = new Vector3(0.4f,0,0.4f);
    [SerializeField] Sprite itemSprite;

    public Sprite Sprite { get { return itemSprite; } }
    public string Name { get { return itemName; } }
    public Vector3 Offset { get { return itemOffset; } }
    public Collider Collider { get { return itemCollider; } }
    public Rigidbody Rigidbody { get {  return itemRigidbody; } }

    private void Start()
    {
        itemCollider = GetComponent<Collider>();
        itemRigidbody = GetComponent<Rigidbody>();
    }

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
        Rigidbody.isKinematic = false;
        Rigidbody.AddForce(transform.forward*1000, ForceMode.Force);
    }
}
