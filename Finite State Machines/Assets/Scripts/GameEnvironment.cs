using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnvironment : MonoBehaviour
{
    private static GameEnvironment _instance;
    public static GameEnvironment Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameEnvironment();
                var checkpts = GameObject.FindGameObjectsWithTag("Checkpoint");
                _instance.CheckPoints.AddRange(checkpts);
            }
            return _instance;
        }
    }
    public List<GameObject> CheckPoints { get; private set; } = new List<GameObject>();

    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        
    }
}
