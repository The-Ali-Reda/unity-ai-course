using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nurse : GAgent
{
    // Start is called before the first frame update
    private void Start()
    {
        base.Init();
        SubGoal s1 = new SubGoal("treatPatient", 1, false);
        SubGoal s2 = new SubGoal("rested", 1, false);
        SubGoals.Add(s1, 3);
        SubGoals.Add(s2, 7);
        Invoke("GetTired", Random.Range(10, 20));
    }
    private void GetTired()
    {
        AgentBeliefs.ModifyState("exhausted", 1);
        Invoke("GetTired", Random.Range(10, 20));
    }
}
