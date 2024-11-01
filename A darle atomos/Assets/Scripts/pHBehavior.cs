using UnityEngine;

public class pHBehavior : MonoBehaviour
{
    public float currentpH = 7f; // pH inicial de la mol�cula (neutro)
    private Rigidbody[] particleRigidbodies;
    private Vector3[] initialLocalPositions; // Posiciones locales iniciales de las part�culas

    void Start()
    {
        particleRigidbodies = GetComponentsInChildren<Rigidbody>();

        // Guardar las posiciones locales iniciales
        initialLocalPositions = new Vector3[particleRigidbodies.Length];
        for (int i = 0; i < particleRigidbodies.Length; i++)
        {
            initialLocalPositions[i] = particleRigidbodies[i].transform.localPosition;
        }
    }

    void Update()
    {
        MaintainStructure(); // Intentar mantener la estructura unida
    }

    void MaintainStructure()
    {
        for (int i = 0; i < particleRigidbodies.Length; i++)
        {
            Rigidbody rb = particleRigidbodies[i];
            Vector3 targetPosition = transform.TransformPoint(initialLocalPositions[i]);
            Vector3 forceDirection = (targetPosition - rb.position).normalized;

            // Aplicar una fuerza para atraer la part�cula a su posici�n inicial relativa
            rb.AddForce(forceDirection * Time.deltaTime);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        pHBehavior otherMolecule = collision.gameObject.GetComponent<pHBehavior>();

        if (otherMolecule != null)
        {
            // Ajustar el pH si la otra mol�cula tiene un pH m�s bajo (�cido) o m�s alto (b�sico)
            if (otherMolecule.currentpH != currentpH)
            {
                AdjustpH((otherMolecule.currentpH + currentpH) / 2); // Promediar el pH entre las mol�culas
            }
        }
    }

    public void AdjustpH(float newpH)
    {
        currentpH = newpH;

    }

    void OnCollisionEnter(Collider other)
    {
        if (other.CompareTag("Boundary"))
        {
            foreach (Rigidbody rb in particleRigidbodies)
            {
                if (rb != null)
                {
                    // Aplicar una fuerza hacia el centro del contenedor para mantener las mol�culas dentro
                    Vector3 directionToCenter = (Vector3.zero - rb.position).normalized;
                    rb.AddForce(directionToCenter * 2000f); // Ajusta la magnitud de la fuerza seg�n sea necesario
                }
            }
        }
    }

    public void AttractNearbyMolecules()
    {
        // Encuentra todas las mol�culas cercanas y atrae esta mol�cula hacia ellas
        Collider[] nearbyMolecules = Physics.OverlapSphere(transform.position, 0.45f); // Radio ajustable

        foreach (Collider collider in nearbyMolecules)
        {
            pHBehavior otherMolecule = collider.GetComponentInParent<pHBehavior>();

            if (otherMolecule != null && otherMolecule != this)
            {
                // Atrae la mol�cula hacia la posici�n de la otra mol�cula cercana
                Vector3 directionToOther = (otherMolecule.transform.position - transform.position).normalized;
                foreach (Rigidbody rb in particleRigidbodies)
                {
                    rb.velocity += directionToOther * 0.3f * Time.deltaTime;
                }
            }
        }
    }
}
