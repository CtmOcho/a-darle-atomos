using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GasLawsController : MonoBehaviour
{
    public Slider volumeSlider;
    public Slider pressureSlider;
    public Slider temperatureSlider;

    public TMP_Text volumeValueText;
    public TMP_Text pressureValueText;
    public TMP_Text temperatureValueText;

    private float n = 1f;  // Asume 1 mol para simplificar
    private float R = 8.314f;  // Constante de los gases ideales en J/(mol·K)

    void Start()
    {
        // Asegúrate de que todos los componentes estén asignados
        if (volumeSlider == null || pressureSlider == null || temperatureSlider == null ||
            volumeValueText == null || pressureValueText == null || temperatureValueText == null)
        {
            Debug.LogError("Uno o más componentes no están asignados en el Inspector.");
            return;
        }

        // Inicializa los textos de los sliders
        UpdateVolumeText();
        UpdatePressureText();
        UpdateTemperatureText();

        // Asigna los listeners para los sliders
        volumeSlider.onValueChanged.AddListener(delegate { OnVolumeChanged(); });
        pressureSlider.onValueChanged.AddListener(delegate { OnPressureChanged(); });
        temperatureSlider.onValueChanged.AddListener(delegate { OnTemperatureChanged(); });
    }

    void OnVolumeChanged()
    {
        // Calcula la presión cuando el volumen cambia
        float V = volumeSlider.value;
        float T = temperatureSlider.value;

        if (V > 0)
        {
            float P = (n * R * T) / V;
            pressureSlider.value = P;
            UpdatePressureText();
        }
        else
        {
            Debug.LogWarning("El volumen no puede ser cero. Ajusta el volumen para continuar.");
        }

        UpdateVolumeText();
    }

    void OnPressureChanged()
    {
        // Calcula la temperatura cuando la presión cambia
        float P = pressureSlider.value;
        float V = volumeSlider.value;

        if (V > 0)
        {
            float T = (P * V) / (n * R);

            if (T >= 400)
            {
                T = 400;
                V = (n * R * T) / P;
                volumeSlider.value = V;
                UpdateVolumeText();
            }

            temperatureSlider.value = T;
            UpdateTemperatureText();
        }
        else
        {
            Debug.LogWarning("El volumen no puede ser cero. Ajusta el volumen para continuar.");
        }

        UpdatePressureText();
    }

    void OnTemperatureChanged()
    {
        // Calcula la presión cuando la temperatura cambia
        float T = temperatureSlider.value;
        float V = volumeSlider.value;

        if (V > 0)
        {
            float P = (n * R * T) / V;
            pressureSlider.value = P;
            UpdatePressureText();
        }
        else
        {
            Debug.LogWarning("El volumen no puede ser cero. Ajusta el volumen para continuar.");
        }

        UpdateTemperatureText();
    }

    void UpdateVolumeText()
    {
        volumeValueText.text = volumeSlider.value.ToString("F2") + " m³";
    }

    void UpdatePressureText()
    {
        pressureValueText.text = pressureSlider.value.ToString("F2") + " Pa";
    }

    void UpdateTemperatureText()
    {
        temperatureValueText.text = temperatureSlider.value.ToString("F2") + " K";
    }
}
