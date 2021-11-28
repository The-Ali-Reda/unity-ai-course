using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Condition : Node
{
    private Func<bool> _predicate;
    public Condition(string name, Func<bool> predicate) : base(name)
    {
        _predicate = predicate;
    }

    public override Status Process()
    {

        if (_predicate())
            return Status.SUCCESS;
        return Status.FAILURE;
    }
}
