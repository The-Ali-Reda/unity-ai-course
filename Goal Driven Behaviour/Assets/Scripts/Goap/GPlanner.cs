using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class Node
{
    public Node parent;
    public float cost;
    public Dictionary<string, int> state;
    public GAction action;
    public Node(Node parent, float cost, Dictionary<string, int> allstates, GAction action)
    {
        this.parent = parent;
        this.cost = cost;
        this.state = new Dictionary<string, int>(allstates);
        this.action = action;
    }
}
public class GPlanner
{
    public Queue<GAction> Plan(List<GAction> actions, Dictionary<string, int> goal, WorldStates states)
    {
        List<GAction> usableActions = new List<GAction>();
        foreach (GAction action in actions)
        {
            if(action.IsAchievable())
                usableActions.Add(action);
        }
        List<Node> leaves = new List<Node> ();
        Node start = new Node(null, 0, GWorld.Instance.GetWorld().GetStates(), null);
        bool success = BuildGraph(start, leaves, usableActions, goal);
        if (!success)
        {
            Debug.Log("NO Plan");
            return null;
        }
        Node cheapest = null;
        foreach(Node leaf in leaves)
        {
            if(cheapest==null || leaf.cost < cheapest.cost)
            {
                cheapest = leaf;
            }
        }
        List<GAction> result = new List<GAction> ();
        Node n = cheapest;
        while (n != null)
        {
            if(n.action != null)
            {
                result.Insert(0, n.action);
            }
            n = n.parent;
        }
        Queue<GAction> queue = new Queue<GAction>(result);
        Debug.Log("The plan is: ");
        foreach (GAction action in queue)
        {
            Debug.Log("Q: "+ action.ActionName);
        }
        return queue;
    }

    private bool BuildGraph(Node parent, List<Node> leaves, List<GAction> usableActions, Dictionary<string, int> goal)
    {
        bool foundPath = false;
        foreach (GAction action in usableActions)
        {
            if (action.IsAchievableGiven(parent.state))
            {
                Dictionary<string, int> currentState = new Dictionary<string, int>(parent.state);
                foreach(KeyValuePair<string, int> effect in action.Posteffects)
                {
                    if (!currentState.ContainsKey(effect.Key))
                    {
                        currentState[effect.Key] = effect.Value;
                    }
                }
                Node node = new Node(parent, parent.cost + action.Cost, currentState, action);
                if(GoalAchieved(goal, currentState))
                {
                    foundPath = true;
                    leaves.Add(node);
                }
                else
                {
                    List<GAction> actionsSubset = ActionSubset(usableActions, action);
                    foundPath = BuildGraph(node, leaves, actionsSubset, goal);
                    
                }
            }
        }
        return foundPath;
    }

    private List<GAction> ActionSubset(List<GAction> actions, GAction toBeRemoved)
    {
        List<GAction> subset = new List<GAction>();
        foreach(GAction action in actions)
        {
            if(!action.Equals(toBeRemoved))
                subset.Add(action);
        }
        return subset;
    }

    private bool GoalAchieved(Dictionary<string, int> goal, Dictionary<string, int> currentState)
    {
        foreach(KeyValuePair<string, int> g in goal)
        {
            if(!currentState.ContainsKey(g.Key))
                return false;
        }
        return true;
    }
}
