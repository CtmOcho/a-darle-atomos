using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VolumeSlider : MonoBehaviour
{
    public Slider volumeSlider;
    public TMP_Text volumeValueText;  // Usa TMP_Text en lugar de Text
    public Slider pressureSlider;
    public Slider temperatureSlider;

    private float n = 1f;  // Asume 1 mol para simplificar
    private float R = 8.314f;  // Constante de los gases ideales en J/(mol·K)

    void Start()
    {
        // Asegúrate de que todos los objetos estén asignados
        if (volumeSlider == null || volumeValueText == null || pressureSlider == null || temperatureSlider == null)
        {
            return;
        }

        // Inicializa el valor del texto y la presión
        UpdateVolumeText();
        UpdatePressure();
    }

    public void OnVolumeChanged()
    {
        // Actualiza el texto de volumen
        UpdateVolumeText();

        // Actualiza el valor de presión basado en el nuevo volumen
        UpdatePressure();
    }

    void UpdateVolumeText()
    {
        // Muestra el valor del slider de volumen en el texto
        volumeValueText.text = volumeSlider.value.ToString("F2") + " m³";
    }

    void UpdatePressure()
    {
        float V = volumeSlider.value;
        float T = temperatureSlider.value;

        // Asegúrate de que V no sea cero para evitar una división por cero
        if (V > 0)
        {
            // Calcula la presión usando la ley de los gases ideales
            float P = (n * R * T) / V;

            // Actualiza el valor del slider de presión
            pressureSlider.value = P;
        }
        else
        {
            pressureSlider.value = 0;  // Asigna un valor por defecto si V es 0 o menor
        }
    }
}
