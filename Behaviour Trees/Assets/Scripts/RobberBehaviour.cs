using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RobberBehaviour : MonoBehaviour
{
    private BehaviourTree _tree;
    [SerializeField]
    private GameObject _diamond;
    [SerializeField]
    private GameObject _van;
    private NavMeshAgent _agent;
    public enum ActionState { IDLE, WORKING};
    ActionState _state = ActionState.IDLE;
    // Start is called before the first frame update
    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _tree = new BehaviourTree();
        Node steal = new Node("Steal Something");
        Leaf goToDiamond = new Leaf("Go To Diamond", GoToDiamond);
        Leaf goToVan = new Leaf("Go To Van", GoToVan);
        steal.AddChild(goToDiamond);
        steal.AddChild(goToVan);
        _tree.AddChild(steal);

        _tree.PrintTree();
        _tree.Process();
    }
    public Node.Status GoToVan()
    {
        return GoToLocaton(_van.transform.position);
    }
    public Node.Status GoToDiamond()
    {
        return GoToLocaton(_diamond.transform.position);
    }
    public Node.Status GoToLocaton(Vector3 destination)
    {
        float distance = Vector3.Distance(destination, transform.position);
        if (_state == ActionState.IDLE) { 
            _agent.SetDestination(destination); 
            _state = ActionState.WORKING;
        } else if(Vector3.Distance(_agent.pathEndPosition, transform.position) >= 2)
        {
            _state = ActionState.IDLE;
            return Node.Status.FAILURE;
        } else if(Vector3.Distance(_agent.pathEndPosition, transform.position) < 2)
        {
            _state = ActionState.IDLE;
            return Node.Status.SUCCESS;
        }
        return Node.Status.RUNNING;
    }
    // Update is called once per frame
    private void Update()
    {
        
    }
}
