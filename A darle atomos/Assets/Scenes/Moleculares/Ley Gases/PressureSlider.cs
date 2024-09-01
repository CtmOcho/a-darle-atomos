using UnityEngine;
using UnityEngine.UI;
using TMPro;  // Asegúrate de agregar esta línea

public class PressureSlider : MonoBehaviour
{
    public Slider pressureSlider;
    public TMP_Text pressureValueText;  // Usa TMP_Text en lugar de Text
    public Slider temperatureSlider;
    public Slider volumeSlider;

    private float n = 1f;  // Asume 1 mol para simplificar
    private float R = 8.314f;  // Constante de los gases ideales en J/(mol·K)

    void Start()
    {
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
        pressureValueText.text = P.ToString("F2") + " Pa";  // F2 muestra dos decimales
    }

    void UpdateVolumeAndTemperature()
    {
        float P = pressureSlider.value;
        float V = volumeSlider.value;

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
}
