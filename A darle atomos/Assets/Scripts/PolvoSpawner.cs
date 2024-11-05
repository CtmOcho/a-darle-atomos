using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolvoSpawner : MonoBehaviour
{
   // Variables públicas para ajustar los valores
    public GameObject targetObject; // El objeto al que llamaremos la función
    private BoxCollider boxCollider; // El BoxCollider que debe ser isTrigger


    void Start()
    {
        // Verificamos que el BoxCollider esté presente y sea un trigger
        boxCollider = GetComponent<BoxCollider>();

        if (boxCollider == null || !boxCollider.isTrigger)
        {
            Debug.LogError("El objeto no tiene un BoxCollider o no está configurado como Trigger.");
        }


    }

    // Este método se llama mientras el objeto permanece dentro del trigger
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject == targetObject)
        {
            DropperPolvitoSpawner dropper = targetObject.GetComponent<DropperPolvitoSpawner>();
                if (!dropper.isFull)
                {
                    dropper.objectToSpawn.SetActive(true);
                    dropper.isFull = true;
                }
            
        }
    }


}
