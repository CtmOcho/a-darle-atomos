using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.Rendering;
using UnityEngine.VFX;
using UnityEngine.Rendering.Universal;

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
        TurnOnAndOff();
    }

    void TurnOnAndOff()
    {
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
                light.intensity = 0.015f;
            }
        }
    }

    public void KnobState(bool knobState)
    {
        knobIsPressed = knobState;
        if (knobState) isOn = !isOn;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<FireReagentController>(out var spoon))
        {
            if (spoon.colorLlama >= 0)
            {
                StartCoroutine(ChangeFlameColor(spoon.colorLlama));
                StartCoroutine(ReturnToRegularColor(8));
                spoon.colorLlama = -1;
            }
        }
        else if (other.gameObject.tag == "Glass")
        {
            activateGlassCollision(other.gameObject.GetComponent<MeshCollider>());
        }
    }

    void activateGlassCollision(MeshCollider glassCollider)
    {
        // TODO: implement dynamic particle collisions
        Vector3 center = glassCollider.bounds.center;
    }

    public IEnumerator ChangeFlameColor(int targetColor)
    {
        blend = 0;
        vfx.SetInt("Target Color", targetColor);
        while (blend < 1)
        {
            blend += 0.01f;
            vfx.SetFloat("Blend", blend);
            light.color = Color.Lerp(fireColor(currentColor), fireColor(targetColor), blend);
            yield return new WaitForFixedUpdate();
        }
        currentColor = targetColor;
        vfx.SetInt("Color value", targetColor);
    }

    IEnumerator ReturnToRegularColor(float delay)
    {
        yield return new WaitForSeconds(delay);
        StartCoroutine(ChangeFlameColor(7));
    }

    Color fireColor(int id)
    {
        switch (id)
        {
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
