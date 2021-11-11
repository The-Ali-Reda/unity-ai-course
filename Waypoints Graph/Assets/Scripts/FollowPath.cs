using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour
{
    private Transform _goal;
    private float _speed = 5f;
    private float _rotSpeed = 5f;
    [SerializeField]
    private GameObject _wpManager;
    private GameObject[] _wps;
    private GameObject _currentNode;
    private int _currentWP = 0;
    private Graph _graph;
    private float _accuracy = 1.0f;
    // Start is called before the first frame update
    private void Start()
    {
        _wps = _wpManager.GetComponent<WPManager>().Waypoints;
        _graph = _wpManager.GetComponent<WPManager>().GraphObj;
        _currentNode = _wps[0];
    }
    public void GoToRocks()
    {
        _graph.AStar(_currentNode, _wps[0]);
        _currentWP = 0;
    }
    public void GoToResidence()
    {
        _graph.AStar(_currentNode, _wps[1]);
        _currentWP = 0;
    }
    public void GoToBranch()
    {
        _graph.AStar(_currentNode, _wps[2]);
        _currentWP = 0;
    }
    public void GoToFactoryFront()
    {
        _graph.AStar(_currentNode, _wps[5]);
        _currentWP = 0;
    }
    public void GoToFactorySide()
    {
        _graph.AStar(_currentNode, _wps[6]);
        _currentWP = 0;
    }
    public void GoToFactoryBackCorner()
    {
        _graph.AStar(_currentNode, _wps[7]);
        _currentWP = 0;
    }
    public void GoToFactoryBack()
    {
        _graph.AStar(_currentNode, _wps[8]);
        _currentWP = 0;
    }
    public void GoToFactoryOutskirts()
    {
        _graph.AStar(_currentNode, _wps[4]);
        _currentWP = 0;
    }
    public void GoToResidenceOutskirts()
    {
        _graph.AStar(_currentNode, _wps[3]);
        _currentWP = 0;
    }
    // Update is called once per frame
    private void LateUpdate()
    {
        if (_graph.getPathLength() == 0 || _currentWP == _graph.getPathLength())
            return;
        _currentNode = _graph.getPathPoint(_currentWP);
        if(Vector3.Distance(_graph.getPathPoint(_currentWP).transform.position, transform.position) < _accuracy)
        {
            _currentWP++;
        }
        if(_currentWP < _graph.getPathLength())
        {
            _goal = _graph.getPathPoint(_currentWP).transform;
            Vector3 lookAtGoal = new Vector3(_goal.position.x, this.transform.position.y, _goal.position.z);
            Vector3 direction = lookAtGoal - this.transform.position;
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * _rotSpeed);
            this.transform.Translate(0, 0, _speed * Time.deltaTime);
        }
    }
}
