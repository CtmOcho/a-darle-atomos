using System;
using UnityEngine;

public class DustCollisionController : MonoBehaviour
{
    public Stirrer_controller controller;
    private float actualPHvalue; // pH actual de la solución
    private float dustPHvalue; // pH actual del polvo
    private float actualLiquidVolume; // Volumen actual de la solución en ml
    private float totalMolesHPlus; // Moles totales de H+ en la solución
    private float totalMolesOHPlus; // Moles totales de OH- en la solución
    private float newScale = 0.001f;
    private SolutionPHCalculator phCalculator; // Referencia al controlador SolutionPHCalculator
    private bool alreadyUpdated = false; // Nueva variable para controlar la actualización única

    // Volumen del polvo en ml (puedes ajustarlo según el polvo)
    public float dustVolume = 10f; // Este valor será dinámico, en ml

    void Start()
    {

        // Obtenemos la referencia al SolutionPHCalculator en el mismo GameObject
        phCalculator = GetComponent<SolutionPHCalculator>();


    }

    private void Update()
    {
        // Solo actualizamos si el stirrer está activo, el contador es mayor que 0 y no se ha actualizado ya
        if (controller.isActive && !alreadyUpdated)
        {
            actualPHvalue = phCalculator.CalculateNewPH(GetComponent<DropCollisionController>().actualPHvalue, GetComponent<DropCollisionController>().actualLiquidVolume, dustPHvalue, dustVolume);

            // Actualizamos el valor de actualPHvalue y el volumen en DropCollisionController
            DropCollisionController dropController = GetComponent<DropCollisionController>();
            dropController.actualPHvalue = actualPHvalue;
            dropController.changeColorScript.ColorChange(); // Cambia el color según el pH actual, pero sin modificar el pH
            alreadyUpdated = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Spoon") && other.gameObject.transform.GetChild(0).gameObject.activeSelf)
        {
            // Obtenemos el pH del polvo (dust) desde SpoonDustProperties
            dustPHvalue = other.gameObject.GetComponent<SpoonDustProperties>().ph;

            // Reiniciamos el estado de actualización para una nueva interacción
            alreadyUpdated = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Spoon") && other.gameObject.transform.GetChild(0).gameObject.activeSelf)
        {
            // Reducimos el tamaño del polvo hasta que desaparezca
            other.gameObject.transform.GetChild(0).transform.localScale -= new Vector3(1.0f, 1.4f, 1.0f) * newScale;

            if (other.gameObject.transform.GetChild(0).transform.localScale.magnitude <= 0.001f)
            {
                other.gameObject.transform.GetChild(0).gameObject.SetActive(false);
                DropCollisionController dropController = GetComponent<DropCollisionController>();
                
                dropController.actualLiquidVolume += dustVolume; // Sumamos el volumen del polvo al volumen total de la solución
                Vector3 volumeScale = transform.localScale;

                volumeScale.y += (dustVolume / 300f); // Actualizamos la escala basada en el volumen agregado
                dropController.transform.localScale = volumeScale;
            }
                        // ***Actualizamos el volumen en DropCollisionController***


        }
    }
}
