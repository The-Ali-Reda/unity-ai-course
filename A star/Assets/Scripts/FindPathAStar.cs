using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PathMarker
{
    public MapLocation Location { get; private set; }
    public float G { get; set; }
    public float H { get; set; }
    public float F { get; set; }
    public GameObject Marker { get; set; }
    public PathMarker Parent { get; set; }
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
    [SerializeField]
    private Maze _maze;
    [SerializeField]
    private Material _closedMaterial;
    [SerializeField]
    private Material _openMaterial;
    [SerializeField]
    private GameObject _Start;
    [SerializeField]
    private GameObject _end;
    [SerializeField]
    private GameObject _pathP;

    private List<PathMarker> _open = new List<PathMarker>();
    private List<PathMarker> _closed = new List<PathMarker>();

    private PathMarker _goalNode;
    private PathMarker _startNode;
    private PathMarker _lastPosition;
    private bool _done = false;
    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) 
            BeginSearch();
        if (Input.GetKeyDown(KeyCode.C) && !_done)
            Search(_lastPosition);
        if (Input.GetKeyDown(KeyCode.M) && _done)
        {
            Debug.Log($"I am here");
            GetPath();
        }
    }
    private void GetPath()
    {
        RemoveAllMarkers();
        var begin = _lastPosition;
        Debug.Log($"hmmm am I null? {begin.Location}");
        while (!_startNode.Equals(begin) && begin != null)
        {
            var pathBlock = Instantiate(_pathP, new Vector3(begin.Location.x * _maze.scale, 0, begin.Location.z * _maze.scale), Quaternion.identity);
            var values = pathBlock.GetComponentsInChildren<TextMesh>();
            values[0].text = $"G: {begin.G:0.00}";
            values[1].text = $"H: {begin.H:0.00}";
            values[2].text = $"F: {begin.F:0.00}";
            begin = begin.Parent;
        }
        Instantiate(_pathP, new Vector3(_startNode.Location.x * _maze.scale, 0, _startNode.Location.z * _maze.scale), Quaternion.identity);

    }
    private void RemoveAllMarkers()
    {
        var markers = GameObject.FindGameObjectsWithTag("marker");
        foreach(var marker in markers)
        {
            Destroy(marker);
        }
    }
    private void BeginSearch()
    {
        _done = false;
        RemoveAllMarkers();
        List<MapLocation> locations = new List<MapLocation>();
        for(var z =1; z < _maze.depth-1; z++)
        {
            for (var x = 1; x < _maze.width-1; x++)
            {
                if (_maze.map[x, z] != 1)
                    locations.Add(new MapLocation(x, z));
            }
        }
        locations.Shuffle();
        Vector3 startLocation = new Vector3(locations[0].x *_maze.scale, 0, locations[0].z * _maze.scale);
        Vector3 endLocation = new Vector3(locations[1].x * _maze.scale, 0, locations[1].z * _maze.scale);
        var marker = Instantiate(_Start, startLocation, Quaternion.identity);
        _startNode = new PathMarker(new MapLocation(locations[0].x, locations[0].z), 0, 0, 0, marker, null);
        var endmarker = Instantiate(_end, endLocation, Quaternion.identity);
        _goalNode = new PathMarker(new MapLocation(locations[1].x, locations[1].z), 0, 0, 0, endmarker, null);

        _open.Clear();
        _closed.Clear();
        _open.Add(_startNode);
        _lastPosition = _startNode;

    }

    private void Search(PathMarker thisNode)
    {
        if (thisNode.Equals(_goalNode))
        {
            _done = true;
            return;
        }
        foreach(var dir in _maze.directions)
        {
            var neighbor = dir + thisNode.Location;
            if (_maze.map[neighbor.x, neighbor.z] == 1)
                continue;
            if (neighbor.x < 1 || neighbor.x >= _maze.width || neighbor.z < 1 || neighbor.z >= _maze.depth)
                continue;
            if (IsClosed(neighbor))
                continue;
            float G = Vector2.Distance(thisNode.Location.ToVector(), neighbor.ToVector()) + thisNode.G;
            float H = Vector2.Distance(neighbor.ToVector(), _goalNode.Location.ToVector());
            float F = G + H;
            var pathBlock = Instantiate(_pathP, new Vector3(neighbor.x * _maze.scale, 0, neighbor.z * _maze.scale), Quaternion.identity);
            var values = pathBlock.GetComponentsInChildren<TextMesh>();
            values[0].text = $"G: {G:0.00}";
            values[1].text = $"H: {H:0.00}";
            values[2].text = $"F: {F:0.00}";
            if(!UpdateMarker(neighbor, G, H, F, thisNode))
            {
                _open.Add(new PathMarker(neighbor, G, H, F, pathBlock, thisNode));
            }
        }
        _open = _open.OrderBy(p => p.F).ToList();
        var pm = _open[0];
        _closed.Add(pm);
        _open.RemoveAt(0);
        pm.Marker.GetComponent<Renderer>().material = _closedMaterial;
        _lastPosition = pm;
    }

    private bool UpdateMarker(MapLocation pos, float G, float H, float F, PathMarker prt)
    {
        foreach (var p in _open)
        {
            if (p.Location.Equals(pos))
            {
                p.G = G;
                p.H = H;
                p.F = F;
                p.Parent = prt;
                return true;
            }
        }
        return false;
    }

    private bool IsClosed(MapLocation marker)
    {
        foreach(var p in _closed)
        {
            if (p.Location.Equals(marker))
                return true;
        }
        return false;
    }
}
