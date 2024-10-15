using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleContentSpawner : MonoBehaviour
{
    public GameObject[] objectToSpawn;
    public float objectQuantity;
    public AnimationCurve weight;

    float tolerance;
    int counter;
    int subCounter;
    float randAngle;
    float randRadius;
    float radius;

    // Start is called before the first frame update
    void Start()
    {
        UnityEngine.Random.InitState(DateTime.Now.Millisecond);
        counter = 0;
        subCounter = 0;
        tolerance = 50;
        radius = 0.01f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(transform.rotation.eulerAngles.x - 90f) < tolerance && counter < objectQuantity && subCounter > 50)
        {
            randAngle = UnityEngine.Random.Range(0, 360);
            randRadius = Mathf.Lerp(0, radius, GetNumber());
            Vector3 point = new Vector3(randRadius, 0, 0);
            Vector3 dir = transform.TransformPoint(point) - transform.position;
            Vector3 rotatedPoint = Quaternion.Euler(0, randAngle, 0) * dir;
            point = rotatedPoint + transform.position;
            Instantiate(objectToSpawn[UnityEngine.Random.Range(0, 2)], point, Quaternion.identity, null);
            counter++;
            subCounter = 0;
        }
        subCounter++;
    }
    public float GetNumber()
    {
        return weight.Evaluate(UnityEngine.Random.value);
    }
}
