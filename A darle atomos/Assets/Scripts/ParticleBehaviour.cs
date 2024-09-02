using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class ParticleBehaviour : MonoBehaviour
{
    private Rigidbody[] rb;
    public float minimumSpeed = 0.3f;
    public float maximumSpeed = 5f;
    public float minimumTemperature = 20f;
    public float maximumTemperature = 100f;
    public float minimumPressure = 760f;
    public float maximumPressure = 3000f;
    private float currentTemperature;
    public float value = 0;
    public TextMeshPro temperatureText;
    public TextMeshPro pressureText;

    void Start()
    {
        currentTemperature = minimumTemperature;
        // Get the Rigidbody component attached to the object
        rb = GetComponentsInChildren<Rigidbody>();

        if (rb == null)
        {
            Debug.LogError("No Rigidbody component found on this object.");
            return;
        }

        // Apply a random force to the Rigidbody
        ApplyRandomForce();
    }

    void Update()
    {
        currentTemperature = Mathf.Lerp(minimumTemperature, maximumTemperature, value);
        temperatureText.text = currentTemperature.ToString("F1");
        pressureText.text = Mathf.Lerp(minimumPressure, maximumPressure, value).ToString("F1");
    }

    void FixedUpdate()
    {
        for (int i = 0; i < rb.Length; i++)
        {
            rb[i].velocity = rb[i].velocity.normalized * Mathf.Lerp(minimumSpeed, maximumSpeed, value);
        }
    }

    void ApplyRandomForce()
    {
        for (int i = 0; i < rb.Length; i++)
        {
            Vector3 randomDirection = Random.onUnitSphere;

            Vector3 force = randomDirection * minimumSpeed;

            rb[i].velocity = force;
        }
    }
}