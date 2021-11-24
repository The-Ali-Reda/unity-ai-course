using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class WorldState
{
    public string key;
    public int value;
}
public class WorldStates
{
    private Dictionary<string, int> _states;
    
    public WorldStates()
    {
        _states = new Dictionary<string, int>();
    }
    public bool HasState(string key)
    {
        return _states.ContainsKey(key);
    }
    private void AddState(string key, int value)
    {
        _states[key] = value;
    }
    public void ModifyState(string key, int value)
    {
        if (_states.ContainsKey(key))
        {
            _states[key] += value;
            if(_states[key]<=0)
                RemoveState(key);
        }
        else
            _states.Add(key, value);
    }
    public void RemoveState(string key)
    {
        _states.Remove(key);
    }
    public void SetState(string key, int value)
    {
        _states[key] = value;
    }
    public Dictionary<string, int> GetStates()
    {
        return _states;
    }
}
