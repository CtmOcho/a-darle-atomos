using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Balanza : MonoBehaviour
{
    float weight = 0.000f;

    public TextMeshPro scaleTMP;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        scaleTMP.text = weight.ToString();
    }
    private void OnTriggerEnter(Collider other)
    {
        weight += Mathf.Abs(other.GetComponent<Rigidbody>().mass*1000);
        
    }
    private void OnTriggerExit(Collider other)
    {
        weight -= Mathf.Abs(other.GetComponent<Rigidbody>().mass*1000);
    }
}
