using UnityEngine;
using UnityEngine.UI;
using TMPro;  // Asegúrate de agregar esta línea si usas TextMeshPro

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
        // Inicializa el valor del texto
        UpdateVolumeText();
    }

    public void OnVolumeChanged()
    {
        float V = volumeSlider.value;
        float T = temperatureSlider.value;

        // Calcula la presión usando la ley de los gases ideales
        float P = (n * R * T) / V;

        // Actualiza el valor del slider de presión
        pressureSlider.value = P;

        // Actualiza el valor del texto de volumen
        UpdateVolumeText();
    }

    void UpdateVolumeText()
    {
        // Muestra el valor del slider de volumen en el texto
        volumeValueText.text = volumeSlider.value.ToString("F2") + " m³";
    }
}
