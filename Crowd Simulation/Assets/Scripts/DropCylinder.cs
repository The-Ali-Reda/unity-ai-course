using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropCylinder : MonoBehaviour
{
    [SerializeField]
    private GameObject _obstacle;
    private GameObject[] _agents;
    // Start is called before the first frame update
    private void Start()
    {
        _agents = GameObject.FindGameObjectsWithTag("Agent");
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hitInfo;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray.origin, ray.direction, out hitInfo))
            {
                Instantiate(_obstacle, hitInfo.point, _obstacle.transform.rotation);
                foreach(var a in _agents)
                {
                    a.GetComponent<AIControl>().DetectNewObstacle(hitInfo.point);
                }
            }
        }
    }
}
