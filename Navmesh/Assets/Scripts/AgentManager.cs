using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentManager : MonoBehaviour
{
    GameObject[] _agents;
    // Start is called before the first frame update
    private void Start()
    {
        _agents = GameObject.FindGameObjectsWithTag("AI");
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
            {
                foreach(var agent in _agents)
                {
                    agent.GetComponent<AIControl>()._agent.SetDestination(hit.point);
                }
            }
        }
    }
}
