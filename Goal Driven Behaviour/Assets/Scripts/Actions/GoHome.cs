using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoHome : GAction
{

    public override bool PostPerform()
    {
        Destroy(gameObject);
        return true;
    }

    public override bool PrePerform()
    {
        
        return true;
    }
}
