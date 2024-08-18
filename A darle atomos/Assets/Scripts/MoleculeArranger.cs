using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MoleculeArranger : MonoBehaviour
{
    public GameObject moleculePairPrefab;
    public int gridSize = 3;
    public float spacing = 1.0f;

    public Slider temperatureSlider;
    public TMP_Text temperatureText;
    public TMP_Text explanationText;  // Texto para mostrar la explicación
    private GameObject[] molecules;

    private bool hasReachedMaxTemperature = false;  // Booleano para controlar si se ha alcanzado la temperatura máxima

    void Start()
    {
        int moleculeCount = gridSize * gridSize * gridSize;
        molecules = new GameObject[moleculeCount];
        ArrangeMolecules();

        temperatureSlider.onValueChanged.AddListener(delegate { OnTemperatureSliderChanged(temperatureSlider.value); });

        SetInitialTemperature(temperatureSlider.value);
        ShowExplanation();  // Mostrar la explicación al inicio
    }

    void ArrangeMolecules()
    {
        Vector3 origin = transform.position - new Vector3(gridSize - 1, gridSize - 1, gridSize - 1) * spacing / 2;
        int index = 0;
        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                for (int z = 0; z < gridSize; z++)
                {
                    Vector3 position = origin + new Vector3(x*2, y, z) * spacing;
                    GameObject moleculePair = Instantiate(moleculePairPrefab, position, Quaternion.identity);
                    molecules[index] = moleculePair;
                    index++;
                }
            }
        }
        moleculePairPrefab.SetActive(false);
    }
void OnTemperatureSliderChanged(float value)
{
    // Actualizar el texto de la UI
    temperatureText.text = value.ToString("F1") + "°C";
    UpdateExplanationText(value);  // Actualizar la explicación basada en la temperatura

    if (molecules.Length > 0)
    {
        // Verificar si se ha alcanzado la temperatura máxima
        if (value >= 114f)
        {
            hasReachedMaxTemperature = true;
        }

        foreach (var molecule in molecules)
        {
            float tempIncrement = value - molecule.GetComponent<MoleculeBehavior>().currentTemperature;
            molecule.GetComponent<MoleculeBehavior>().IncreaseTemperature(tempIncrement);
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
            molecule.GetComponent<MoleculeBehavior>().IncreaseTemperature(temperature - 20f);
        }
    }
}


    void ShowExplanation()
    {
        explanationText.text = "Las moléculas de yodo están en estado sólido. A medida que aumentas la temperatura con el slider, las moléculas comienzan a vibrar. \n\n" +
                               "Entre 20º y 60º, la vibración aumenta lentamente, ya que la estructura sólida sigue siendo estable. \n\n" +
                               "Entre 60º y 113º, la energía se transmite más rápidamente a través de toda la estructura, aumentando la vibración en todas las moléculas. \n\n" +
                               "Al alcanzar los 114º, el yodo se sublima: las moléculas se separan y vibran intensamente, representando el estado gaseoso.";
    }

    void UpdateExplanationText(float temperature)
    {
        if (temperature < 60f)
        {
            explanationText.text = "Las moléculas están en estado sólido y vibran lentamente.";
        }
        else if (temperature >= 60f && temperature < 114f)
        {
            explanationText.text = "Las moléculas están transmitiendo la energía a través de la estructura, aumentando la vibración.";
        }
        else if (temperature >= 114f)
        {
            explanationText.text = "Las moléculas se han sublimado, separándose y vibrando intensamente.";
        }
    }
}
