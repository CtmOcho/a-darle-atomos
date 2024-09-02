using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PressureSlider : MonoBehaviour
{
    public Slider pressureSlider;
    public TMP_Text pressureValueText;
    public Slider temperatureSlider;
    public Slider volumeSlider;

    private float n = 1f;  // Asume 1 mol para simplificar
    private float R = 8.314f;  // Constante de los gases ideales en J/(mol·K)

    void Start()
    {
        // Verifica que los componentes estén asignados
        if (pressureSlider == null || pressureValueText == null || temperatureSlider == null || volumeSlider == null)
        {
            Debug.LogError("Uno o más componentes no están asignados en el Inspector.");
            return;
        }

        // Inicializa el valor del texto
        UpdatePressureText();
    }

    public void OnPressureChanged()
    {
        // Actualiza el valor del texto de la presión
        UpdatePressureText();

        // Actualiza los valores de volumen y temperatura
        UpdateVolumeAndTemperature();
    }

    void UpdatePressureText()
    {
        float P = pressureSlider.value;
        // Muestra el valor del slider de presión en el texto
        pressureValueText.text = P.ToString("F2") + " Pa";
    }

    void UpdateVolumeAndTemperature()
    {
        float P = pressureSlider.value;
        float V = volumeSlider.value;

        // Asegúrate de que el volumen no sea cero para evitar divisiones por cero
        if (V > 0)
        {
            // Calcula la temperatura usando la ley de los gases ideales
            float T = (P * V) / (n * R);

            // Si la temperatura calculada excede la máxima permitida, ajusta el volumen
            if (T >= 400)
            {
                T = 400;
                V = (n * R * T) / P;
                volumeSlider.value = V;
            }

            // Actualiza el valor del slider de temperatura
            temperatureSlider.value = T;
        }
        else
        {
            Debug.LogWarning("El volumen no puede ser cero. Ajusta el volumen para continuar.");
            temperatureSlider.value = 0;  // Asigna un valor por defecto si V es 0 o menor
        }
    }
}
