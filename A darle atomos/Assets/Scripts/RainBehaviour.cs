using Unity.VisualScripting;
using UnityEngine;

public class RainBehavior : MonoBehaviour
{
    public float initialTemperature = 10f; // Temperatura inicial de la molécula
    public float maxTemperature = 80f; // Temperatura máxima de la molécula
    public float vibrationIntensity = 0f; // Intensidad de la vibración
    public float maxVibrationIntensity = 5f; // Máxima intensidad de la vibración
    public float vibrationSpeed = 5f; // Velocidad de la vibración
    public bool isVibrating = false; // Estado de vibración de la molécula
    public string moleculeType; // Tipo de molécula: "Agua" o "Etanol"
    public float minHeight = 0f;

    private Rigidbody[] particleRigidbodies;
    public float currentTemperature;
    private Vector3[] initialLocalPositions; // Posiciones locales iniciales de las partículas

    void Start()
    {
        particleRigidbodies = GetComponentsInChildren<Rigidbody>();
        currentTemperature = initialTemperature;

        // Guardar las posiciones locales iniciales
        initialLocalPositions = new Vector3[particleRigidbodies.Length];
        for (int i = 0; i < particleRigidbodies.Length; i++)
        {
            initialLocalPositions[i] = particleRigidbodies[i].transform.localPosition;
        }
    }

    void Update()
    {
        if (isVibrating)
        {
            Vibrate();
        }

        MaintainStructure(); // Intentar mantener la estructura unida
    }

    void Vibrate()
    {
        foreach (Rigidbody rb in particleRigidbodies)
        {
            // Verificar si la temperatura ha alcanzado o superado la temperatura máxima
            bool isMaxTemperatureReached = currentTemperature <= maxTemperature;

            // Generar vibración con un sesgo hacia el eje Y positivo solo si se ha alcanzado la temperatura máxima
            Vector3 vibration;
            if (isMaxTemperatureReached && rb.gameObject.transform.position.y > minHeight)
            {
                vibration = new Vector3(
                    Random.Range(-1f, 1f) * vibrationIntensity * Time.deltaTime,
                    -Mathf.Abs(Random.Range(0f, 1f)) * vibrationIntensity * Time.deltaTime * 0.7f, // Sesgo más fuerte hacia el eje Y positivo
                    Random.Range(-1f, 1f) * vibrationIntensity * Time.deltaTime
                );
            }
            else
            {
                vibration = new Vector3(
                    Random.Range(1f, -1f) * vibrationIntensity * Time.deltaTime,
                    //Mathf.Abs(Random.Range(-1f, 1f)) * vibrationIntensity * Time.deltaTime, // Sesgo ligero hacia el eje Y positivo
                    Random.Range(-1f, 1f) * vibrationIntensity * Time.deltaTime
                );
            }

            // Aplicar un factor de amortiguación para evitar movimientos excesivos
            rb.velocity *= 0.9f;

            rb.MovePosition(rb.position + vibration);
        }
    }

    void MaintainStructure()
    {
        for (int i = 0; i < particleRigidbodies.Length; i++)
        {
            Rigidbody rb = particleRigidbodies[i];
            Vector3 targetPosition = transform.TransformPoint(initialLocalPositions[i]);
            Vector3 forceDirection = (targetPosition - rb.position).normalized;

            // Aplicar una fuerza para atraer la partícula a su posición inicial relativa
            rb.AddForce(forceDirection * vibrationSpeed * Time.deltaTime);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        DistilationBehavior otherMolecule = collision.gameObject.GetComponent<DistilationBehavior>();

        if (otherMolecule != null)
        {
            // Transmitir la temperatura si la otra molécula está más caliente
            if (otherMolecule.currentTemperature > currentTemperature)
            {
                currentTemperature = otherMolecule.currentTemperature;
            }

            // Asegurar que la molécula comience a vibrar al colisionar
            StartVibration();
        }
    }

    public void StartVibration()
    {
        isVibrating = true;

        // Limitar la intensidad de la vibración a un valor razonable
        vibrationIntensity = Mathf.Clamp(
            Mathf.Lerp(0f, maxVibrationIntensity, Mathf.InverseLerp(10f, maxTemperature, currentTemperature)),
            0f,
            maxVibrationIntensity
        );

        // Modificar la velocidad de vibración según el tipo de molécula
        float volatilityFactor = (moleculeType == "Etanol") ? 3.0f : 1.5f; // Etanol vibra más rápido
        vibrationSpeed = Mathf.Lerp(0f, maxVibrationIntensity * volatilityFactor, Mathf.InverseLerp(20f, maxTemperature, currentTemperature));
    }

    void OnCollisionEnter(Collider other)
    {
        if (other.CompareTag("Boundary"))
        {
            foreach (Rigidbody rb in particleRigidbodies)
            {
                if (rb != null)
                {
                    // Aplicar una fuerza hacia el centro del cubo
                    Vector3 directionToCenter = (Vector3.zero - rb.position).normalized;
                    rb.AddForce(directionToCenter * 2000f); // Ajusta la magnitud de la fuerza según sea necesario
                }
            }
        }
    }

    public void IncreaseTemperature(float amount)
    {
        currentTemperature += amount;
        StartVibration();
    }

    public void AttractNearbyMolecules()
    {
        // Encuentra todas las moléculas cercanas y atrae esta molécula hacia ellas
        Collider[] nearbyMolecules = Physics.OverlapSphere(transform.position, 0.45f); // Radio ajustable

        foreach (Collider collider in nearbyMolecules)
        {
            DistilationBehavior otherMolecule = collider.GetComponentInParent<DistilationBehavior>();

            if (otherMolecule != null && otherMolecule != this)
            {
                // Atrae la molécula hacia la posición de la otra molécula cercana
                Vector3 directionToOther = (otherMolecule.transform.position - transform.position).normalized;
                foreach (Rigidbody rb in particleRigidbodies)
                {
                    rb.velocity += directionToOther * vibrationSpeed * 0.3f * Time.deltaTime;
                }
            }
        }
    }
}
