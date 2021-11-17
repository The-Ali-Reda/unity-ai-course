using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Patrol : State
{

    private int _currentIndex = -1;
    public Patrol(GameObject npc, NavMeshAgent agent, Animator anim, Transform player) : base(npc, agent, anim, player)
    {
        _name = STATE.PATROL;
        _agent.speed = 2;
        _agent.isStopped = false;
    }
    public override void Enter()
    {
        _currentIndex = GameEnvironment.Instance.GetClosestWaypointIndex(_npc.transform.position);
        _agent.SetDestination(GameEnvironment.Instance.CheckPoints[_currentIndex].transform.position);
        _anim.SetTrigger("isWalking");
        base.Enter();
    }

    public override void Exit()
    {
        _anim.ResetTrigger("isWalking");
        base.Exit();
    }

    public override void Update()
    {
        if (_agent.hasPath && _agent.remainingDistance < 1f)
        {
            _currentIndex = (_currentIndex + 1) % GameEnvironment.Instance.CheckPoints.Count;
            _agent.SetDestination(GameEnvironment.Instance.CheckPoints[_currentIndex].transform.position);
        }
        else if (CanSeePlayer())
        {
            _nextState = new Pursue(_npc, _agent, _anim, _player);
            _stage = EVENT.EXIT;
        } else if (IsCreptBehind())
        {
            _nextState = new Scared(_npc, _agent, _anim, _player);
            _stage = EVENT.EXIT;
        }
    }
}
