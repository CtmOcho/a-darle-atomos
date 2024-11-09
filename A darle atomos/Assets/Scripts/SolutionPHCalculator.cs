using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolutionPHCalculator : MonoBehaviour
{
    public float CalculateNewPH(float currentPH, float currentVolume, float addedPH, float addedVolume)
    {
        // Convertimos pH en concentraciones de H+ y OH-
        float currentHPlusConcentration = Mathf.Pow(10, -currentPH);
        float currentOHMinusConcentration = Mathf.Pow(10, -(14 - currentPH));
        float addedHPlusConcentration = Mathf.Pow(10, -addedPH);
        float addedOHMinusConcentration = Mathf.Pow(10, -(14 - addedPH));   

        // Calcular nueva concentración de H+ y OH- considerando neutralización
        float newHPlusConcentration = (currentHPlusConcentration * currentVolume + addedHPlusConcentration * addedVolume);
        float newOHMinusConcentration = (currentOHMinusConcentration * currentVolume + addedOHMinusConcentration * addedVolume);

        // Ajustar concentraciones por neutralización mutua
        if (newHPlusConcentration > newOHMinusConcentration)
        {
            newHPlusConcentration -= newOHMinusConcentration;
            newOHMinusConcentration = 0;
        }
        else
        {
            newOHMinusConcentration -= newHPlusConcentration;
            newHPlusConcentration = 0;
        }

        // Calcular el nuevo volumen total
        float totalVolume = currentVolume + addedVolume;

        // Calculamos la concentración final de H+ y OH- usando dilución
        float finalHPlusConcentration = newHPlusConcentration / totalVolume;
        float finalOHMinusConcentration = newOHMinusConcentration / totalVolume;

        // Determina el pH a partir de las concentraciones finales
        float newPH = finalHPlusConcentration > 0 ? -Mathf.Log10(finalHPlusConcentration) : 14f + Mathf.Log10(finalOHMinusConcentration);

        return Mathf.Clamp(newPH, 0f, 14f);
    }
}
