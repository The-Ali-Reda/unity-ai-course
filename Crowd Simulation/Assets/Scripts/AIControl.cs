using UnityEngine;
using UnityEngine.AI;

public class AIControl : MonoBehaviour {

    GameObject[] goalLocations;
    UnityEngine.AI.NavMeshAgent agent;
    Animator anim;
    float _speedMult;
    float _detectionRadius = 5;
    float _fleeRadius = 15;

    void ResetAgent()
    {
        _speedMult = Random.Range(0.1f, 1.5f);
        agent.speed = 2 * _speedMult;
        agent.angularSpeed = 120;
        anim.SetFloat("speedMult", _speedMult);
        anim.SetTrigger("isWalking");
        agent.ResetPath();
    }

    // Use this for initialization
    void Start() {
        goalLocations = GameObject.FindGameObjectsWithTag("goal");
        agent = this.GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.SetDestination(goalLocations[Random.Range(0, goalLocations.Length)].transform.position);
        anim = this.GetComponent<Animator>();
        anim.SetFloat("wOffset", Random.Range(0.0f, 1.0f));
        ResetAgent();
    }

    // Update is called once per frame
    void Update() {

        if (agent.remainingDistance < 1) {
            ResetAgent();
            agent.SetDestination(goalLocations[Random.Range(0, goalLocations.Length)].transform.position);
        }
    }
    public void DetectNewObstacle(Vector3 position)
    {
        if(Vector3.Distance(position, transform.position) < _detectionRadius)
        {
            var fleeDirection = (transform.position - position).normalized;
            var newGoal = transform.position + fleeDirection * _fleeRadius;
            var path = new NavMeshPath();
            agent.CalculatePath(newGoal, path);
            if(path.status != NavMeshPathStatus.PathInvalid)
            {
                agent.SetDestination(path.corners[path.corners.Length-1]);
                anim.SetTrigger("isRunning");
                agent.speed = 10;
                agent.angularSpeed = 500;
            }
        }
    }
}
