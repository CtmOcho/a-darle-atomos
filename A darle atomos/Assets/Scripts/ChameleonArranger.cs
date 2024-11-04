using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

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

    public TMP_Text explanationText;
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

    public void UpdateExplanationText(float pH)
    {
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
            explanationText.text = "El pH es básico. A medida que el pH aumenta, se reduce la concentración de protones (H+) y las moléculas pueden formar iones hidróxido (OH-).";
        }
    }

    // Método para oxidar todas las moléculas
    public void buttonOxidize()
    {
        foreach (GameObject molecule in molecules)
        {
            molecule.GetComponent<ChameleonBehaviourController>().Oxidize();
        }
    }

    // Método para reducir todas las moléculas
    public void buttonReduce()
    {
        foreach (GameObject molecule in molecules)
        {
            molecule.GetComponent<ChameleonBehaviourController>().Reduce();
        }
    }
}
