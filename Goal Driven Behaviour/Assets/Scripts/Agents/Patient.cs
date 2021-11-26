using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patient : GAgent
{
    // Start is called before the first frame update
    private void Start()
    {
        base.Init();
        SubGoal s1 = new SubGoal("isWaiting", 1, true);
        SubGoals.Add(s1, 3);
    }
}
