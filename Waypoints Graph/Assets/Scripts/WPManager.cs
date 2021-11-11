using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum direction { UNI, BI };

[System.Serializable]
public struct Link
{
    public GameObject node1;
    public GameObject node2;
    public direction dir;
}

public class WPManager : MonoBehaviour
{
    [field: SerializeField]
    public GameObject[] Waypoints { get; private set; }
    [SerializeField]
    private Link[] _links;

    public Graph GraphObj { get; private set; } = new Graph();
    // Start is called before the first frame update
    private void Start()
    {
        if (Waypoints.Length > 0)
        {
            foreach(var wp in Waypoints)
            {
                GraphObj.AddNode(wp);
            }
            foreach(var l in _links)
            {
                GraphObj.AddEdge(l.node1, l.node2);
                if (l.dir == direction.BI)
                {
                    GraphObj.AddEdge(l.node2, l.node1);
                }
            }
        }
    }

    // Update is called once per frame
    private void Update()
    {
        GraphObj.debugDraw();
    }
}
