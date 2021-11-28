using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : Node
{
    public Selector(string name) : base(name)
    {
    }
    public override Status Process()
    {
        Status childStatus = Children[_currentChild].Process();
        if (childStatus == Status.RUNNING)
            return Status.RUNNING;
        if (childStatus == Status.SUCCESS)
        {
            _currentChild = 0;
            return Status.SUCCESS;
        }

        _currentChild++;
        if (_currentChild >= Children.Count)
        {
            _currentChild = 0;
            return Status.FAILURE;
        }
        return Status.RUNNING;
    }
}
