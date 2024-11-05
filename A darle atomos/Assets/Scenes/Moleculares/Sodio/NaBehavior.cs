using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NaBehavior : MonoBehaviour
{
    public GameObject prefabNaOH; // Hacer pública esta variable
    public GameObject prefabH2; // Hacer pública esta variable
    private GameObject targetH2O;
    private bool reactionStarted = false;

    public void Initialize(GameObject h2o, GameObject naoh, GameObject h2)
    {
        targetH2O = h2o;
        prefabNaOH = naoh;
        prefabH2 = h2;
    }

    void OnTriggerEnter(Collider other)
    {
        if (!reactionStarted && other.gameObject == targetH2O)
        {
            StartReaction(targetH2O.transform.position, targetH2O);
        }
    }

    public void StartReaction(Vector3 collisionPosition, GameObject h2o)
    {
        reactionStarted = true;
        Destroy(gameObject);

        if (prefabNaOH != null)
        {
            Instantiate(prefabNaOH, collisionPosition, Quaternion.identity);
            Instantiate(prefabNaOH, collisionPosition, Quaternion.identity);
        }

        if (prefabH2 != null)
        {
            GameObject h2 = Instantiate(prefabH2, collisionPosition, Quaternion.identity);
            Rigidbody rb = h2.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.useGravity = false; // Desactivar gravedad
                rb.AddForce(Vector3.up * 10f, ForceMode.Impulse); // Aplicar impulso inicial hacia arriba
                StartCoroutine(ApplyContinuousForce(rb)); // Aplicar fuerza continua
            }
        }
        Destroy(h2o); // Destruir la molécula de H2O después de la reacción
    }

    private IEnumerator ApplyContinuousForce(Rigidbody rb)
    {
        while (rb != null)
        {
            rb.AddForce(Vector3.up * 10000f, ForceMode.Force); // Aplicar fuerza continua hacia arriba
            yield return new WaitForSeconds(0.001f); // Aplicar fuerza cada 0.1 segundos
        }
    }
}
