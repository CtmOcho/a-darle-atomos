using UnityEngine;
using UnityEngine.UI;

public class TemperatureSlider : MonoBehaviour
{
    public Slider temperatureSlider;
    public Text temperatureValueText;  // Componente de texto para mostrar el valor de la temperatura
    public Slider pressureSlider;
    public Slider volumeSlider;

    private float n = 1f;  // Asume 1 mol para simplificar
    private float R = 8.314f;  // Constante de los gases ideales en J/(mol·K)

    void Start()
    {
        // Inicializa el valor del texto
        UpdateTemperatureText();
    }

    public void OnTemperatureChanged()
    {
        float T = temperatureSlider.value;
        float V = volumeSlider.value;

        // Calcula la presión usando la ley de los gases ideales
        float P = (n * R * T) / V;

        // Actualiza el valor del slider de presión
        pressureSlider.value = P;

        // Actualiza el valor del texto de temperatura
        UpdateTemperatureText();
    }

    void UpdateTemperatureText()
    {
        // Muestra el valor del slider de temperatura en el texto
        temperatureValueText.text = temperatureSlider.value.ToString("F2") + " K";
    }
}
