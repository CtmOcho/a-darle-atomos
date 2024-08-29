using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomForce : MonoBehaviour
{
    public float forceMagnitude = 10f; // The magnitude of the force to apply
    private Rigidbody[] rb;

    void Start()
    {
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

    void ApplyRandomForce()
    {
        for (int i = 0; i < rb.Length; i++)
        {
            // Generate a random direction
            Vector3 randomDirection = Random.onUnitSphere;

            // Calculate the force vector based on the random direction and magnitude
            Vector3 force = randomDirection * forceMagnitude;

            // Apply the force to the Rigidbody
            rb[i].AddForce(force);
        }
    }
}

