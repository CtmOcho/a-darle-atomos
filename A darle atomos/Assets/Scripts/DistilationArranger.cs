using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DistilationArranger : MonoBehaviour
{
    public GameObject ethanolPrefab;
    public GameObject waterPrefab;
    public int sizeX = 3;
    public int sizeY = 3;
    public int sizeZ = 3;
    public float spacing = 1.0f;

    public Slider temperatureSlider;
    public TMP_Text temperatureText;
    public TMP_Text explanationText;
    private GameObject[] molecules;

    private bool hasReachedMaxTemperature = false;
    private bool ethanolReachedHeight = false;  // Estado para saber si el etanol alcanzó la altura deseada

    public float ethanolTargetHeight = 10.0f;  // Altura objetivo para las moléculas de etanol
    public float ethanolTargetTemperature = 81f;  // Temperatura de punto de ebullición del etanol

    void Start()
    {
        int moleculeCount = sizeX * sizeY * sizeZ;
        molecules = new GameObject[moleculeCount];
        ArrangeMolecules();

        temperatureSlider.onValueChanged.AddListener(delegate { OnTemperatureSliderChanged(temperatureSlider.value); });

        SetInitialTemperature(temperatureSlider.value);
        ShowExplanation();
    }

    void ArrangeMolecules()
    {
        Vector3 origin = transform.position - new Vector3(sizeX - 1, sizeY - 1, sizeZ - 1) * spacing / 2;
        int index = 0;
        bool useEthanol = true;

        for (int x = 0; x < sizeX; x++)
        {
            for (int y = 0; y < sizeY; y++)
            {
                for (int z = 0; z < sizeZ; z++)
                {
                    Vector3 position;

                    // Seleccionar el prefab y aplicar el espaciado personalizado
                    if (useEthanol)
                    {
                        position = origin + new Vector3(x * 5, y * 3, z) * spacing;
                        GameObject moleculePair = Instantiate(ethanolPrefab, position, Quaternion.identity);
                        molecules[index] = moleculePair;
                    }
                    else
                    {
                        position = origin + new Vector3(x * 3, y * 2, z) * spacing;
                        GameObject moleculePair = Instantiate(waterPrefab, position, Quaternion.identity);
                        molecules[index] = moleculePair;
                    }

                    index++;

                    // Alternar entre etanol y agua
                    useEthanol = !useEthanol;
                }
            }
        }

        // Desactivar ambos prefabs originales
        ethanolPrefab.SetActive(false);
        waterPrefab.SetActive(false);
    }

    void OnTemperatureSliderChanged(float value)
    {
        if (value >= ethanolTargetTemperature && !ethanolReachedHeight)
        {
            temperatureSlider.value = ethanolTargetTemperature;  // Bloquear el slider en 80°C
            CheckEthanolHeight();
            return;
        }

        // Actualizar el texto de la UI
        temperatureText.text = value.ToString("F1") + "°C";
        UpdateExplanationText(value);

        if (molecules.Length > 0)
        {
            foreach (var molecule in molecules)
            {
                float tempIncrement = value - molecule.GetComponent<DistilationBehavior>().currentTemperature;
                molecule.GetComponent<DistilationBehavior>().IncreaseTemperature(tempIncrement);
            }
        }
    }

    void CheckEthanolHeight()
    {
        bool allEthanolReachedHeight = true;

        foreach (var molecule in molecules)
        {
            DistilationBehavior behavior = molecule.GetComponent<DistilationBehavior>();

            if (behavior.moleculeType == "Etanol" && molecule.transform.position.y < ethanolTargetHeight)
            {
                allEthanolReachedHeight = false;
                break;
            }
        }

        if (allEthanolReachedHeight)
        {
            ethanolReachedHeight = true;
            temperatureSlider.maxValue = 100f;  // Desbloquear el slider
        }
    }

    void SetInitialTemperature(float temperature)
    {
        temperatureText.text = temperature.ToString("F1") + "°C";
        if (molecules.Length > 0)
        {
            foreach (var molecule in molecules)
            {
                molecule.GetComponent<DistilationBehavior>().IncreaseTemperature(temperature - 20f);
            }
        }
    }

    void ShowExplanation()
    {
        explanationText.text = "Las moléculas están en una mezcla. A medida que aumentas la temperatura con el slider, las moléculas comienzan a vibrar simulando su separación.";
    }

    void UpdateExplanationText(float temperature)
    {
        if (temperature < 80f)
        {
            explanationText.text = "Las moléculas están mezcladas y vibran lentamente debido al estado líquido.\nA menos de 80º, la vibración aumenta lentamente, ya que las dos moleculas siguen siendo estables.";
        }
        else if (temperature >= 80f && temperature < 100f)
        {
            explanationText.text = "Al alcanzar los 80º, se llega al punto de ebullición del etanol, por lo que empezará a evaporarse, aumentando la vibración de sus moleculas.\nLa temperatura se mantiene constante hasta que se evapora todo el etanol";
        }
        else if (temperature >= 100f)
        {
            explanationText.text = "Al alcanzar los 100º, se alcanza el punto de ebullición del Agua, donde se inicia a evaporar: las moléculas se separan y vibran intensamente, representando el estado gaseoso.";
        }
    }
}
