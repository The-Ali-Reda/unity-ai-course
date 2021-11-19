using UnityEngine;
using UnityEngine.AI;

public class Bot : MonoBehaviour
{
    private NavMeshAgent _agent;
    [SerializeField]
    private GameObject _target;
    private Drive _drive;
    private Vector3 _wanderTarget = Vector3.zero;
    private bool _cooldown = false;
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
        if (_drive.CurrentSpeed < 0.01f || (toTarget > 90 && relativeHeading < 20))
        {
            Seek(_target.transform.position);
            return;
        }
        var lookAhead = targetDir.magnitude / (_agent.speed + _drive.CurrentSpeed);
        Seek(_target.transform.position + _target.transform.forward * lookAhead);
    }

    private void Evade()
    {
        var targetDir = _target.transform.position - transform.position;
        var lookAhead = targetDir.magnitude / (_agent.speed + _drive.CurrentSpeed);
        var targetPosition = _target.transform.position + _target.transform.forward * lookAhead;
        Flee(targetPosition);
    }

    private void Wander()
    {
        float wanderRadius = 10;
        float wanderDistance = 20;
        float wanderJitter = 1;
        _wanderTarget += new Vector3(Random.Range(-1.0f, 1.0f) * wanderJitter, 0, Random.Range(-1.0f, 1.0f) * wanderJitter);
        _wanderTarget.Normalize();
        _wanderTarget *= wanderRadius;
        Vector3 targetLocal = _wanderTarget + new Vector3(0, 0, wanderDistance);
        Vector3 targetWorld = gameObject.transform.TransformVector(targetLocal);
        Seek(targetWorld);
    }

    private void Hide()
    {
        float distance = Mathf.Infinity;
        Vector3 chosenSpot = Vector3.zero;
        for (int i = 0; i < World.Instance.GetHidingSpots().Length; i++)
        {
            Vector3 hideDirection = World.Instance.GetHidingSpots()[i].transform.position - _target.transform.position;
            Vector3 hidePos = World.Instance.GetHidingSpots()[i].transform.position + hideDirection.normalized * 5;
            if (Vector3.Distance(transform.position, hidePos) < distance)
            {
                chosenSpot = hidePos;
                distance = Vector3.Distance(transform.position, hidePos);
            }
        }
        Seek(chosenSpot);
    }

    private void CleverHide()
    {
        float distance = Mathf.Infinity;
        Vector3 chosenSpot = Vector3.zero;
        Vector3 chosenDirection = Vector3.zero;
        var chosenGO = World.Instance.GetHidingSpots()[0];
        for (int i = 0; i < World.Instance.GetHidingSpots().Length; i++)
        {
            Vector3 hideDirection = World.Instance.GetHidingSpots()[i].transform.position - _target.transform.position;
            Vector3 hidePos = World.Instance.GetHidingSpots()[i].transform.position + hideDirection.normalized * 5;
            if (Vector3.Distance(transform.position, hidePos) < distance)
            {
                chosenSpot = hidePos;
                chosenDirection = hideDirection;
                chosenGO = World.Instance.GetHidingSpots()[i];
                distance = Vector3.Distance(transform.position, hidePos);
            }
        }
        Collider hideCol = chosenGO.GetComponent<Collider>();
        Ray backRay = new Ray(chosenSpot, -chosenDirection.normalized);
        RaycastHit info;
        float rayDist = 100.0f;
        hideCol.Raycast(backRay, out info, rayDist);
        var endPoint = info.point + chosenDirection.normalized * 5;
        Seek(endPoint);
    }

    private bool CanSeeTarget()
    {
        RaycastHit info;
        Vector3 rayToTarget = _target.transform.position - transform.position;
        if (Physics.Raycast(transform.position, rayToTarget, out info))
        {
            if (info.transform.tag == "Cop")
                return true;
        }
        return false;
    }

    private bool CanSeeMe()
    {
        Vector3 toAgent = transform.position - _target.transform.position;
        float lookingAngle = Vector3.Angle(_target.transform.forward, toAgent);
        if (lookingAngle < 60)
            return true;
        return false;
    }
    private void BehaviorCooldown()
    {
        _cooldown = false;
    }
    private bool IsTargetInRange()
    {
        float dist = Vector3.Distance(_target.transform.position, transform.position);
        return dist <= 10;
    }
    // Update is called once per frame
    private void Update()
    {
        if (_cooldown)
            return;
        if (!IsTargetInRange())
            Wander();
        else if (CanSeeTarget() && CanSeeMe())
        {
            CleverHide();
            _cooldown = true;
            Invoke("BehaviorCooldown", 5);
        }
        else
            Pursue();

    }
}
