using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.VFX;

public class flame : MonoBehaviour
{
    private int regularColor = 7;
    private VisualEffect vfx;
    private int currentColor;
    private float blend;
    public Transform knob;
    public float flameStrength;
    public int targetColor;
    

    // Start is called before the first frame update
    void Start()
    {
        vfx = GetComponent<VisualEffect>();
        currentColor = vfx.GetInt("Color value");
        vfx.SetInt("Color value", 7);
        vfx.SetFloat("Blend", 0);
    }
    
    // Update is called once per frame
    void Update()
    {
        if (knob.hasChanged)
        {
            Vector3 knob_euler_angles = knob.transform.localEulerAngles;
            float knob_rotation_normalized = Mathf.InverseLerp(10f, 80f, knob_euler_angles.x);
            float value = knob_rotation_normalized * 8f;
            if (value < 0.2f){
                vfx.SetInt("spawn_rate", 0);
            } else {
                vfx.SetInt("spawn_rate", 100000);
            }
            vfx.SetFloat("Flame Strength", knob_rotation_normalized);
            knob.hasChanged = false;
        }
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
            yield return new WaitForFixedUpdate();
        }
        vfx.SetInt("Color value", targetColor);
    }
    
    IEnumerator ReturnToRegularColor(float delay){
        yield return new WaitForSeconds(delay);
        blend = 0;
        vfx.SetInt("Target Color", regularColor);
        while (blend < 1){
            blend += 0.01f;
            vfx.SetFloat("Blend", blend);
            yield return new WaitForFixedUpdate();
        }
        vfx.SetInt("Color value", regularColor);
    }
}
