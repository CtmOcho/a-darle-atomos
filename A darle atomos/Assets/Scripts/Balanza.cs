using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Balanza : MonoBehaviour
{
    float weight = 0.000f;

    public TextMeshPro scaleTMP;
    private List<Rigidbody> objectsOnScale = new List<Rigidbody>();


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
    
        Rigidbody otherRigidbody = other.GetComponent<Rigidbody>();
        if (otherRigidbody != null && !objectsOnScale.Contains(otherRigidbody))
        {
            
            objectsOnScale.Add(otherRigidbody);
            weight += Mathf.Abs(otherRigidbody.mass * 1000);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Rigidbody otherRigidbody = other.GetComponent<Rigidbody>();
        if (otherRigidbody != null && objectsOnScale.Contains(otherRigidbody))
        {

            objectsOnScale.Remove(otherRigidbody);

            weight -= Mathf.Abs(otherRigidbody.mass * 1000);


            if (weight < 0)
            {
                weight = 0;
            }
        }
    }
}
