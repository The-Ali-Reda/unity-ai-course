using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Node
{
    public enum Status { SUCCESS, RUNNING, FAILURE};
    protected Status _status;
    public List<Node> Children { get; protected set; } = new List<Node>();
    protected int _currentChild = 0;
    public string Name { get; protected set; }
    public Node()
    {

    }
    public Node(string name)
    {
        Name = name;
    }
    public virtual Status Process() {
        return Children[_currentChild].Process(); 
    }
    public void AddChild(Node n)
    {
        Children.Add(n);
    }
}
