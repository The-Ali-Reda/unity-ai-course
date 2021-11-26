using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class GAction : MonoBehaviour
{
    [field:SerializeField]
    public string ActionName { get; private set; } = "Action";
    [field:SerializeField]
    public float Cost { get; private set; } = 1f;
    [field:SerializeField]
    public GameObject Target { get; private set; }
    [field:SerializeField]
    public string TargetTag { get; private set; }
    [field:SerializeField]
    public float Duration { get; private set; } = 0f;
    [SerializeField]
    private WorldState[] _preConditions;
    [SerializeField]
    private WorldState[] _postEffects;
    [field:SerializeField]
    public NavMeshAgent Agent { get; private set; }
    [SerializeField]
    private Dictionary<string, int> _preconditions;
    [field:SerializeField]
    public Dictionary<string, int> Posteffects { get; private set; }
    [SerializeField]
    private WorldStates _agentBeliefs;
    [field:SerializeField]
    public bool Running { get; private set; }

    public GAction()
    {
        _preconditions = new Dictionary<string, int>();
        Posteffects = new Dictionary<string, int>();
    }

    public void Run()
    {
        Running = true;
        Agent.SetDestination(Target.transform.position);
    }

    public void SetTarget(GameObject target)
    {
        Target = target;
    }
    private void Awake()
    {
        Agent = GetComponent<NavMeshAgent>();
        if (_preConditions != null)
        {
            foreach (var preCondition in _preConditions)
            {
                _preconditions.Add(preCondition.key, preCondition.value);
            }
        }
        if (_postEffects != null)
        {
            foreach (var postEffect in _postEffects)
            {
                Posteffects.Add(postEffect.key, postEffect.value);
            }
        }
    }

    public bool IsAchievable()
    {
        return true;
    }

    public bool IsAchievableGiven(Dictionary<string, int> conditions)
    {
        foreach(var condition in _preconditions)
        {
            if (!conditions.ContainsKey(condition.Key))
                return false;
        }
        return true;
    }

    public void Complete()
    {
        Running = false;
        PostPerform();
    }
    public abstract bool PrePerform();
    public abstract bool PostPerform();
}
