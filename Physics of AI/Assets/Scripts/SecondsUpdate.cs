using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondsUpdate : MonoBehaviour
{
    float _timeStartOffset = 0;
    bool _gotStartTime = false;
    void Update()
    {
        if (!_gotStartTime)
        {
            _timeStartOffset = Time.realtimeSinceStartup;
            _gotStartTime = true;
        }
        var newZ = (Time.realtimeSinceStartup - _timeStartOffset);
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, newZ);
    }
}
