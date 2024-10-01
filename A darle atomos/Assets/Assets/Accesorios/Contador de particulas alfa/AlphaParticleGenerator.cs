using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class AlphaParticleGenerator : MonoBehaviour
{
    public ParticleCounterController controller;
    public GameObject alphaParticle;
    public int maxParticles;
    public AnimationCurve weight;

    int subCounter;
    float randAngle;
    float randRadius;
    float radius;

    GameObject[] particleArr;
    void Start()
    {
        UnityEngine.Random.InitState(DateTime.Now.Millisecond);
        subCounter = 0;
        radius = 0.006f;
    }

    // Update is called once per frame
    void Update()
    {
        if(controller.isOn)
        {
            particleArr = GameObject.FindGameObjectsWithTag("alphaParticle");
            if (particleArr.Length < maxParticles && subCounter > 50)
            {
                randAngle = UnityEngine.Random.Range(0, 360);
                randRadius = Mathf.Lerp(0, radius, GetWeightedNumber());
                Vector3 point = new Vector3(randRadius, 0, 0);
                Vector3 rotatedPoint = Quaternion.Euler(0, 0, randAngle) * point;
                GameObject particle = Instantiate(alphaParticle, transform.TransformPoint(rotatedPoint), transform.rotation);
                Destroy(particle, 2);
                subCounter = 0;
            }
            subCounter++;
        }
    }
    public float GetWeightedNumber()
    {
        return weight.Evaluate(UnityEngine.Random.value);
    }
}
