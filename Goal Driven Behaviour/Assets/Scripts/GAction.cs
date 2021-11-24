using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class GAction : MonoBehaviour
{
    [SerializeField]
    private string _actionName = "Action";
    [SerializeField]
    private float _cost = 1f;
    [SerializeField]
    private GameObject _target;
    [SerializeField]
    private GameObject _targetTag;
    [SerializeField]
    private float _duaration = 0f;
    [SerializeField]
    private WorldState[] _preConditions;
    [SerializeField]
    private WorldState[] _postEffects;
    [SerializeField]
    private NavMeshAgent _agent;
    [SerializeField]
    private Dictionary<string, int> _preconditions;
    [SerializeField]
    private Dictionary<string, int> _posteffects;
    [SerializeField]
    private WorldStates _agentBeliefs;
    [SerializeField]
    private bool running;

    public GAction()
    {
        _preconditions = new Dictionary<string, int>();
        _posteffects = new Dictionary<string, int>();
    }

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
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
                _posteffects.Add(postEffect.key, postEffect.value);
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
    public abstract bool PrePerform();
    public abstract bool PostPerform();
}
