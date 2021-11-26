using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class SubGoal
{
    public Dictionary<string, int> SubGoals { get; private set; }
    public bool Remove { get; private set; }
    public SubGoal(string s, int i, bool r)
    {
        SubGoals = new Dictionary<string,int>();
        SubGoals[s] = i;
        Remove = r;
    }
}

public class GAgent : MonoBehaviour
{
    [field:SerializeField]
    public List<GAction> Actions { get; private set; } = new List<GAction> ();
    public Dictionary<SubGoal, int> SubGoals { get; private set; } = new Dictionary<SubGoal, int> ();
    public GPlanner Planner { get; private set; }
    private Queue<GAction> ActionsQueue { get; set; }
    public GAction CurrentAction { get; private set; }
    private SubGoal _currentGoal;
    private bool invoked = false;
    public void Init()
    {
        GAction[] acts = this.GetComponents<GAction> ();
        foreach (GAction act in acts)
        {
            Actions.Add (act);
        }
    }

    private void CompleteAction()
    {
        CurrentAction.Complete();
        invoked = false;
    }

    private void LateUpdate()
    {
        if (CurrentAction != null && CurrentAction.Running)
        {
            if(CurrentAction.Agent.hasPath && CurrentAction.Agent.remainingDistance < 1f)
            {
                if (!invoked)
                {
                    Invoke("CompleteAction", CurrentAction.Duration);
                    invoked = true;
                }
            }
            return;
        }

        if (Planner == null || ActionsQueue == null)
        {
            Planner = new GPlanner ();
            var sortedGoals = SubGoals.OrderByDescending(g=> g.Value);
            foreach(KeyValuePair<SubGoal, int> pair in sortedGoals)
            {
                ActionsQueue = Planner.Plan(Actions, pair.Key.SubGoals, null);
                if (ActionsQueue != null)
                {
                    _currentGoal = pair.Key;
                    break;
                }
            }
        }
        if(ActionsQueue!= null && ActionsQueue.Count == 0)
        {
            if(_currentGoal.Remove)
                SubGoals.Remove(_currentGoal);
            Planner = null;

        }
        if(ActionsQueue != null&& ActionsQueue.Count>0)
        {
            CurrentAction = ActionsQueue.Dequeue();
            if (CurrentAction.PrePerform())
            {
                if(CurrentAction.Target ==null && CurrentAction.TargetTag != string.Empty)
                {
                    var tar = GameObject.FindGameObjectWithTag(CurrentAction.TargetTag);
                    CurrentAction.SetTarget(tar);
                }
                if (CurrentAction.Target != null)
                {
                    CurrentAction.Run();
                }
            }
            else
            {
                ActionsQueue = null;
            }
        }
    }
}
