using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    private NavMeshAgent _agent;
    private Animator _animator;
    [SerializeField]
    private Transform _player;
    private State _currentState;
    // Start is called before the first frame update
    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _currentState = new Idle(gameObject, _agent, _animator, _player);
    }

    // Update is called once per frame
    private void Update()
    {
        _currentState = _currentState.process();
    }
}
