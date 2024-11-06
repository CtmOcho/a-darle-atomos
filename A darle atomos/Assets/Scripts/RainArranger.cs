using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RainArranger : MonoBehaviour
{
    public GameObject ethanolPrefab;
    public GameObject waterPrefab;
    public int sizeX = 3;
    public int sizeY = 3;
    public int sizeZ = 3;
    public float spacingX = 3.0f;
    public float spacingY = 3.0f;
    public float spacingZ = 3.0f;
    public float spacing = 1.0f;

    public Slider temperatureSlider;
    public TMP_Text temperatureText;
    public TMP_Text explanationText;
    private GameObject[] molecules;

    public bool labCompleted = false;

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
        int ethanolCount = 0;
        int waterCount = 0;

        System.Random random = new System.Random(); // Generador de números aleatorios

        for (int x = 0; x < sizeX; x++)
        {
            for (int y = 0; y < sizeY; y++)
            {
                for (int z = 0; z < sizeZ; z++)
                {
                    Vector3 position = origin + new Vector3(x * spacingX, y * spacingY, z * spacingZ) * spacing;

                    GameObject moleculePair;

                    // Elegir aleatoriamente el tipo de molécula respetando el patrón 2:1
                    if (ethanolCount < 2 && waterCount < 1)
                    {
                        // Puede elegir aleatoriamente entre etanol y agua
                        if (random.Next(0, 2) == 0)
                        {
                            moleculePair = Instantiate(ethanolPrefab, position, Quaternion.identity);
                            ethanolCount++;
                        }
                        else
                        {
                            moleculePair = Instantiate(waterPrefab, position, Quaternion.identity);
                            waterCount++;
                        }
                    }
                    else if (ethanolCount >= 2)
                    {
                        // Instanciar agua si ya se han instanciado 2 etanol
                        moleculePair = Instantiate(waterPrefab, position, Quaternion.identity);
                        ethanolCount = 0;
                        waterCount = 1;
                    }
                    else
                    {
                        // Instanciar etanol si ya se ha instanciado agua
                        moleculePair = Instantiate(ethanolPrefab, position, Quaternion.identity);
                        waterCount = 0;
                        ethanolCount = 1;
                    }

                    molecules[index] = moleculePair;
                    index++;
                }
            }
        }

        // Desactivar ambos prefabs originales
        ethanolPrefab.SetActive(false);
        waterPrefab.SetActive(false);
    }

    void OnTemperatureSliderChanged(float value)
    {
        // Actualizar el texto de la UI
        temperatureText.text = value.ToString("F1") + "°C";
        UpdateExplanationText(value);

        if (molecules.Length > 0)
        {
            foreach (var molecule in molecules)
            {

                if (molecule.GetComponent<RainBehavior>() != null)
                {
                    float tempIncrement = value - molecule.GetComponent<RainBehavior>().currentTemperature;
                    molecule.GetComponent<RainBehavior>().IncreaseTemperature(tempIncrement);
                }
                else
                {
                    float tempIncrement = value - molecule.GetComponent<DistilationBehavior>().currentTemperature;
                    molecule.GetComponent<DistilationBehavior>().IncreaseTemperature(tempIncrement);
                }
            }
        }
    }

    void SetInitialTemperature(float temperature)
    {
        temperatureText.text = temperature.ToString("F1") + "°C";
        if (molecules.Length > 0)
        {
            foreach (var molecule in molecules)
            {
                if (molecule.GetComponent<RainBehavior>() != null)
                {
                    molecule.GetComponent<RainBehavior>().IncreaseTemperature(temperature - 10f);
                }
                else
                {
                    molecule.GetComponent<DistilationBehavior>().IncreaseTemperature(temperature - 10f);
                }
            }
        }
    }

    void ShowExplanation()
    {
        explanationText.text = "Las moléculas están en una mezcla. A medida que disminuyes la temperatura con el slider, las moléculas comienzan a vibrar simulando su separación.";
    }

    void UpdateExplanationText(float temperature)
    {
        if (temperature > 60f)
        {
            explanationText.text = "Las moléculas están mezcladas y vibran rapidamente debido al estado líquido.";
        }
        else if (temperature <= 60f)
        {
            labCompleted = true;
            explanationText.text = "Al alcanzar los 60º, el yoduro de plomo se comienza a separar del nitrato de potasio";
        }
    }
}
