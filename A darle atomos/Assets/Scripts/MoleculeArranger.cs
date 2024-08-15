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
    private GameObject[] molecules;

    private bool hasReachedMaxTemperature = false;  // Booleano para controlar si se ha alcanzado la temperatura máxima

    void Start()
    {
        int moleculeCount = gridSize * gridSize * gridSize;
        molecules = new GameObject[moleculeCount];
        ArrangeMolecules();

        temperatureSlider.onValueChanged.AddListener(delegate { OnTemperatureSliderChanged(temperatureSlider.value); });

        SetInitialTemperature(temperatureSlider.value);
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

        if (molecules.Length > 0)
        {
            if (value >= 114f)
            {
                hasReachedMaxTemperature = true;
                //molecules[0].GetComponent<MoleculeBehavior>().IncreaseTemperature(value - 20f);
            }else{
                  // Asegurarse de no aplicar un incremento masivo de temperatura
        float tempIncrement = value - molecules[0].GetComponent<MoleculeBehavior>().currentTemperature;
        molecules[0].GetComponent<MoleculeBehavior>().IncreaseTemperature(tempIncrement);
            }
            if (hasReachedMaxTemperature)
            {
                foreach (var molecule in molecules)
                {
                    // Reducir la temperatura de todas las moléculas
                    molecule.GetComponent<MoleculeBehavior>().IncreaseTemperature(value - molecule.GetComponent<MoleculeBehavior>().currentTemperature);
                }
            }
        }
    }

    void SetInitialTemperature(float temperature)
    {
        temperatureText.text = temperature.ToString("F1") + "°C";
        if (molecules.Length > 0)
        {
            molecules[0].GetComponent<MoleculeBehavior>().IncreaseTemperature(temperature - 20f);
        }
    }
}
