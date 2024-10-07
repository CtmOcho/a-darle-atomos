using System;
using UnityEngine;

public class DustCollisionController : MonoBehaviour
{
    public Stirrer_controller controller;
    private float actualPHvalue; // pH actual de la solución
    private float dustPHvalue; // pH actual del polvo
    private float actualLiquidVolume; // Moles totales de H+ en la solución
    private float totalMolesHPlus; // Moles totales de H+ en la solución
    private float totalMolesOHPlus; // Moles totales de OH+ en la solución
    private float newScale = 0.001f;

    private int counter;
    void Start()
    {
        actualPHvalue = GetComponent<DropCollisionController>().actualPHvalue;
        // Calculamos el volumen inicial de la solución
        actualLiquidVolume = transform.localScale.z * 300f; // Asumiendo que el factor de escala a volumen es 300 ml

        // Calculamos los moles iniciales de H+ en la solución
        float initialHPlusConcentration = Mathf.Pow(10, -actualPHvalue); // Concentración inicial de H+ en mol/L
        float initialOHPlusConcentration = Mathf.Pow(10, -(14 - dustPHvalue)); // Concentración inicial de OH+ en mol/L
        totalMolesOHPlus = initialOHPlusConcentration * (actualLiquidVolume / 1000f); // Convertimos ml a L
        totalMolesHPlus = initialHPlusConcentration * (actualLiquidVolume / 1000f); // Convertimos ml a L
    }
    private void Update()
    {
        if (controller.isActive && counter > 0)
        {
            if (dustPHvalue < 7.0f)
            {
                // Calculamos los moles de H+ en la gota
                float dropHPlusConcentration = Mathf.Pow(10, -dustPHvalue); // Concentración de H+ de la gota en mol/L
                float dropMolesHPlus = dropHPlusConcentration * (0.04f / 1000f); // Convertimos ml a L

                totalMolesHPlus += dropMolesHPlus;

                // Calculamos la nueva concentración de H+ en la solución
                float newHPlusConcentration = totalMolesHPlus / (actualLiquidVolume / 1000f); // Volumen total en L

                // Calculamos el nuevo pH
                actualPHvalue = -Mathf.Log10(newHPlusConcentration);
            }
            else
            {

                // Calculamos los moles de OH+ en la gota
                float dropOHPlusConcentration = Mathf.Pow(10, -(14 - dustPHvalue)); // Concentración de OH+ de la gota en mol/L
                float dropMolesOHPlus = dropOHPlusConcentration * (0.04f / 1000f); // Convertimos ml a L

                totalMolesOHPlus += dropMolesOHPlus;

                // Calculamos la nueva concentración de OH+ en la solución
                float newOHPlusConcentration = totalMolesOHPlus / (actualLiquidVolume / 1000f); // Volumen total en L

                // Calculamos el nuevo pH
                actualPHvalue = 14 - -Mathf.Log10(newOHPlusConcentration);
            }
            // Aseguramos que el pH esté entre 0 y 14
            actualPHvalue = Mathf.Clamp(actualPHvalue, 0f, 14f);
            GetComponent<DropCollisionController>().actualPHvalue = actualPHvalue;
            counter--;
            Debug.Log(actualPHvalue);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Spoon") && other.gameObject.transform.GetChild(0).gameObject.activeSelf)
        {
            dustPHvalue = other.gameObject.GetComponent<SpoonDustProperties>().ph;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Spoon") && other.gameObject.transform.GetChild(0).gameObject.activeSelf)
        {
            other.gameObject.transform.GetChild(0).transform.localScale = other.gameObject.transform.GetChild(0).transform.localScale - (new Vector3(1.0f,1.4f,1.0f) * newScale);
            if(other.gameObject.transform.GetChild(0).transform.localScale.magnitude <= 0.001f) other.gameObject.transform.GetChild(0).gameObject.SetActive(false);
            else
            {
                counter++;
            }
        }
    }
}
