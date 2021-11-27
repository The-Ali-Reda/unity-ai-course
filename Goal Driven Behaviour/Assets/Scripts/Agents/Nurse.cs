using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nurse : GAgent
{
    // Start is called before the first frame update
    private void Start()
    {
        base.Init();
        SubGoal s1 = new SubGoal("treatPatient", 1, true);
        SubGoals.Add(s1, 3);
    }
}
