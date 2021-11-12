using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathMarker
{
    public MapLocation Location { get; private set; }
    public float G { get; private set; }
    public float H { get; private set; }
    public float F { get; private set; }
    public GameObject Marker { get; private set; }
    public PathMarker Parent { get; private set; }
    public PathMarker(MapLocation location, float G, float H, float F, GameObject marker, PathMarker parent)
    {
        this.Location = location;
        this.G = G;
        this.H = H;
        this.F = F;
        this.Marker = marker;
        this.Parent = parent;
    }
    public override bool Equals(object obj)
    {
        if (obj == null || !this.GetType().Equals(obj.GetType()))
            return false;
        PathMarker marker = obj as PathMarker;
        return Location.Equals(marker.Location);
    }
    public override int GetHashCode()
    {
        return 0;
    }
}

public class FindPathAStar : MonoBehaviour
{

    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        
    }
}
