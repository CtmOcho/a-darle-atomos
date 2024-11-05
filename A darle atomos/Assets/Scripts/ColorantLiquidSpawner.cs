using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorantLiquidSpawner : MonoBehaviour
{
    // Variables públicas para ajustar los valores
    public GameObject targetObject; // El objeto al que llamaremos la función
  
    private BoxCollider boxCollider; // El BoxCollider que debe ser isTrigger


    public Color liquidColor;
    private Renderer sourceRenderer;

    public bool isColorant = true;

    void Start()
    {
        // Verificamos que el BoxCollider esté presente y sea un trigger
        boxCollider = GetComponent<BoxCollider>();

        if (boxCollider == null || !boxCollider.isTrigger)
        {
            Debug.LogError("El objeto no tiene un BoxCollider o no está configurado como Trigger.");
        }

            Renderer sourceRenderer = GetComponent<Renderer>();
            liquidColor = sourceRenderer.material.GetColor("_Color");

        


    }

    // Este método se llama mientras el objeto permanece dentro del trigger
    void OnTriggerStay(Collider other)
    {     
        if (other.gameObject == targetObject)
        {
            DropperLiquidSpawner dropper = targetObject.GetComponent<DropperLiquidSpawner>();
                if (!dropper.isFull)
                {
                    dropper.FillDropper();
                
                        GameObject dropperLiquidObject = GameObject.FindWithTag("dropperLiquid");
                        if (dropperLiquidObject != null)
                            {
                                // Obtenemos el Renderer del objeto que tiene el tag
                                Renderer dropperLiquidRenderer = dropperLiquidObject.GetComponent<Renderer>();

                                if (dropperLiquidRenderer != null)
                                {
                                    // Asignamos el color del material
                                    dropperLiquidRenderer.material.SetColor("_Color", liquidColor);
                                }
                            }
                            dropper.isElephantExp = true;
                            dropper.isColorant = true;
                            dropper.liquidColor = liquidColor;


                }
            
        }


    }
    
}

