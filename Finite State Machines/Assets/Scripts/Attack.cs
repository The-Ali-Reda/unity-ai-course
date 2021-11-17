using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts
{
    public class Attack: State
    {
        private float _rotSpeed = 2f;
        private AudioSource _audioSource; 
        public Attack(GameObject npc, NavMeshAgent agent, Animator anim, Transform player) : base(npc, agent, anim, player)
        {
            _name = STATE.ATTACK;
            _agent.speed = 5;
            _agent.isStopped = false;
            _audioSource = npc.GetComponent<AudioSource>();
        }
        public override void Enter()
        {
            _anim.SetTrigger("isShooting");
            _agent.isStopped = true;
            _audioSource.Play();
            base.Enter();
        }

        public override void Exit()
        {
            _anim.ResetTrigger("isShooting");
            _audioSource.Stop();
            base.Exit();
        }

        public override void Update()
        {
            Vector3 direction = _player.position - _npc.transform.position;
            float angle = Vector3.Angle(direction, _npc.transform.forward);
            direction.y = 0;
            _npc.transform.rotation = Quaternion.Slerp(_npc.transform.rotation, Quaternion.LookRotation(direction), _rotSpeed * Time.deltaTime);

            if (!CanAttackPlayer())
            {
                _nextState = new Idle(_npc, _agent, _anim, _player);
                _stage = EVENT.EXIT;
            }
        }
    }
}
