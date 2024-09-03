using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TemperatureStateDisplay : MonoBehaviour
{
    private ThermometerController thermometerController; // Referencia al script ThermometerController
    public TMP_Text textMeshPro; // Referencia al componente TMP_Text para mostrar el texto

    private string estadoEthanol;
    private string estadoAgua;

    void Start() {
        thermometerController = FindObjectOfType<ThermometerController>();
    }


    void Update()
    {
        // Obtener el valor de la temperatura desde el ThermometerController
        float temperature = thermometerController.temperature;

        // Determinar el estado del etanol y del agua según la temperatura
        if (temperature >= 80f)
        {
            estadoEthanol = "Gaseoso";
            estadoAgua = "Líquido";
        }
        else
        {
            estadoEthanol = "Líquido"; // Supongo que para temperaturas menores a 80 es líquido
            estadoAgua = "Líquido"; // Asumo que el agua sigue siendo líquida por debajo de 100 grados
        }

        // Actualizar el texto en el TMP_TextMeshPro
        textMeshPro.text = $"Temperatura mezcla: {temperature.ToString("F1")}\nEstado Ethanol: {estadoEthanol}\nEstado Agua: {estadoAgua}";

    }
}
