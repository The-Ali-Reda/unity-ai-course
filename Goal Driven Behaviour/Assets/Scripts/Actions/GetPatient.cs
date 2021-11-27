using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetPatient : GAction
{
    private GameObject resource; //cubicle
    public override bool PostPerform()
    {
        GWorld.Instance.GetWorld().ModifyState("patientWaiting", -1);
        if (Target)
        {
            Target.GetComponent<GAgent>().Inventory.AddItem(resource);
        }
        return true;
    }

    public override bool PrePerform()
    {
        Target = GWorld.Instance.RemovePatient();
        if (Target == null)
            return false;
        resource = GWorld.Instance.RemoveCubicle();
        if (resource != null)
            Inventory.AddItem(resource);
        else
        {
            GWorld.Instance.AddPatient(Target);
            Target = null;
            return false;
        }
        GWorld.Instance.GetWorld().ModifyState("FreeCubicle", -1);
        return true;
    }
}
