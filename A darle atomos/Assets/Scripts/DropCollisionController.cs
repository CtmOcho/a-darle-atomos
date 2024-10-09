using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropCollisionController : MonoBehaviour
{
    public float actualLiquidVolume; // Volumen actual de la solución en mililitros (ml)
    public float actualPHvalue; // pH actual de la solución
    public bool hasPhenolphtalein;
    private float totalMolesHPlus; // Moles totales de H+ en la solución
    public bool isPHExp;
    private ChangeColor changeColorScript;
    void Start()
    {
        // Calculamos el volumen inicial de la solución
        actualLiquidVolume = transform.localScale.z * 300f; // Asumiendo que el factor de escala a volumen es 300 ml

        // Calculamos los moles iniciales de H+ en la solución
        float initialHPlusConcentration = Mathf.Pow(10, -actualPHvalue); // Concentración inicial de H+ en mol/L
        totalMolesHPlus = initialHPlusConcentration * (actualLiquidVolume / 1000f); // Convertimos ml a L
        changeColorScript  = GetComponent<ChangeColor>();
    }

    void Update(){
        if(isPHExp){
            if(hasPhenolphtalein){    
                changeColorScript.ColorChange();
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

            // Actualizamos el volumen total de la solución
            actualLiquidVolume += addedVolume;

            if(isPHExp){
                if(dropInfo.hasPhenolphtalein){
                    hasPhenolphtalein = true;
                    changeColorScript.boolfenoftaleina = true;
                }
            // Calculamos los moles de H+ en la gota
            float dropHPlusConcentration = Mathf.Pow(10, -dropInfo.liquidPH); // Concentración de H+ de la gota en mol/L
            float dropMolesHPlus = dropHPlusConcentration * (addedVolume / 1000f); // Convertimos ml a L

            // Actualizamos los moles totales de H+ en la solución
            totalMolesHPlus += dropMolesHPlus;

            // Calculamos la nueva concentración de H+ en la solución
            float newHPlusConcentration = totalMolesHPlus / (actualLiquidVolume / 1000f); // Volumen total en L

            // Calculamos el nuevo pH
            actualPHvalue = -Mathf.Log10(newHPlusConcentration);

            // Aseguramos que el pH esté entre 0 y 14
            actualPHvalue = Mathf.Clamp(actualPHvalue, 0f, 14f);
            // Mostramos el nuevo valor de pH en la consola
            //Debug.Log("Nuevo valor de pH: " + actualPHvalue);
            }

        }
    }
}
