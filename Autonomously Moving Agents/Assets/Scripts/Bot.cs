using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bot : MonoBehaviour
{
    private NavMeshAgent _agent;
    [SerializeField]
    private GameObject _target;
    private Drive _drive;
    // Start is called before the first frame update
    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _drive = _target.GetComponent<Drive>();
    }
    private void Seek(Vector3 location)
    {
        _agent.SetDestination(location);
    }
    private void Flee(Vector3 location)
    {
        var fleeVector = location - transform.position;
        _agent.SetDestination(transform.position - fleeVector);
    }
    private void Pursue()
    {
        var targetDir = _target.transform.position - transform.position;
        //angle between the two forward directions to determine the trajectory
        // <20 means they are relatively on the same trajectory
        var relativeHeading = Vector3.Angle(transform.forward, _target.transform.forward);
        //angle between the two of the objects to determine who is in front
        // >90 means pursuer (robber in this case) is in front rather than behind
        var toTarget = Vector3.Angle(transform.position, targetDir);
        //if the target is stopped or
        //if the robber is in front and is continuing ahead rather than turning, Seek 
        if(_drive.CurrentSpeed < 0.01f || (toTarget > 90 && relativeHeading < 20))
        {
            Seek(_target.transform.position);
            return;
        }
        var lookAhead = targetDir.magnitude / (_agent.speed + _drive.CurrentSpeed);
        Seek(_target.transform.position + _target.transform.forward*lookAhead );
    }

    private void Evade()
    {
        var targetDir = _target.transform.position - transform.position;
        var lookAhead = targetDir.magnitude / (_agent.speed + _drive.CurrentSpeed);
        var targetPosition = _target.transform.position + _target.transform.forward * lookAhead;
        Flee(targetPosition);
    }

    // Update is called once per frame
    private void Update()
    {
        Evade();
    }
}
