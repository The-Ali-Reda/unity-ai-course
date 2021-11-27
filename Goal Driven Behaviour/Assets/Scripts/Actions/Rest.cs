using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rest : GAction
{

    public override bool PostPerform()
    {
        return true;
    }

    public override bool PrePerform()
    {
        _agentBeliefs.RemoveState("exhausted");
        return true;
    }
}
