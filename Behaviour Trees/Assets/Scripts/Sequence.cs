using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sequence : Node
{

    public Sequence(string name):base(name)
    {
    }
    public override Status Process()
    {
        Status childStatus = Children[_currentChild].Process();
        if (childStatus == Status.RUNNING)
            return Status.RUNNING;
        else if (childStatus == Status.FAILURE)
            return Status.FAILURE;
        _currentChild++;
        if(_currentChild >= Children.Count)
        {
            _currentChild = 0;
            return Status.SUCCESS;
        }
        return Status.RUNNING;
    }
}
