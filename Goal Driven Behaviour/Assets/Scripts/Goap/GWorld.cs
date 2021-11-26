using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class GWorld
{
    private static readonly GWorld _instance = new GWorld();
    private static WorldStates _world;
    public static GWorld Instance { get { return _instance; } }
    static GWorld()
    {
        _world = new WorldStates();
    }
    private GWorld() { }
    public WorldStates GetWorld() { 
        return _world;
    }
}
