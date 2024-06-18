using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BoneSegment : MonoBehaviour
{
    LineRenderer lr;
    void Start()
    {
        lr = gameObject.AddComponent<LineRenderer>();
        lr.useWorldSpace = false;
        lr.widthMultiplier = 0.015f;

    }
    void Update()
    {
        lr.SetPosition(0, Vector3.zero);
        lr.SetPosition(1, gameObject.transform.localPosition * -1);
    }
}
