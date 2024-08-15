using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Glass : MonoBehaviour
{
    private List<GameObject> contents;
    public bool isHot;

    // Start is called before the first frame update
    void Start()
    {
        contents = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {

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
            contents.Add(other.gameObject);
        }
        else if (other.gameObject.CompareTag("Fire"))
        {
            isHot = false;
        }
    }
}