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

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentTemperature = initialTemperature;
    }

    void Update()
    {
        if (isVibrating)
        {
            Vibrate();
            AttractNearbyMolecules();
            //PropagateVibrations();
        }
/*
        // Propagar vibraciones antes de los 60º
        if (currentTemperature < 30f)
        {
            AttractNearbyMolecules();
            PropagateVibrations();
        }
    */
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

    void OnCollisionEnter(Collider other)
{
    if (other.CompareTag("Boundary"))
    {
        if (rb != null)
        {
            // Aplicar una fuerza hacia el centro del cubo
            Vector3 directionToCenter = (Vector3.zero - transform.position).normalized;
            rb.AddForce(directionToCenter * 500f); // Ajusta la magnitud de la fuerza según sea necesario
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
        // Verificar si el collider pertenece a una molécula individual dentro de Yodo_Pair
        MoleculeBehavior otherMolecule = collider.GetComponentInParent<MoleculeBehavior>();

        if (otherMolecule != null && otherMolecule != this)
        {
            // Atrae la molécula hacia la posición de la otra molécula cercana
            Vector3 directionToOther = (otherMolecule.transform.position - transform.position).normalized;
            rb.velocity += directionToOther * vibrationSpeed * 0.1f * Time.deltaTime;
        }
    }
}

/*
    public void PropagateVibrations()
{
    // Encuentra todas las moléculas cercanas
    Collider[] nearbyMolecules = Physics.OverlapSphere(transform.position, 0.45f); // Radio ajustable

    foreach (Collider collider in nearbyMolecules)
    {
        MoleculeBehavior otherMolecule = collider.GetComponentInParent<MoleculeBehavior>();

        if (otherMolecule != null && otherMolecule != this && otherMolecule.currentTemperature < currentTemperature)
        {
            // Igualar la temperatura de la otra molécula a la temperatura de esta molécula
            otherMolecule.currentTemperature = currentTemperature;
            otherMolecule.StartVibration(); // Iniciar la vibración en la molécula afectada
        }
    }
}
*/
}
