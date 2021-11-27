using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateWorld : MonoBehaviour
{
    [SerializeField]
    private Text states;
    
    private void LateUpdate()
    {
        Dictionary<string, int> worldStates = GWorld.Instance.GetWorld().GetStates();
        states.text = "";
        foreach (var state in worldStates)
        {
            states.text += $"{state.Key}, {state.Value} {Environment.NewLine}";
        }
    }
}
