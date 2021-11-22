using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
    [SerializeField]
    private FlockManager _flockManager;
    private float _speed;
    private bool turning = false;
    // Start is called before the first frame update
    private void Start()
    {
        _flockManager = FindObjectOfType<FlockManager>();
        _speed = Random.Range(_flockManager.MinSpeed, _flockManager.MaxSpeed);
    }

    // Update is called once per frame
    private void Update()
    {
        Bounds b = new Bounds(_flockManager.transform.position, _flockManager.SwimLimits * 2) ;
        RaycastHit hit;
        var direction = _flockManager.transform.position - transform.position;

        if (!b.Contains(transform.position))
        {
            turning = true;
            Debug.DrawLine(transform.position, transform.forward * 50, Color.red);
        } else if (Physics.Raycast(transform.position, transform.forward * 50, out hit))
        {
            turning = true;
            direction = Vector3.Reflect(transform.forward, hit.normal);
        }
        else
        {
            turning = false;
        }
        if (turning)
        {
            
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), _flockManager.RotationSpeed * Time.deltaTime);
        }
        else
        {
            if (Random.Range(0, 100) < 10)
                _speed = Random.Range(_flockManager.MinSpeed, _flockManager.MaxSpeed);
            if (Random.Range(0, 100) < 20)
                ApplyRules();
        }
        transform.Translate(0,0, _speed*Time.deltaTime);
    }
    private void ApplyRules()
    {
        var gos = _flockManager.AllFish;
        var vcentre = Vector3.zero;
        var vavoid = Vector3.zero;
        float gSpeed = 0.1f;
        float nDistance;
        int groupSize = 0;
        foreach(var go in gos)
        {
            if(go!= gameObject)
            {
                nDistance = Vector3.Distance(go.transform.position, transform.position);
                if(nDistance <= _flockManager.NeighborDistance)
                {
                    vcentre += go.transform.position;
                    groupSize++;
                    if (nDistance < 1.0f)
                    {
                        vavoid += transform.position-go.transform.position;
                    }
                    Flock anotherFlock = go.GetComponent<Flock>();
                    gSpeed = gSpeed + anotherFlock._speed;
                }
            }
        }
        if(groupSize > 0)
        {
            vcentre = vcentre / groupSize + (_flockManager.GoalPos - transform.position);
            _speed = gSpeed / groupSize;
            var direction = (vcentre + vavoid) - transform.position;
            if(direction != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), _flockManager.RotationSpeed*Time.deltaTime);
            }
        }
    }
}
