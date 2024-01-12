using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGrabbing: IUseable
{
    public void GrabItem(ref PlayerInventory playerInventory);
}
