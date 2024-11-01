using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class pHArranger : MonoBehaviour
{
    public GameObject waterPrefab;
    public int sizeX = 3;
    public int sizeY = 3;
    public int sizeZ = 3;
    public float spacing = 1.0f;

    public Slider pHSlider;
    public TMP_Text pHText;
    public TMP_Text explanationText;
    private GameObject[] molecules;

    void Start()
    {
        int moleculeCount = sizeX * sizeY * sizeZ;
        molecules = new GameObject[moleculeCount];
        ArrangeMolecules();

        pHSlider.onValueChanged.AddListener(delegate { OnpHSliderChanged(pHSlider.value); });

        SetInitialpH(pHSlider.value);
        ShowExplanation();
    }

    void ArrangeMolecules()
    {
        Vector3 origin = transform.position - new Vector3(sizeX - 1, sizeY - 1, sizeZ - 1) * spacing / 2;
        int index = 0;

        for (int x = 0; x < sizeX; x++)
        {
            for (int y = 0; y < sizeY; y++)
            {
                for (int z = 0; z < sizeZ; z++)
                {
                    Vector3 position = origin + new Vector3(x * 5, y * 3, z * 1.5f) * spacing;
                    GameObject molecule = Instantiate(waterPrefab, position, Quaternion.identity);
                    molecules[index] = molecule;
                    index++;
                }
            }
        }

        // Desactivar el prefab original
        waterPrefab.SetActive(false);
    }

    void OnpHSliderChanged(float value)
    {
        // Actualizar el texto de la UI para el valor de pH
        pHText.text = "pH: " + value.ToString("F1");
        UpdateExplanationText(value);

        if (molecules.Length > 0)
        {
            foreach (var molecule in molecules)
            {
                float pHChange = value - molecule.GetComponent<pHBehavior>().currentpH;
                molecule.GetComponent<pHBehavior>().AdjustpH(pHChange);
            }
        }
    }

    void SetInitialpH(float pH)
    {
        pHText.text = "pH: " + pH.ToString("F1");
        if (molecules.Length > 0)
        {
            foreach (var molecule in molecules)
            {
                molecule.GetComponent<pHBehavior>().AdjustpH(pH - 7f); // Configura el pH inicial cerca del neutro (pH 7)
            }
        }
    }

    void ShowExplanation()
    {
        explanationText.text = "Esta es una soluci�n de agua pura. Ajusta el pH con el deslizador para observar c�mo las mol�culas reaccionan en funci�n de la acidez o alcalinidad.";
    }

    void UpdateExplanationText(float pH)
    {
        if (pH < 7)
        {
            explanationText.text = "El pH es �cido. A medida que el pH disminuye, las mol�culas de agua liberan protones (H?), aumentando la concentraci�n de iones de hidr�geno.";
        }
        else if (pH == 7)
        {
            explanationText.text = "El pH es neutro. El agua est� en equilibrio, sin una tendencia �cida o b�sica significativa.";
        }
        else if (pH > 7)
        {
            explanationText.text = "El pH es b�sico. A medida que el pH aumenta, se reduce la concentraci�n de protones (H?) y las mol�culas pueden formar iones hidr�xido (OH?).";
        }
    }
}
