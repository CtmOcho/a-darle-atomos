using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.Rendering;
using UnityEngine.VFX;

public class flame : MonoBehaviour
{
    private VisualEffect vfx;
    private new Light light;
    private int currentColor;
    private float blend;
    [SerializeField]
    private Transform knob;
    public float flameStrength;
    public int targetColor;
    
    bool knobIsPressed;
    bool isOn;

    // Start is called before the first frame update
    void Start()
    {
        vfx = GetComponent<VisualEffect>();
        light = GetComponentInChildren<Light>();
        currentColor = vfx.GetInt("Color value");
        vfx.SetInt("Color value", 7);
        vfx.SetFloat("Blend", 0);
    }
    
    // Update is called once per frame
    void Update()
    {
        /*
        if (knob.hasChanged)
        {
            Vector3 knob_euler_angles = knob.transform.localEulerAngles;
            float knob_rotation_normalized = Mathf.InverseLerp(10f, 80f, knob_euler_angles.x);
            float value = knob_rotation_normalized * 8f;
            if (value < 0.2f){
                vfx.SetInt("spawn_rate", 0);
                light.intensity = 0;
            } else {
                vfx.SetInt("spawn_rate", 100000);
                vfx.SetFloat("Flame Strength", knob_rotation_normalized);
                light.intensity = Mathf.Lerp(100, 600, knob_rotation_normalized);
                knob.hasChanged = false;
            }
        }
        */
        if (knobIsPressed)
        {
            if (!isOn)
            {
                vfx.SetInt("spawn_rate", 0);
                light.intensity = 0;
            }
            else
            {
                vfx.SetInt("spawn_rate", 100000);
                vfx.SetFloat("Flame Strength", 1);
                light.intensity = Mathf.Lerp(100, 200, 1);
            }
        }
    }

    public void KnobState(bool knobState)
    {
        knobIsPressed = knobState;
        if(knobState) isOn = !isOn;
    }
    
    void OnTriggerEnter(Collider other){
        FireReagentController spoon = other.GetComponent<FireReagentController>();
            if (spoon.colorLlama >= 0){
                StartCoroutine(ChangeFlameColor(spoon.colorLlama));
                StartCoroutine(ReturnToRegularColor(8));
                spoon.colorLlama = -1;
            }
    }
    
    public IEnumerator ChangeFlameColor(int targetColor){
        blend = 0;
        vfx.SetInt("Target Color", targetColor);
        while (blend < 1){
            blend += 0.01f;
            vfx.SetFloat("Blend", blend);
            light.color = Color.Lerp(fireColor(currentColor), fireColor(targetColor), blend);
            yield return new WaitForFixedUpdate();
        }
        currentColor = targetColor;
        vfx.SetInt("Color value", targetColor);
    }
    
    IEnumerator ReturnToRegularColor(float delay){
        yield return new WaitForSeconds(delay);
        StartCoroutine(ChangeFlameColor(7));
    }
    
    Color fireColor(int id){
        switch(id){
            case 0:
                return Color.green; 
            case 1:
                return Color.red; 
            case 2:
                return Color.yellow; 
            case 3:
                return Color.blue; 
            case 4:
                return Color.red; 
            case 5:
                return Color.white; 
            case 6:
                return Color.magenta; 
            default:
                return Color.white;
        }
    }
}
