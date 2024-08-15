using UnityEngine;

public class MoleculeBehavior : MonoBehaviour
{
    public float initialTemperature = 20f; // Temperatura inicial de la molécula
    public float vibrationIntensity = 0f; // Intensidad de la vibración
    public float maxVibrationIntensity = 5f; // Máxima intensidad de la vibración
    public float vibrationSpeed = 5f; // Velocidad de la vibración
    public bool isVibrating = false; // Estado de vibración de la molécula

    private Rigidbody rb;
    public float currentTemperature;
    private Vector3 initialPosition;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentTemperature = initialTemperature;
        initialPosition = transform.localPosition; // Guardar la posición inicial relativa
    }

    void Update()
    {
        if (isVibrating)
        {
            Vibrate();
        }
        // Eliminar la llamada a AdjustDistanceBasedOnTemperature() ya que no se aplicará una distancia máxima.
    }

    void Vibrate()
    {
        // Limitar el movimiento de la vibración a un rango razonable
        Vector3 vibration = Random.insideUnitSphere * vibrationIntensity * Time.deltaTime;
        
        // Aplicar un factor de amortiguación para evitar movimientos excesivos
        rb.velocity *= 0.9f; 

        rb.MovePosition(rb.position + vibration);
    }

    void OnCollisionEnter(Collision collision)
    {
        MoleculeBehavior otherMolecule = collision.gameObject.GetComponent<MoleculeBehavior>();
        
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
            Mathf.Lerp(0f, maxVibrationIntensity, Mathf.InverseLerp(20f, 114f, currentTemperature)), 
            0f, 
            maxVibrationIntensity
        );
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Boundary"))
        {
            // Asegurarse de que el Rigidbody no sea nulo antes de intentar acceder a él
            if (rb != null)
            {
                // Invertir la dirección del movimiento cuando toca los bordes
                rb.velocity = -rb.velocity;
            }
        }
    }

    public void IncreaseTemperature(float amount)
    {
        currentTemperature += amount;
        StartVibration();
    }
}
