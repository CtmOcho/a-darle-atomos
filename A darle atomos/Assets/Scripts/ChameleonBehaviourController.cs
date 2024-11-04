using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChameleonBehaviourController : MonoBehaviour
{

    public GameObject KMnO4Prefab;
    public GameObject K2MnO4Prefab;
    public GameObject K3MnO4Prefab;
    public GameObject MnO2Prefab;

    private int oxidationLevel = 4; // Nivel de oxidación inicial (KMnO4)
    private GameObject currentPrefab;

    void Start()
    {
        UpdateStructure(); // Configurar el primer estado visible al inicio
    }

    public void Oxidize()
    {
        if (oxidationLevel < 4)
        {
            oxidationLevel++;
            UpdateStructure();
        }
    }

    public void Reduce()
    {
        if (oxidationLevel > 1)
        {
            oxidationLevel--;
            UpdateStructure();
        }
    }

    private void UpdateStructure()
    {
        // Desactivar todos los prefabs
        KMnO4Prefab.SetActive(false);
        K2MnO4Prefab.SetActive(false);
        K3MnO4Prefab.SetActive(false);
        MnO2Prefab.SetActive(false);

        // Mantener la rotación del objeto padre para el nuevo prefab
        Quaternion parentRotation = transform.rotation;

        // Activar el prefab correspondiente al nivel de oxidación actual y aplicar la rotación del padre
        switch (oxidationLevel)
        {
            case 4:
                KMnO4Prefab.SetActive(true);
                currentPrefab = KMnO4Prefab;
                break;
            case 3:
                K2MnO4Prefab.SetActive(true);
                currentPrefab = K2MnO4Prefab;
                break;
            case 2:
                K3MnO4Prefab.SetActive(true);
                currentPrefab = K3MnO4Prefab;
                break;
            case 1:
                MnO2Prefab.SetActive(true);
                currentPrefab = MnO2Prefab;
                break;
        }

        // Aplicar la rotación del padre al prefab activado
        if (currentPrefab != null)
        {
            currentPrefab.transform.rotation = parentRotation;
        }
    }
}
