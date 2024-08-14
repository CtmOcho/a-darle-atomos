using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MoleculeArranger : MonoBehaviour
{
    public PhysicMaterial gaseousMaterial;
    public PhysicMaterial solidMaterial;

    public GameObject moleculePrefab;
    public int gridSize = 3;
    public float spacing = 1.0f;

    public float sublimationTemperature = 114f; // Temperatura a la que ocurre la sublimación
    public float movementThreshold = 60f;       // Umbral para empezar a moverse más rápido
    public float coolingThreshold = 30f;        // Temperatura donde el movimiento se reduce a 0
    public float currentTemperature = 25f;      // Temperatura actual

    public TMP_Text temperatureText;
    public Slider temperatureSlider;

    private GameObject[] molecules;
    private Vector3[] originalPositions;

    void Start()
    {
        int moleculeCount = gridSize * gridSize * gridSize;
        molecules = new GameObject[moleculeCount];
        originalPositions = new Vector3[moleculeCount];
        ArrangeMolecules();

        SetTemperature(temperatureSlider.value);
        temperatureSlider.onValueChanged.AddListener(SetTemperature);
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
                    Vector3 position = origin + new Vector3(x, y, z) * spacing;
                    GameObject molecule = Instantiate(moleculePrefab, position, Quaternion.identity);
                    molecules[index] = molecule;
                    originalPositions[index] = position;
                    index++;
                }
            }
        }

        moleculePrefab.SetActive(false);
    }

    void Update()
    {
        HandleMoleculeBehavior();
    }

    void HandleMoleculeBehavior()
    {
        for (int i = 0; i < molecules.Length; i++)
        {
            GameObject molecule = molecules[i];
            Rigidbody rb = molecule.GetComponent<Rigidbody>();

            if (currentTemperature >= movementThreshold)
            {
                // Si la temperatura es superior a 60ºC, las moléculas empiezan a moverse
                float t = Mathf.InverseLerp(movementThreshold, sublimationTemperature, currentTemperature);
                float movementSpeed = Mathf.Lerp(0f, 5f, t);  // Velocidad del movimiento
                float dragValue = Mathf.Lerp(1f, 0.1f, t);    // Suavizado del arrastre
                float angularDragValue = Mathf.Lerp(1f, 0.1f, t);  // Suavizado del arrastre angular

                rb.isKinematic = false;
                rb.useGravity = false;
                rb.drag = dragValue;
                rb.angularDrag = angularDragValue;
                rb.GetComponent<Collider>().material = gaseousMaterial;

                // Movimiento utilizando MovePosition
                Vector3 randomDirection = Random.insideUnitSphere;
                Vector3 newPosition = rb.position + randomDirection * movementSpeed * Time.deltaTime;

                rb.MovePosition(newPosition);
            }
            else if (currentTemperature < movementThreshold)
            {
                // Si la temperatura baja por debajo de 60ºC, gradualmente se detienen
                float t = Mathf.InverseLerp(coolingThreshold, 0f, currentTemperature);
                float dragValue = Mathf.Lerp(0.1f, 1f, t);  // Incremento del arrastre para detener las moléculas

                rb.drag = dragValue;
                rb.angularDrag = dragValue;
                
                if (currentTemperature <= coolingThreshold && t == 1f)
                {
                    rb.isKinematic = true;
                }
            }
        }
    }

    public void SetTemperature(float temperature)
    {
        currentTemperature = temperature;
        UpdateTemperatureDisplay(temperature);
        //Debug.Log("Current Temperature: " + currentTemperature);
    }

    private void UpdateTemperatureDisplay(float temperature)
    {
        if (temperatureText != null)
        {
            temperatureText.text = temperature.ToString("F1") + "°C";
        }
    }
}
