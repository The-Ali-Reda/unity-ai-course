using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts
{
    public class Pursue: State
    {
        public Pursue(GameObject npc, NavMeshAgent agent, Animator anim, Transform player) : base(npc, agent, anim, player)
        {
            _name = STATE.PURSUE;
            _agent.speed = 5;
            _agent.isStopped = false;
        }
        public override void Enter()
        {

            _anim.SetTrigger("isRunning");
            base.Enter();
        }

        public override void Exit()
        {
            _anim.ResetTrigger("isRunning");
            base.Exit();
        }

        public override void Update()
        {
            _agent.SetDestination(_player.position);
            if(_agent.hasPath && CanAttackPlayer())
            {
                _nextState = new Attack(_npc, _agent, _anim, _player);
                _stage = EVENT.EXIT;
            } else if (!CanSeePlayer())
            {
                _nextState = new Patrol(_npc, _agent, _anim, _player);
                _stage = EVENT.EXIT;
            }
        }
    }
}
