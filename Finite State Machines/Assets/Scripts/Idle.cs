using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Idle : State
{
    public Idle(GameObject npc, NavMeshAgent agent, Animator anim, Transform player) : base(npc, agent, anim, player)
    {
        _name = STATE.IDLE;
    }
    public override void Enter()
    {
        _anim.SetTrigger("isIdle");
        base.Enter();
    }

    public override void Exit()
    {
        _anim.ResetTrigger("isIdle");
        base.Exit();
    }

    public override void Update()
    {
        if(Random.Range(0,100)<10)
        {
            _nextState = new Patrol(_npc, _agent, _anim, _player);
            _stage = EVENT.EXIT;
        }
        else
            base.Update();
    }
}
