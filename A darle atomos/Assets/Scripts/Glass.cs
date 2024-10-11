using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class Glass : MonoBehaviour
{
    public float temperature;
    public float maxTemperature = 114f;
    public float minTemperature = 20f;
    public float tempStep;
    public List<GameObject> contents;
    public bool isHot;
    public flame flame;
    public bool distilationLab =false;
    public bool yodoLab = false;
    public bool rainLab = false;

    // Update is called once per frame
    void Update()
    {
        CheckFire();
        if (isHot)
        {
            AddHeat();
        }
        else
        {
            Cooldown();
        }
    }
    void CheckFire()
    {
        if (flame != null)
        {
            isHot = flame.isOn;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Iodine"))
        {
            contents.Add(other.gameObject);
        }
        else if (other.gameObject.CompareTag("Fire"))
        {
            flame = other.GetComponent<flame>();
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Iodine"))
        {
            contents.Remove(other.gameObject);
        }
        else if (other.gameObject.CompareTag("Fire"))
        {
            flame = null;
        }
    }
    private void AddHeat()
    {
        if (temperature > maxTemperature)
        {
            return;
        }

        for (int i = contents.Count - 1; i >= 0; i--)
        {
            if (contents[i] == null)
            {
                contents.RemoveAt(i);
            }
        }

        temperature += tempStep;
        for (int i = contents.Count - 1; i >= 0; i--)
        {
            contents[i].GetComponent<IodineReaction>().temperature = temperature;
        }
    }

    private void Cooldown()
    {
        if (temperature < minTemperature)
        {
            return;
        }
        tempStep = 0.01f;
        temperature -= tempStep / 10;
        for (int i = contents.Count - 1; i >= 0; i--)
        {
            contents[i].GetComponent<IodineReaction>().temperature = temperature;
        }
    }

}