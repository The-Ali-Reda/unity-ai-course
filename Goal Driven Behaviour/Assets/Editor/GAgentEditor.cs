using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

[CustomEditor(typeof(GAgentVisual))]
[CanEditMultipleObjects]
public class GAgentVisualEditor : Editor 
{


    void OnEnable()
    {

    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        serializedObject.Update();
        GAgentVisual agent = (GAgentVisual) target;
        GUILayout.Label("Name: " + agent.name);
        GUILayout.Label("Current Action: " + agent.gameObject.GetComponent<GAgent>().CurrentAction);
        GUILayout.Label("Actions: ");
        foreach (GAction a in agent.gameObject.GetComponent<GAgent>().Actions)
        {
            string pre = "";
            string eff = "";

            foreach (KeyValuePair<string, int> p in a.Preconditions)
                pre += p.Key + ", ";
            foreach (KeyValuePair<string, int> e in a.Posteffects)
                eff += e.Key + ", ";

            GUILayout.Label("====  " + a.ActionName + "(" + pre + ")(" + eff + ")");
        }
        GUILayout.Label("Goals: ");
        foreach (KeyValuePair<SubGoal, int> g in agent.gameObject.GetComponent<GAgent>().SubGoals)
        {
            GUILayout.Label("---: ");
            foreach (KeyValuePair<string, int> sg in g.Key.SubGoals)
                GUILayout.Label("=====  " + sg.Key);
        }
        serializedObject.ApplyModifiedProperties();
    }
}