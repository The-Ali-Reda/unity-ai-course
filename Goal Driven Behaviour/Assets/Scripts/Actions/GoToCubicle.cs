using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToCubicle : GAction
{

    public override bool PostPerform()
    {
        return true;
    }

    public override bool PrePerform()
    {
        Target = GWorld.Instance.RemoveCubicle();
        if (Target == null)
            return false;
        return true;
    }
}
