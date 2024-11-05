using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropperPolvitoSpawner : MonoBehaviour
{
    public GameObject objectToSpawn;
    public bool isFull;
    public bool isInValidZone;
    void Start()
    {
        isFull = false;
        
    }

    void Update()
    {
    
        if (Mathf.Abs(Vector3.Dot(transform.up, Vector3.down)) > 0.5f && isInValidZone)
        {
            GameObject drop = Instantiate(objectToSpawn, objectToSpawn.transform.position, objectToSpawn.transform.rotation);

            // Agrega un Rigidbody al objeto instanciado si no tiene uno
            Rigidbody rb = drop.GetComponent<Rigidbody>();
            if (rb == null)
            {
                rb = drop.AddComponent<Rigidbody>();
            }
            rb.useGravity = true;
            // Actualizamos la información del drop (gota)
            BoxCollider boxCollider = drop.GetComponent<BoxCollider>();
        if (boxCollider == null)
        {
            boxCollider = drop.AddComponent<BoxCollider>();
        }
            DropInformation dropInfo = drop.GetComponent<DropInformation>();
            if (dropInfo != null)
            {
                dropInfo.isDust = true;
            }
            Destroy(drop, 0.8f);
            objectToSpawn.SetActive(false);
            isFull = false;
        }
    }

}
