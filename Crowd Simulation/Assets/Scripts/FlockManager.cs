using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _fishPrefab;
    [SerializeField]
    private int _numFish = 20;
    private GameObject[] _allFish;
    [SerializeField]
    private Vector3 _swimLimits = new Vector3(5,5,5);
    [SerializeField]
    [Header("Fish Settings")]
    [Range(0, 5)]
    private float _minSpeed;
    [SerializeField]
    [Range(0, 5)]
    private float _maxSpeed;
    [SerializeField]
    [Range(0, 10)]
    private float _neighborDistance;
    [SerializeField]
    [Range(0, 5)]
    private float _rotationSpeed;

    private Vector3 _goalPos;
    public float MinSpeed { get { return _minSpeed; } }
    public float MaxSpeed { get { return _maxSpeed; } }
    public float NeighborDistance { get { return _neighborDistance; } }
    public float RotationSpeed { get { return _rotationSpeed; } }
    public GameObject[] AllFish { get { return _allFish; } }
    public Vector3 GoalPos { get { return _goalPos; } }
    public Vector3 SwimLimits { get { return _swimLimits; } }
    // Start is called before the first frame update
    private void Start()
    {
        _allFish = new GameObject[_numFish];
        for (int i = 0; i < _numFish; i++)
        {
            Vector3 pos = transform.position + new Vector3(Random.Range(-_swimLimits.x, _swimLimits.x)
                , Random.Range(-_swimLimits.y, _swimLimits.y),
                Random.Range(-_swimLimits.z, _swimLimits.z));
            _allFish[i] = Instantiate(_fishPrefab,pos,Quaternion.identity);
            _goalPos = transform.position;
        }
        
    }

    // Update is called once per frame
    private void Update()
    {
        if(Random.Range(0,100)<10)
            _goalPos = transform.position + new Vector3(Random.Range(-_swimLimits.x, _swimLimits.x)
                    , Random.Range(-_swimLimits.y, _swimLimits.y),
                    Random.Range(-_swimLimits.z, _swimLimits.z));
    }
}
