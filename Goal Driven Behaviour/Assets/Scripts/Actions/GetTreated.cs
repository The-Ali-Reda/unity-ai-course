using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetTreated : GAction
{

    public override bool PostPerform()
    {
        GWorld.Instance.GetWorld().ModifyState("isTreated", 1);
        Inventory.DeleteItem(Target);
        return true;
    }

    public override bool PrePerform()
    {
        Target = Inventory.FindItemWithTag("Cubicle");
        if (Target == null)
            return false;
        return true;
    }
}