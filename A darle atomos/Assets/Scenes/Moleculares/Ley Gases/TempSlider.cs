using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TemperatureSlider : MonoBehaviour
{
    public Slider temperatureSlider;
    public TMP_Text temperatureValueText;
    public Slider pressureSlider;
    public Slider volumeSlider;

    private float n = 1f;  // Asume 1 mol para simplificar
    private float R = 8.314f;  // Constante de los gases ideales en J/(mol·K)

    void Start()
    {
        // Verifica que los componentes estén asignados
        if (temperatureSlider == null || temperatureValueText == null || pressureSlider == null || volumeSlider == null)
        {
            Debug.LogError("Uno o más componentes no están asignados en el Inspector.");
            return;
        }

        // Inicializa el valor del texto
        UpdateTemperatureText();
    }

    public void OnTemperatureChanged()
    {
        float T = temperatureSlider.value;
        float V = volumeSlider.value;

        // Asegúrate de que el volumen no sea cero para evitar divisiones por cero
        if (V > 0)
        {
            // Calcula la presión usando la ley de los gases ideales
            float P = (n * R * T) / V;

            // Actualiza el valor del slider de presión
            pressureSlider.value = P;
        }
        else
        {
            Debug.LogWarning("El volumen no puede ser cero. Ajusta el volumen para continuar.");
            pressureSlider.value = 0;  // Asigna un valor por defecto si V es 0 o menor
        }

        // Actualiza el valor del texto de temperatura
        UpdateTemperatureText();
    }

    void UpdateTemperatureText()
    {
        // Muestra el valor del slider de temperatura en el texto
        temperatureValueText.text = temperatureSlider.value.ToString("F2") + " K";
    }
}
