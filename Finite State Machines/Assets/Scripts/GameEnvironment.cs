using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameEnvironment
{
    private static GameEnvironment _instance;
    public static GameEnvironment Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameEnvironment();
                var checkpts = GameObject.FindGameObjectsWithTag("Checkpoint").OrderBy(x=> x.name).ToList();
                _instance.SafeCube = GameObject.FindGameObjectWithTag("Safe");
                _instance.CheckPoints.AddRange(checkpts);
            }
            return _instance;
        }
    }
    public List<GameObject> CheckPoints { get; private set; } = new List<GameObject>();
    public GameObject SafeCube { get; private set; }
    public GameObject GetClosestWaypoint(Vector3 position)
    {
        return CheckPoints.OrderBy(x => Vector3.Distance(x.transform.position, position)).First();
    }
    public int GetClosestWaypointIndex(Vector3 position)
    {
        var gameObj = GetClosestWaypoint(position);
        return CheckPoints.IndexOf(gameObj);
    }
}
