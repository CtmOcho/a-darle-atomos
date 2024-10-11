using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolutionPHCalculator : MonoBehaviour
{
    // Método para calcular el nuevo pH usando dilución
    public float CalculateNewPH(float currentPH, float currentVolume, float addedPH, float addedVolume)
    {
        // Convertimos el pH actual y el pH agregado en concentraciones de H+ y OH-
        float currentHPlusConcentration = Mathf.Pow(10, -currentPH); // Concentración de H+ en la solución actual
        float addedHPlusConcentration = Mathf.Pow(10, -addedPH); // Concentración de H+ en la sustancia agregada

        // Calculamos el nuevo volumen total de la mezcla
        float totalVolume = currentVolume + addedVolume;

        // Calculamos la concentración promedio de H+ usando la dilución
        float newHPlusConcentration = (currentHPlusConcentration * currentVolume + addedHPlusConcentration * addedVolume) / totalVolume;

        // Calculamos el nuevo pH a partir de la nueva concentración de H+
        float newPH = -Mathf.Log10(newHPlusConcentration);

        // Aseguramos que el pH esté dentro del rango (0 a 14)
        return Mathf.Clamp(newPH, 0f, 14f);
    }
}
