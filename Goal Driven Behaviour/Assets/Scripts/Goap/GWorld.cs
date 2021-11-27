using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class GWorld
{
    private static readonly GWorld _instance = new GWorld();
    private static WorldStates _world;
    private static Queue<GameObject> _patients;
    private static Queue<GameObject> _cubicles;
    
    public static GWorld Instance { get { return _instance; } }
    static GWorld()
    {
        var cubs = GameObject.FindGameObjectsWithTag("Cubicle");
        _cubicles = new Queue<GameObject>(cubs);
        _patients = new Queue<GameObject>();
        _world = new WorldStates();
        _world.ModifyState("FreeCubicle", _cubicles.Count);
    }
    private GWorld() { }
    public WorldStates GetWorld() { 
        return _world;
    }
    public void AddPatient(GameObject p)
    {
        _patients.Enqueue(p);
    }
    public GameObject RemovePatient()
    {
        if(_patients.Count > 0 )
            return _patients.Dequeue();
        return null;
    }
    public void AddCubicle(GameObject c)
    {
        _cubicles.Enqueue(c);
    }
    public GameObject RemoveCubicle()
    {
        if (_cubicles.Count > 0)
            return _cubicles.Dequeue();
        return null;
    }
}
