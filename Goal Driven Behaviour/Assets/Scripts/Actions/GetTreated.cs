using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetTreated : GAction
{

    public override bool PostPerform()
    {
        Inventory.DeleteItem(Target);
        _agentBeliefs.ModifyState("isCured", 1);
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