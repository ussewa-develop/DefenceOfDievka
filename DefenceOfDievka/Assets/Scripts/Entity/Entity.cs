using Mirror;
using UnityEngine;

public class Entity : NetworkBehaviour, IUseable
{
    public virtual string Tip()
    {
        return "Навел на банан";
    }

    public virtual void Use()
    {

    }
}
