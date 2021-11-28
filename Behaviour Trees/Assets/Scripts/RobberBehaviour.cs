using System;
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
    [SerializeField]
    private GameObject _frontDoor;
    [SerializeField]
    private GameObject _backDoor;
    [SerializeField]
    [Range(0, 1000)]
    private int _money = 800;

    private NavMeshAgent _agent;
    public enum ActionState { IDLE, WORKING};
    ActionState _state = ActionState.IDLE;
    Node.Status _treeStatus = Node.Status.RUNNING;
    // Start is called before the first frame update
    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _tree = new BehaviourTree();
        Sequence steal = new Sequence("Steal Something");
        Leaf goToDiamond = new Leaf("Go To Diamond", GoToDiamond);
        Leaf goToVan = new Leaf("Go To Van", GoToVan);
        Leaf goToBackDoor = new Leaf("Go To Back Door", GoToBackDoor);
        Leaf goToFrontDoor = new Leaf("Go To Front Door", GoToFrontDoor);
        Selector openDoor = new Selector("Open Door");
        Condition hasMoney = new Condition("Has Money?", ()=> _money<500);
        openDoor.AddChild(goToFrontDoor);
        openDoor.AddChild(goToBackDoor);
        steal.AddChild(hasMoney);
        steal.AddChild(openDoor);
        steal.AddChild(goToDiamond);
        steal.AddChild(goToVan);
        _tree.AddChild(steal);

        _tree.PrintTree();
        _tree.Process();
    }
    private Node.Status CheckMoney()
    {
        if (_money < 500)
            return Node.Status.SUCCESS;
        return Node.Status.FAILURE;
    }
    private Node.Status GoToFrontDoor()
    {
        return GoToDoor(_frontDoor);
    }

    private Node.Status GoToBackDoor()
    {
        return GoToDoor(_backDoor);
    }

    public Node.Status GoToDoor(GameObject door)
    {
        Node.Status status = GoToLocaton(door.transform.position);
        if(status == Node.Status.SUCCESS)
        {
            var isLocked = door.GetComponent<Lock>().isLocked;
            if (!isLocked)
            {
                door.SetActive(false);
                return Node.Status.SUCCESS;
            }
            return Node.Status.FAILURE;
        }
        return status;
    }

    public Node.Status GoToVan()
    {
        Node.Status status = GoToLocaton(_van.transform.position);
        if (status == Node.Status.SUCCESS)
            _money += 300;
        return status;
    }
    public Node.Status GoToDiamond()
    {
        Node.Status status = GoToLocaton(_diamond.transform.position);
        if(status==Node.Status.SUCCESS)
            _diamond.SetActive(false);
        return status;
    }
    public Node.Status GoToLocaton(Vector3 destination)
    {
        float distance = Vector3.Distance(destination, transform.position);
        if (_state == ActionState.IDLE) { 
            _agent.SetDestination(destination); 
            _state = ActionState.WORKING;
        } else if(Vector3.Distance(_agent.pathEndPosition, destination) >= 4)
        {
            _state = ActionState.IDLE;
            return Node.Status.FAILURE;
        } else if(distance < 4)
        {
            _state = ActionState.IDLE;
            return Node.Status.SUCCESS;
        }
        return Node.Status.RUNNING;
    }
    // Update is called once per frame
    private void Update()
    {
        if (_treeStatus != Node.Status.SUCCESS)
            _treeStatus = _tree.Process();
    }
}
