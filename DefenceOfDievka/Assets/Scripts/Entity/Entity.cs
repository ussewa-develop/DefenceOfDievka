using Mirror;
using UnityEngine;

public class Entity : NetworkBehaviour, IUseable
{
    public virtual string Tip()
    {
        return "����� �� �����";
    }

    public virtual void Use()
    {

    }
}
