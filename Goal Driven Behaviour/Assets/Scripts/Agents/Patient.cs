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
        SubGoal s2 = new SubGoal("isTreated", 1, true);
        SubGoal s3 = new SubGoal("isHome", 1, true);
        SubGoals.Add(s1, 1);
        SubGoals.Add(s2, 5);
        SubGoals.Add(s3, 5);
    }
}
