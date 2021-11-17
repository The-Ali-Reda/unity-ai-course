using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class State
{
    public enum STATE
    {
        IDLE, PATROL, PURSUE, ATTACK, SLEEP, SCARED
    };
    public enum EVENT
    {
        ENTER, UPDATE, EXIT
    };
    protected STATE _name;
    protected EVENT _stage;
    protected GameObject _npc;
    protected Animator _anim;
    protected Transform _player;
    protected State _nextState;
    protected NavMeshAgent _agent;
    private float _visDistance = 10.0f;
    private float _visAngle = 30.0f;
    private float _shootDist = 7.0f;
    private float _creptDist = 2.0f;
    private float _creptAngle = 30.0f;
    public STATE Name { get { return _name; } }
    public State(GameObject npc, NavMeshAgent agent, Animator anim, Transform player)
    {
        _npc = npc;
        _agent = agent;
        _anim = anim;
        _player = player;
        _stage = EVENT.ENTER;
    }

    public virtual void Enter()
    {
        _stage = EVENT.UPDATE;
    }
    public virtual void Update()
    {
        _stage = EVENT.UPDATE;
    }
    public virtual void Exit()
    {
        _stage = EVENT.EXIT;
    }
    public State process()
    {
        if (_stage == EVENT.ENTER)
            Enter();
        if (_stage == EVENT.UPDATE)
            Update();
        if (_stage == EVENT.EXIT)
        {
            Exit();
            return _nextState;
        }
        return this;
    }

    public bool CanSeePlayer()
    {
        Vector3 direction = _player.position - _npc.transform.position;
        float angle = Vector3.Angle(direction, _npc.transform.forward);
        if (direction.magnitude < _visDistance && angle < _visAngle)
            return true;
        return false;
    }
    public bool CanAttackPlayer()
    {
        Vector3 direction = _player.position - _npc.transform.position;
        if (direction.magnitude < _shootDist)
            return true;
        return false;
    }
    public bool IsCreptBehind()
    {
        Vector3 direction = _player.position - _npc.transform.position;
        float angle = Vector3.Angle(direction, -_npc.transform.forward);
        if (direction.magnitude <= _creptDist && angle <= _creptAngle)
            return true;
        return false;
    }
}
