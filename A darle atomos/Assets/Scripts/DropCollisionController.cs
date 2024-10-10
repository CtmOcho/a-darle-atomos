using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropCollisionController : MonoBehaviour
{
    public float actualLiquidVolume; // Volumen actual de la solución en mililitros (ml)
    public float actualPHvalue; // pH actual de la solución
    public bool hasPHDetector; // Indica si el detector universal está presente
    private float totalMolesHPlus; // Moles totales de H+ en la solución
    public bool isPHExp;
    private ChangeColor changeColorScript;
    private SolutionPHCalculator phCalculator; // Referencia al controlador SolutionPHCalculator

    void Start()
    {
        // Calculamos el volumen inicial de la solución
        actualLiquidVolume = transform.localScale.z * 300f; // Asumiendo que el factor de escala a volumen es 300 ml

        // Calculamos los moles iniciales de H+ en la solución
        float initialHPlusConcentration = Mathf.Pow(10, -actualPHvalue); // Concentración inicial de H+ en mol/L
        totalMolesHPlus = initialHPlusConcentration * (actualLiquidVolume / 1000f); // Convertimos ml a L
        
        // Obtenemos la referencia del script ChangeColor y SolutionPHCalculator
        changeColorScript = GetComponent<ChangeColor>();
        phCalculator = GetComponent<SolutionPHCalculator>(); // Busca el controlador en el mismo GameObject
    }

    void Update()
    {
        if (isPHExp)
        {
            if (hasPHDetector)
            {
                changeColorScript.ColorChange(); // Cambia el color según el pH actual, pero sin modificar el pH
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Verificamos si el objeto que entra es una gota (drop) con el script DropInformation
        DropInformation dropInfo = other.gameObject.GetComponent<DropInformation>();
        if (dropInfo != null)
        {
            // Aumentamos la escala en el eje Z del objeto que tiene este script
            Vector3 newScale = transform.localScale;
            float addedVolume = dropInfo.dropAmmount; // Volumen de la gota en ml
            newScale.z += (addedVolume / 300f); // Actualizamos la escala basada en el volumen agregado
            transform.localScale = newScale;

            // Actualizamos el volumen total de la solución usando el 
            actualLiquidVolume += addedVolume;

            if (isPHExp)
            {
                if (dropInfo.hasPHDetector)
                {
                    // Si la gota que se está añadiendo es el detector de pH, no modificamos el pH
                    hasPHDetector = true;
                    changeColorScript.boolPHDetector = true;
                }
                else
                {
                    // Calculamos el nuevo pH utilizando el SolutionPHCalculator solo si no es el detector de pH
                    actualPHvalue = phCalculator.CalculateNewPH(actualPHvalue, actualLiquidVolume, dropInfo.liquidPH, addedVolume);

                    // Aseguramos que el pH esté entre 0 y 14
                    actualPHvalue = Mathf.Clamp(actualPHvalue, 0f, 14f);

                    // Mostramos el nuevo valor de pH en la consola
                    Debug.Log("Nuevo valor de pH: " + actualPHvalue);
                }
            }
        }
    }
}
