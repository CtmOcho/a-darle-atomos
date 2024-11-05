using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.UI;

public class ChameleonArranger : MonoBehaviour
{
    public GameObject moleculePrefabContainer; // GameObject que contiene los 4 prefabs y el script controlador
    public int sizeX = 3;
    public int sizeY = 3;
    public int sizeZ = 3;
    public float spacing = 1.0f;
    public float spacingX = 3.0f;
    public float spacingY = 3.0f;
    public float spacingZ = 3.0f;
    public bool H2O2 = false;
    public bool ChameleonlabCompleted = false;

    public Image imagenColor;
    
    public TMP_Text explanationText; // Texto para la explicación
    private List<GameObject> molecules = new List<GameObject>(); // Lista para almacenar los objetos instanciados

    void Start()
    {
        ArrangeMolecules();
        ShowExplanation();
    }

    void ArrangeMolecules()
    {
        Vector3 origin = transform.position - new Vector3(sizeX - 1, sizeY - 1, sizeZ - 1) * spacing / 2;

        for (int x = 0; x < sizeX; x++)
        {
            for (int y = 0; y < sizeY; y++)
            {
                for (int z = 0; z < sizeZ; z++)
                {
                    Vector3 position = origin + new Vector3(x * spacingX, y * spacingY, z * spacingZ) * spacing;

                    // Generar una rotación aleatoria
                    Quaternion randomRotation = Quaternion.Euler(
                        Random.Range(0f, 360f),
                        Random.Range(0f, 360f),
                        Random.Range(0f, 360f)
                    );

                    // Usar la rotación aleatoria para el contenedor de moléculas
                    GameObject moleculeContainer = Instantiate(moleculePrefabContainer, position, randomRotation);
                    molecules.Add(moleculeContainer); // Agregar el objeto instanciado a la lista
                }
            }
        }

        // Desactivar el prefab original
        moleculePrefabContainer.SetActive(false);
    }

    void ShowExplanation()
    {
        explanationText.text = "Esta es una solución de agua pura. Ajusta el pH con el deslizador para observar cómo las moléculas reaccionan en función de la acidez o alcalinidad.";
    }

    public void UpdateExplanationText(int previousLevel, int newLevel)
    {
        string explanation = "";

        // Determina el texto basado en la transición de estados
        if (previousLevel == 4 && newLevel == 3)
        {
            explanation = "El permanganato de potasio (KMnO4) se reduce a manganato de potasio (K2MnO4) al perder un átomo de potasio.";
            imagenColor.color = new Color(0.0f, 1.0f, 0.0f);
        }
        else if (previousLevel == 3 && newLevel == 4)
        {
            explanation = "El manganato de potasio (K2MnO4) se oxida a permanganato de potasio (KMnO4) al ganar un átomo de potasio.";
            imagenColor.color = new Color(0.5f, 0.0f, 0.5f);

        }
        else if (previousLevel == 3 && newLevel == 2)
        {
            explanation = "El manganato de potasio (K2MnO4) se reduce a hipomanganato de potasio (K2MnO4) al ganar un átomo de potasio adicional.";
            imagenColor.color = new Color(0.0f, 0.0f, 1.0f);
        
        }
        else if (previousLevel == 2 && newLevel == 3)
        {
            explanation = "El hipomanganato de potasio (K2MnO4) se oxida a manganato de potasio (K2MnO4) al perder un átomo de potasio.";
            imagenColor.color = new Color(0.0f, 1.0f, 0.0f);
        
        }
        else if (previousLevel == 2 && newLevel == 1)
        {
            explanation = "El hipomanganato de potasio (K2MnO4) se reduce a dióxido de manganeso (MnO2) al perder tanto potasio como oxígeno adicional.";
            imagenColor.color = new Color(0.5f, 0.25f, 0.0f);
            ChameleonlabCompleted = true;
        
        }
        else if (previousLevel == 1 && newLevel == 2)
        {
            explanation = "El dióxido de manganeso (MnO2) se oxida a hipomanganato de potasio (K2MnO4) al ganar potasio y oxígeno adicional.";
            imagenColor.color = new Color(0.0f, 0.0f, 1.0f);
        }
        else
        {
            explanation = "Estado actual de la molécula.";
        }

        explanationText.text = explanation;
    }

    // Método para oxidar todas las moléculas
    public void buttonOxidize()
    {
        if (molecules.Count > 0)
        {
            // Obtener el nivel de oxidación actual de la primera molécula (asumiendo que todas están sincronizadas)
            var moleculeController = molecules[0].GetComponent<ChameleonBehaviourController>();
            int previousLevel = moleculeController.oxidationLevel;

            // Aplicar oxidación a todas las moléculas
            foreach (GameObject molecule in molecules)
            {
                molecule.GetComponent<ChameleonBehaviourController>().Oxidize();
            }

            // Actualizar el texto de explicación después de la oxidación
            UpdateExplanationText(previousLevel, moleculeController.oxidationLevel);
        }
    }

    // Método para reducir todas las moléculas
    public void buttonReduce()
    {
        if (molecules.Count > 0)
        {
            // Obtener el nivel de oxidación actual de la primera molécula (asumiendo que todas están sincronizadas)
            var moleculeController = molecules[0].GetComponent<ChameleonBehaviourController>();
            int previousLevel = moleculeController.oxidationLevel;

            // Aplicar reducción a todas las moléculas
            foreach (GameObject molecule in molecules)
            {
                molecule.GetComponent<ChameleonBehaviourController>().Reduce();
            }

            // Actualizar el texto de explicación después de la reducción
            UpdateExplanationText(previousLevel, moleculeController.oxidationLevel);
        }
    }
}
