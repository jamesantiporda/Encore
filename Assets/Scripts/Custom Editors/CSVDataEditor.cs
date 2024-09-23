using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CSVData))]
public class CSVDataEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        var script = (CSVData)target;

        if (GUILayout.Button("Initialize Values", GUILayout.Height(40)))
        {
            script.InitializeValues();
        }
    }
}
