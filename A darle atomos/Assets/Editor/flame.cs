using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(flame))] // Replace 'Flame' with the name of your MonoBehaviour script
public class FlameEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        flame flameScript = (flame)target;

        // Add a button to the Inspector
        if (GUILayout.Button("Call Coroutine"))
        {
            flameScript.StartCoroutine(flameScript.ChangeFlameColor(flameScript.targetColor)); // Call your coroutine here
        }
    }
}
