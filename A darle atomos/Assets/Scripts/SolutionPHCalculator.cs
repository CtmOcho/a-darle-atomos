using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolutionPHCalculator : MonoBehaviour
{
    // Método para calcular el nuevo pH
    public float CalculateNewPH(float currentPH, float currentVolume, float addedPH, float addedVolume)
    {
        // Convertimos el pH actual y el pH agregado en concentraciones de H+ y OH-
        float currentHPlusConcentration = Mathf.Pow(10, -currentPH); // Concentración de H+ en la solución actual
        float currentOHMinusConcentration = Mathf.Pow(10, -(14 - currentPH)); // Concentración de OH- en la solución actual
        
        float addedHPlusConcentration = Mathf.Pow(10, -addedPH); // Concentración de H+ en la sustancia agregada
        float addedOHMinusConcentration = Mathf.Pow(10, -(14 - addedPH)); // Concentración de OH- en la sustancia agregada

        // Calculamos los moles de H+ y OH- actuales
        float currentMolesHPlus = currentHPlusConcentration * (currentVolume / 1000f); // Convertimos ml a L
        float currentMolesOHMinus = currentOHMinusConcentration * (currentVolume / 1000f); // Convertimos ml a L

        // Calculamos los moles de H+ y OH- agregados
        float addedMolesHPlus = addedHPlusConcentration * (addedVolume / 1000f); // Convertimos ml a L
        float addedMolesOHMinus = addedOHMinusConcentration * (addedVolume / 1000f); // Convertimos ml a L

        // Sumamos los moles de H+ y OH-
        float totalMolesHPlus = currentMolesHPlus + addedMolesHPlus;
        float totalMolesOHMinus = currentMolesOHMinus + addedMolesOHMinus;

        // Neutralizamos H+ y OH-
        float remainingMolesHPlus, remainingMolesOHMinus;

        if (totalMolesHPlus > totalMolesOHMinus)
        {
            remainingMolesHPlus = totalMolesHPlus - totalMolesOHMinus;
            remainingMolesOHMinus = 0;
        }
        else
        {
            remainingMolesOHMinus = totalMolesOHMinus - totalMolesHPlus;
            remainingMolesHPlus = 0;
        }

        // Calculamos el nuevo volumen total
        float newTotalVolume = currentVolume + addedVolume; // Suma de los volúmenes en ml

        // Calculamos el nuevo pH según el exceso de H+ o OH-
        float newPH;
        if (remainingMolesHPlus > 0)
        {
            // Si hay exceso de H+, la solución es ácida
            float newHPlusConcentration = remainingMolesHPlus / (newTotalVolume / 1000f); // Concentración en mol/L
            newPH = -Mathf.Log10(newHPlusConcentration);
        }
        else if (remainingMolesOHMinus > 0)
        {
            // Si hay exceso de OH-, la solución es básica
            float newOHMinusConcentration = remainingMolesOHMinus / (newTotalVolume / 1000f); // Concentración en mol/L
            newPH = 14 - Mathf.Log10(newOHMinusConcentration); // Ajuste en el cálculo
        }
        else
        {
            // Caso muy raro donde H⁺ y OH⁻ se neutralizan completamente, pH neutro
            newPH = 7;
        }

        // Retornamos el nuevo pH calculado
        return Mathf.Clamp(newPH, 0f, 14f); // Aseguramos que el pH esté entre 0 y 14
    }


}
