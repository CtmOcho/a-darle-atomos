using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustRemovalControllerRainLab : MonoBehaviour
{
    public string elementData;
    public Glass glassScript;
    public bool disolvedDust = false;

    public float temperatureTreshold;
    // Variables para la funcionalidad de reducción de escala
    public float decreaseAmount = 0.1f; // Cantidad por la que se reducirá la escala en cada llamada
    public float threshold = 0.1f; // Umbral mínimo de escala antes de desactivar el objeto

    // Método público para reducir la escala en los tres ejes sin desactivar el objeto
    public void ReduceScale()
    {
        if (glassScript.temperature > temperatureTreshold)
        {
            // Reducimos la escala del objeto en los tres ejes (X, Y, Z)
            Vector3 currentScale = transform.localScale;
            currentScale.x -= decreaseAmount;
            currentScale.y -= decreaseAmount;
            currentScale.z -= decreaseAmount;

            // Aseguramos que la escala no sea negativa en ningún eje y seteamos en 0 si pasa el threshold
            if (currentScale.x <= threshold) currentScale.x = 0;
            if (currentScale.y <= threshold) currentScale.y = 0;
            if (currentScale.z <= threshold) currentScale.z = 0;

            // Seteamos el booleano disolvedDust si la escala es cero en algún eje
            if (currentScale.x == 0 && currentScale.y == 0 && currentScale.z == 0)
            {
                disolvedDust = true;
            }

            // Aplicamos la nueva escala
            transform.localScale = currentScale;
        }
    }
}
