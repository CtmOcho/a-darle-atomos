using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.VFX;

public class flame : MonoBehaviour
{
    private VisualEffect vfx;
    private int currentColor;
    private float blend;
    public float flameStrength;
    public int targetColor;

    // Start is called before the first frame update
    void Start()
    {
        vfx = GetComponent<VisualEffect>();
        currentColor = vfx.GetInt("Color value");
        blend = vfx.GetFloat("Blend");
        flameStrength = vfx.GetFloat("Flame Strength");
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator ChangeFlameColor(int targetColor){
        vfx.SetInt("Target Color", targetColor);
        while (blend < 1){
            blend += 0.01f;
            vfx.SetFloat("Blend", blend);
            yield return new WaitForFixedUpdate();
        }
        vfx.SetInt("Color value", targetColor);
        blend = 0;
    }
}
