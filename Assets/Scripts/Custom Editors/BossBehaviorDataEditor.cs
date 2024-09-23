using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BossBehaviorData))]
public class BossBehaviorDataEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        var script = (BossBehaviorData)target;

        if (GUILayout.Button("Generate Segment Attacks and Power ups from CSV", GUILayout.Height(40)))
        {
            script.GenerateSegmentAttacksFromCSV();
        }
    }
}
