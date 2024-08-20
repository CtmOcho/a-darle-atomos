using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class Glass : MonoBehaviour
{
    public List<GameObject> contents;
    public bool isHot;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        AddHeat();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Iodine"))
        {
            contents.Add(other.gameObject);
        }
        else if (other.gameObject.CompareTag("Fire"))
        {
            isHot = true;
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
            isHot = false;
        }
    }
    private void AddHeat()
    {
        for (int i = contents.Count - 1; i >= 0; i--)
        {
            if (contents[i] == null)
            {
                contents.RemoveAt(i);
            }
        }
        for (int i = contents.Count - 1; i >= 0; i--)
        {
            contents[i].GetComponent<IodineReaction>().temperature += 0.07f;
        }
    }

}