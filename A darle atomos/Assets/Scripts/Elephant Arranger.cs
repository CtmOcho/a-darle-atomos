using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class ElephantArranger : MonoBehaviour
{
    public GameObject waterPrefab;
    public int sizeX = 3;
    public int sizeY = 3;
    public int sizeZ = 3;
    public float spacing = 1.0f;
    public float spacingX = 3.0f;
    public float spacingY = 3.0f;
    public float spacingZ = 3.0f;
    public bool H2O2 = false;

    public bool labCompleted = false;

    public TMP_Text explanationText;
    private List<GameObject> molecules = new List<GameObject>(); // Lista para almacenar los objetos instanciados

    void Start()
    {
        ArrangeMolecules();
        ShowExplanation();
    }
void Update()
    {
        // Verificar solo si H2O2 es false
        if (!H2O2)
        {
            foreach (GameObject molecule in molecules)
            {
                // Verificar si la posición en y es menor o igual a -7
                if (molecule.transform.position.y <= -7f)
                {
                    Rigidbody rb = molecule.GetComponent<Rigidbody>();
                    if (rb != null)
                    {
                        rb.useGravity = false;
                        rb.velocity = Vector3.zero;
                    }
                }
            }
        }
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
                    Quaternion rotation = H2O2 ? Quaternion.Euler(0, 0, 180.0f) : Quaternion.identity;
                    
                    GameObject molecule = Instantiate(waterPrefab, position, rotation);
                    molecules.Add(molecule); // Agregar el objeto instanciado a la lista
                }
            }
        }

        // Desactivar el prefab original
        waterPrefab.SetActive(false);
    }

    void ShowExplanation()
    {
        explanationText.text = "Solo está el peróxido de hidrógeno y el jabón líquido. El peróxido de hidrógeno es químicamente inestable y tiende a descomponerse lentamente en agua y oxígeno, pero este proceso es muy lento. El jabón está presente en la mezcla, pero no interviene activamente en ninguna reacción.";
    }

    // Método para activar la gravedad en todos los objetos instanciados
    public void ActivateGravity()
    {
        if(!labCompleted){
            foreach (GameObject molecule in molecules)
            {
                Rigidbody rb = molecule.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.useGravity = true;
                    rb.velocity = new Vector3(0, -75.0f, 0);;
                }
            }
        }
    }

    // Método para desactivar la gravedad en todos los objetos instanciados
    public void DeactivateGravity()
    {
        foreach (GameObject molecule in molecules)
        {
            Rigidbody rb = molecule.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.useGravity = false;
            }
        }
    }
}