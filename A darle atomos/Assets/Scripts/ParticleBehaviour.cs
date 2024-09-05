using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
//using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class ParticleBehaviour : MonoBehaviour
{
    public List<Rigidbody> rb;
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
    public GameObject particlePrefab;
    public float pressureOffset;

    void Start()
    {
        currentTemperature = minimumTemperature;
        // Get the Rigidbody component attached to the object
        rb = GetComponentsInChildren<Rigidbody>().ToList();

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
        pressureText.text = (Mathf.Lerp(minimumPressure, maximumPressure, value) - pressureOffset).ToString("F1");
    }

    void FixedUpdate()
    {
        foreach (Rigidbody particle in rb)
        {
            particle.velocity = particle.velocity.normalized * Mathf.Lerp(minimumSpeed, maximumSpeed, value);
        }
    }

    void ApplyRandomForce()
    {
        foreach (Rigidbody particle in rb)
        {
            Vector3 randomDirection = UnityEngine.Random.onUnitSphere;

            Vector3 force = randomDirection * minimumSpeed;

            particle.velocity = force;
        }
    }

    public void AddParticle()
    {
        GameObject particle = Instantiate(particlePrefab, transform.position, new quaternion(0, 0, 0, 0), transform);
        rb.Add(particle.GetComponent<Rigidbody>());
        Vector3 randomDirection = UnityEngine.Random.onUnitSphere;
        Vector3 force = randomDirection * Mathf.Lerp(minimumSpeed, maximumSpeed, value);
        particle.GetComponent<Rigidbody>().velocity = force;
    }

    public void RemoveParticle()
    {
        Rigidbody rbDelete = rb[0];
        rb.Remove(rbDelete);
        Destroy(rbDelete.gameObject);
    }
}