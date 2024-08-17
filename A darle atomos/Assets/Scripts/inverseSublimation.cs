using System;
using System.Collections.Generic;
using UnityEngine;

public class inverseSublimation : MonoBehaviour
{
    public float radius;
    public int quantity;
    public AnimationCurve weight;

    [HideInInspector]
    public bool isActive = true;

    public GameObject crystal;

    float randAngle;
    float randRadius;
    
    int counter;

    void Start()
    {
        UnityEngine.Random.InitState(DateTime.Now.Millisecond);
        counter = 0;
    }
    void Update()
    {
        if (isActive)
        {
            RaycastHit hit;
            randAngle = UnityEngine.Random.Range(0, 360);
            randRadius = Mathf.Lerp(0, radius, GetNumber());
            Vector3 point = new Vector3(randRadius, 0, 0);
            Vector3 dir = transform.TransformPoint(point) - transform.position;
            //Vector3 rotatedPoint = Quaternion.AngleAxis(randAngle, transform.TransformPoint(transform.up)) * point;
            Vector3 rotatedPoint = Quaternion.Euler(0, randAngle,0)*dir;
            point = rotatedPoint + transform.position;
            if (Physics.Raycast(point, Vector3.up, out hit, 2f))
            {
                if (hit.transform.tag == "IodineCrystal" && hit.transform.localScale.y <= 5f)
                {
                    Vector3 scale = new Vector3(1.04f, 2 - Math.Clamp(GetNumber(), 0, 0.5f), 1.04f);
                    hit.transform.localScale = new Vector3(hit.transform.localScale.x * scale.x, hit.transform.localScale.y * scale.y, hit.transform.localScale.z * scale.z);
                }
                else if (hit.transform.tag != "IodineCrystal" && counter < quantity)
                {
                    Quaternion rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
                    Instantiate(crystal, hit.point, rotation, hit.transform);
                    counter++;
                }
            }
        }
    }
    public float GetNumber()
    {
        return weight.Evaluate(UnityEngine.Random.value);
    }
}
