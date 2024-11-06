using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class pHArranger : MonoBehaviour
{
    public GameObject waterPrefab;
    public Image imagenColor;

    public int sizeX = 3;
    public int sizeY = 3;
    public int sizeZ = 3;
    public float spacingX = 3.0f;
    public float spacingY = 3.0f;
    public float spacingZ = 3.0f;
    public float spacing = 1.0f;

    public Slider pHSlider;
    public TMP_Text pHText;
    public TMP_Text explanationText;
    private GameObject[] molecules;
    
    public bool labCompleted = false;

    void Start()
    {
        int moleculeCount = sizeX * sizeY * sizeZ;
        molecules = new GameObject[moleculeCount];
        ArrangeMolecules();

        imagenColor = imagenColor.GetComponent<Image>();

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
                    Vector3 position = origin + new Vector3(x * spacingX, y * spacingY, z * spacingZ) * spacing;
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
    pHText.text = "pH: " + value.ToString("F1");
    UpdateExplanationText(value);

    if (molecules.Length > 0)
    {
        foreach (var molecule in molecules)
        {
            // Asegúrate de que el valor del Slider se esté pasando correctamente
            molecule.GetComponent<pHBehavior>().AdjustpH(value);
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
                molecule.GetComponent<pHBehavior>().AdjustpH(pH); // Configura el pH inicial cerca del neutro (pH 7)
            }
        }
    }

void ShowExplanation()
{
    imagenColor.color = PhToColor(7.0f);
    explanationText.text = "Esta es una solución de agua pura. Ajusta el pH con el deslizador para observar cómo las moléculas reaccionan en función de la acidez o alcalinidad.";
}

void UpdateExplanationText(float pH)
{
    imagenColor.color = PhToColor(pH);
    if (pH < 7)
    {
        explanationText.text = "El pH es ácido. A medida que el pH disminuye, las moléculas de agua liberan protones (H+), aumentando la concentración de iones de hidrógeno.";
    }
    else if (pH == 7)
    {
        explanationText.text = "El pH es neutro. El agua está en equilibrio, sin una tendencia ácida o básica significativa.";
    }
    else if (pH > 7)
    {
        labCompleted = true;
        explanationText.text = "El pH es básico. A medida que el pH aumenta, se reduce la concentración de protones (H+) y las moléculas pueden formar iones hidróxido (OH-).";
    }
}

        Color PhToColor(float ph)
    {
        // Definimos los valores de color correspondientes a los valores de pH
        Color[] phColors = new Color[]
        {
            new Color(238/255f,28/255f,37/255f),  // pH 0
            new Color(242/255f, 103 / 255f, 36/255f),  // pH 1
            new Color(249/255f, 197 / 255f, 17/255f),  // pH 2
            new Color(245/255f, 237/255f, 28/255f),  // pH 3
            new Color(181/255f, 211/255f, 51/255f),  // pH 4
            new Color(132/255f, 195 / 255f, 65/255f),  // pH 5
            new Color(77/255f, 183/255f, 73/255f),  // pH 6
            new Color(51/255f, 169/255f, 75 / 255f),  // pH 7
            new Color(34/255f, 180/255f, 107/255f),  // pH 8
            new Color(11/255f, 184/255f, 182/255f),  // pH 9
            new Color(70/255f, 144 / 255f, 205/255f),  // pH 10
            new Color(56/255f, 83 / 255f, 164/255f),  // pH 11
            new Color(90/255f, 81/255f, 162/255f),  // pH 12
            new Color(99/255f, 69 / 255f, 157/255f),  // pH 13
            new Color(70/255f, 44/255f, 131/255f)   // pH 14
        };

        // Calculamos los índices para la interpolación entre los valores más cercanos
        int lowerIndex = Mathf.FloorToInt(ph);  // Valor entero menor o igual
        int upperIndex = Mathf.CeilToInt(ph);   // Valor entero mayor o igual

        // Asegurarnos de que los índices están dentro de los límites del array
        lowerIndex = Mathf.Clamp(lowerIndex, 0, phColors.Length - 1);
        upperIndex = Mathf.Clamp(upperIndex, 0, phColors.Length - 1);

        // Si el pH es un valor entero, devolvemos el color correspondiente directamente
        if (lowerIndex == upperIndex)
        {
            return phColors[lowerIndex];
        }

        // Interpolamos entre los dos colores correspondientes a los valores de pH cercanos
        float t = ph - lowerIndex;  // Valor decimal entre 0 y 1 que indica qué tan cerca está del siguiente valor
        return Color.Lerp(phColors[lowerIndex], phColors[upperIndex], t);
    }
}
