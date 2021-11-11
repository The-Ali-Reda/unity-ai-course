using UnityEngine;

public class FollowWP : MonoBehaviour
{

    public GameObject[] waypoints;
    int currentWP = 0;
    public float speed = 10.0f;
    public float rotSpeed = 10.0f;
    public float lookAhead = 10.0f;

    // Store a tracker that the tanks will follow
    GameObject tracker;

    void Start()
    {

        tracker = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        DestroyImmediate(tracker.GetComponent<Collider>());
        tracker.transform.position = this.transform.position;
        tracker.transform.rotation = this.transform.rotation;
    }

    void ProcessTracker()
    {

        if (Vector3.Distance(tracker.transform.position, this.transform.position) > lookAhead) return;

        if (Vector3.Distance(tracker.transform.position, waypoints[currentWP].transform.position) < 3.0f)
        {
            currentWP++;
        }
        if (currentWP >= waypoints.Length)
        {
            currentWP = 0;
        }
        tracker.transform.LookAt(waypoints[currentWP].transform);
        tracker.transform.Translate(0.0f, 0.0f, (speed + 20.0f) * Time.deltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        ProcessTracker();
        Quaternion lookAtWP = Quaternion.LookRotation(tracker.transform.position - this.transform.position);
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, lookAtWP, rotSpeed * Time.deltaTime);
        this.transform.Translate(0.0f, 0.0f, speed * Time.deltaTime);
    }
}