using UnityEngine;
using System.Collections;
using TMPro;

public class ElephantBehaviour : MonoBehaviour
{
    public GameObject oxygenPrefab; // Prefab para el O₂ resultante
    public GameObject waterPrefab; // Prefab para el H₂O resultante
    public TMP_Text explanationText;
    public ElephantArranger arrangerScript;

    private bool reactionOccurred = false; // Bandera para verificar si la reacción ya ocurrió
    private Rigidbody[] particleRigidbodies;

    public float vibrationIntensity = 0f; // Intensidad de la vibración
    public float maxVibrationIntensity = 5f; // Máxima intensidad de la vibración
    public float vibrationSpeed = 5f; // Velocidad de la vibración
    public bool isVibrating = false; // Estado de vibración de la molécula
    public string moleculeType; // Tipo de molécula

    void Start()
    {
        // Obtener todos los Rigidbodies de las partículas
        particleRigidbodies = GetComponentsInChildren<Rigidbody>();
            StartVibration();

    }

    void Update()
    {

        if (isVibrating)
        {
            VibrateXZ(); // Llamar a la vibración en X y Z
        }
    }

    void VibrateXZ()
    {
        foreach (Rigidbody rb in particleRigidbodies)
        {
            // Generar vibración en los ejes X y Z, sin afectar el eje Y
            Vector3 vibration = new Vector3(
                Random.Range(-1f, 1f) * vibrationIntensity * Time.deltaTime,
                0, // No hay vibración en Y
                Random.Range(-1f, 1f) * vibrationIntensity * Time.deltaTime
            );

            // Aplicar un factor de amortiguación para evitar movimientos excesivos
            rb.velocity *= 0.9f;
            rb.MovePosition(rb.position + vibration);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Verificar si la reacción ya ocurrió
        if(moleculeType == "H2O2"){

        if (reactionOccurred) {
            arrangerScript = arrangerScript.GetComponent<ElephantArranger>();
            arrangerScript.labCompleted = true;
            return;
        }
        explanationText.text = "Al agregar el KI, este actúa como catalizador para la descomposición del peróxido de hidrógeno, acelerando la reacción. El peróxido de hidrógeno se descompone rápidamente en agua y oxígeno, liberando burbujas de oxígeno que el jabón atrapa para formar una gran cantidad de espuma. Esta reacción es exotérmica, por lo que genera calor y hace que la espuma esté tibia.";

        // Verificar si el objeto colisionado es el KI y este objeto es H₂O₂
        if (other.gameObject.CompareTag("KI") && gameObject.CompareTag("H2O2"))
        {
            // Desactivar la gravedad de KI
            Rigidbody kiRb = other.gameObject.GetComponent<Rigidbody>();
            if (kiRb != null)
            {
                StartCoroutine(GraduallyStopObject(kiRb));
            }

            // Marcar que la reacción ha ocurrido
            reactionOccurred = true;

            // Llamar a la función para reorganizar moléculas de H₂O₂
            TriggerDecomposition(other.gameObject, this.gameObject);


        }
        
            // Iniciar la vibración
        }
    }

    private IEnumerator GraduallyStopObject(Rigidbody rb)
    {
        while (rb.transform.position.y > -7f)
        {
            rb.velocity = Vector3.Lerp(rb.velocity, Vector3.zero, Time.deltaTime * 15); // Ajusta la velocidad de desaceleración aquí
            yield return null; // Esperar al siguiente frame
        }

        rb.velocity = Vector3.zero;
        rb.useGravity = false;
        rb.isKinematic = true;
    }

    public void StartVibration()
    {
        isVibrating = true;
        vibrationIntensity = Mathf.Clamp(Mathf.Lerp(0f, maxVibrationIntensity, 0.5f), 0f, maxVibrationIntensity);
    }

    IEnumerator AttractNearbyMolecules(Transform o2_1, Transform o2_2, float attractionSpeed)
    {
        Rigidbody o2Rb1 = o2_1.GetComponent<Rigidbody>();
        Rigidbody o2Rb2 = o2_2.GetComponent<Rigidbody>();

        while (Vector3.Distance(o2_1.position, o2_2.position) > 1f)
        {
            Vector3 directionToOther = (o2_2.position - o2_1.position).normalized;
            o2Rb1.velocity += directionToOther * attractionSpeed * Time.deltaTime;
            o2Rb2.velocity -= directionToOther * attractionSpeed * Time.deltaTime;

            yield return null; 
        }

        o2Rb1.velocity = Vector3.zero;
        o2Rb2.velocity = Vector3.zero;
        o2Rb1.velocity = new Vector3(0, 12.0f, 0);
        o2Rb2.velocity = new Vector3(0, 12.0f, 0);
    }

    void TriggerDecomposition(GameObject kiObject, GameObject h2o2Object)
    {
        Transform h1_m1 = h2o2Object.transform.Find("h1_m1");
        Transform h2_m1 = h2o2Object.transform.Find("h2_m1");
        Transform o1_m1 = h2o2Object.transform.Find("o1_m1");
        Transform o2_m1 = h2o2Object.transform.Find("o2_m1");

        Transform h1_m2 = h2o2Object.transform.Find("h1_m2");
        Transform h2_m2 = h2o2Object.transform.Find("h2_m2");
        Transform o1_m2 = h2o2Object.transform.Find("o1_m2");
        Transform o2_m2 = h2o2Object.transform.Find("o2_m2");

        if (h1_m1 != null && h2_m1 != null && o1_m1 != null && o2_m1 != null && 
            h1_m2 != null && h2_m2 != null && o1_m2 != null && o2_m2 != null)
        {
            Rigidbody o2Rb1 = o2_m1.GetComponent<Rigidbody>();
            Rigidbody o2Rb2 = o2_m2.GetComponent<Rigidbody>();

            if (o2Rb1 == null)
            {
                o2Rb1 = o2_m1.gameObject.AddComponent<Rigidbody>();
            }
            if (o2Rb2 == null)
            {
                o2Rb2 = o2_m2.gameObject.AddComponent<Rigidbody>();
            }

            StartCoroutine(AttractNearbyMolecules(o2_m1, o2_m2, 3f));

            o2Rb1.velocity = new Vector3(0, 100.0f, 0);
            o2Rb2.velocity = new Vector3(0, 100.0f, 0);
        }
        else
        {
            Debug.LogWarning("No se encontraron todas las moléculas necesarias para la reacción en los objetos colisionados.");
        }
    }
}
