using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ChangeColor))] // Replace 'Flame' with the name of your MonoBehaviour script
public class ChangeColorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        ChangeColor colorScript = (ChangeColor)target;

        // Add a button to the Inspector
        if (GUILayout.Button("Call Coroutine"))
        {
            colorScript.ColorChange(); // Call your coroutine here
        }
    }
}
