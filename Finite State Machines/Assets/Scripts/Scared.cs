using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Scared : State
{
    private Vector3 _safeLocation;
    public Scared(GameObject npc, NavMeshAgent agent, Animator anim, Transform player) : base(npc, agent, anim, player)
    {
        _name = STATE.SCARED;
        _agent.speed = 6;
        _agent.isStopped = false;
    }
    public override void Enter()
    {

        _anim.SetTrigger("isRunning");
        _safeLocation = GameEnvironment.Instance.SafeCube.transform.position;
        _agent.SetDestination(_safeLocation);
        base.Enter();
    }

    public override void Exit()
    {
        _anim.ResetTrigger("isRunning");
        base.Exit();
    }

    public override void Update()
    {
        
        if (Vector3.Distance(_npc.transform.position, _safeLocation)<1f)
        {
            _nextState = new Patrol(_npc, _agent, _anim, _player);
            _stage = EVENT.EXIT;
        }
    }
}
