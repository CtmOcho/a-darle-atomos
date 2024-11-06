using UnityEngine;
using UnityEngine.UI;
using TMPro; 
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class SodiumWaterReaction : MonoBehaviour
{
    public GameObject waterPrefab;
    public GameObject sodiumPrefab;
    public int initialWaterCount = 5;
    public int amountToRelease = 5; // Variable para determinar la cantidad de sodio liberada

    public Button releaseButton;
    public TMP_Text buttonText; 
    public TMP_Text reactionText;

    public bool labCompleted = false;

    private bool isReleased = false;

    private void Start()
    {
        GenerateWater(initialWaterCount);

        releaseButton.onClick.AddListener(OnButtonClick);

        if (buttonText != null)
        {
            buttonText.text = "Liberar";
        }
        else
        {
            Debug.LogError("buttonText no está asignado en el inspector.");
        }
    }

    private void OnButtonClick()
    {
        if (buttonText == null || reactionText == null)
        {
            Debug.LogError("buttonText o reactionText no están asignados.");
            return;
        }

        if (!isReleased)
        {
            ReleaseSodium();
            labCompleted = true;
            buttonText.text = "Re-establecer";
            reactionText.text = "El sodio reaccionó con el agua formando hidróxido de sodio y liberando hidrógeno. La ecuación es: 2Na + 2H2O -> 2NaOH + H2. El calor generado puede incluso encender el hidrógeno, generando la explosión si la cantidad de hidrógeno que se incendia en un instante es suficiente";
            isReleased = true;
        }
        else
        {
            ReloadScene();
            buttonText.text = "Liberar";
            isReleased = false;
        }
    }

    public void GenerateWater(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Vector3 waterPosition = new Vector3(
                Random.Range(-300f, 443f), 
                Random.Range(0f, -280f),
                840f
            );

            Instantiate(waterPrefab, waterPosition, Quaternion.identity);
        }
    }

    public void ReleaseSodium()
    {
        GameObject[] waterMolecules = GameObject.FindGameObjectsWithTag("H2O");
        List<int> usedIndices = new List<int>();

        for (int i = 0; i < amountToRelease; i++)
        {
            if (waterMolecules.Length > 0 && usedIndices.Count < waterMolecules.Length)
            {
                int index;
                do
                {
                    index = Random.Range(0, waterMolecules.Length);
                }
                while (usedIndices.Contains(index));

                usedIndices.Add(index);
                GameObject water = waterMolecules[index];
                Vector3 waterPosition = water.transform.position;

                Vector3 sodiumPosition = new Vector3(
                    waterPosition.x,
                    Random.Range(108f,380f),
                    waterPosition.z
                );

                GameObject sodium = Instantiate(sodiumPrefab, sodiumPosition, Quaternion.identity);

                NaBehavior sodiumBehavior = sodium.GetComponent<NaBehavior>();
                sodiumBehavior.Initialize(water, sodiumBehavior.prefabNaOH, sodiumBehavior.prefabH2);

                Rigidbody rb = sodium.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.velocity = new Vector3(0, -100f, 0);
                }
            }
        }
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
