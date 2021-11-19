using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class World
{
    public static readonly World _instance = new World();
    public static World Instance { get { return _instance; } }
    private static GameObject[] _hidingSpots;
    static World()
    {
        _hidingSpots = GameObject.FindGameObjectsWithTag("Hide");
    }
    private World() { }
    public GameObject[] GetHidingSpots()
    {
        return _hidingSpots;
    }
}
