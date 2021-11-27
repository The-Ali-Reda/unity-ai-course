using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToCubicle : GAction
{

    public override bool PostPerform()
    {
        GWorld.Instance.AddCubicle(Target);
        Inventory.DeleteItem(Target);
        GWorld.Instance.GetWorld().ModifyState("FreeCubicle", 1);

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
