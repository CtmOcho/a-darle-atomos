using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropperLiquidSpawner : MonoBehaviour
{
    public GameObject objectToSpawn;
    public GameObject dropperLiquid;
    public float objectQuantity;

    public float liquidInitScale = 1;

    int counter;
    int subCounter;

    // Start is called before the first frame update
    void Start()
    {
        SetLiquidScale(liquidInitScale);
        counter = 0;
        subCounter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(Vector3.Dot(transform.up, Vector3.down)) < 0.3f && counter < objectQuantity && subCounter > 50 && dropperLiquid.transform.localScale.z > 0.002f)
        {
            
            SetLiquidScale(dropperLiquid.transform.localScale.z - 0.01f);
            Vector3 pos = new Vector3(transform.position.x, transform.position.y - 0.01f, transform.position.z);
            GameObject drop = Instantiate(objectToSpawn, pos, Quaternion.identity);
            Destroy(drop, 2);
            counter++;
            subCounter = 0;
        }
        subCounter++;
    }
    public void SetLiquidScale(float scale)
    {
        dropperLiquid.transform.localScale = new Vector3(1, 1, scale);
    }
}
