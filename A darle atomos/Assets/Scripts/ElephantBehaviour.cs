using UnityEngine;
using System.Collections;
using TMPro;
/*
Al agregar el KI, este actúa como catalizador de la descomposición del peróxido de hidrógeno, acelerando la reacción de forma inmediata. El peróxido de hidrógeno se descompone rápidamente en agua y oxígeno, liberando burbujas de oxígeno que el jabón atrapa para formar una gran cantidad de espuma. Esta reacción también es exotérmica, lo que genera calor y hace que la espuma esté tibia. Al finalizar, quedan agua, jabón y el KI sin consumir, junto con la espuma formada por el oxígeno atrapado.
*/

public class ElephantBehaviour : MonoBehaviour
{
    public GameObject oxygenPrefab; // Prefab para el O₂ resultante
    public GameObject waterPrefab; // Prefab para el H₂O resultante
    public TMP_Text explanationText;


    private bool reactionOccurred = false; // Bandera para verificar si la reacción ya ocurrió

    void OnTriggerEnter(Collider other)
    {
        // Verificar si la reacción ya ocurrió
        if (reactionOccurred) return;

        explanationText.text = "Al agregar el KI, este actúa como catalizador para la descomposición del peróxido de hidrógeno, acelerando la reacción. El peróxido de hidrógeno se descompone rápidamente en agua y oxígeno, liberando burbujas de oxígeno que el jabón atrapa para formar una gran cantidad de espuma. Esta reacción es exotérmica, por lo que genera calor y hace que la espuma esté tibia.";

        // Verificar si el objeto colisionado es el KI y este objeto es H₂O₂
        if (other.gameObject.CompareTag("KI") && gameObject.CompareTag("H2O2"))
        {
            // Desactivar la gravedad de KI
            Rigidbody kiRb = other.gameObject.GetComponent<Rigidbody>();
            if (kiRb != null)
            {
                kiRb.useGravity = false;
                kiRb.velocity = Vector3.zero;
            }

            // Marcar que la reacción ha ocurrido
            reactionOccurred = true;

            // Llamar a la función para reorganizar moléculas de H₂O₂
            TriggerDecomposition(other.gameObject, this.gameObject);
        }
    }

    IEnumerator AttractNearbyMolecules(Transform o2_1, Transform o2_2, float attractionSpeed)
    {
        Rigidbody o2Rb1 = o2_1.GetComponent<Rigidbody>();
        Rigidbody o2Rb2 = o2_2.GetComponent<Rigidbody>();

        while (Vector3.Distance(o2_1.position, o2_2.position) > 1f)
        {
            // Dirección de atracción
            Vector3 directionToOther = (o2_2.position - o2_1.position).normalized;

            // Aplicar fuerza de atracción a ambas esferas
            o2Rb1.velocity += directionToOther * attractionSpeed * Time.deltaTime;
            o2Rb2.velocity -= directionToOther * attractionSpeed * Time.deltaTime;

            yield return null; // Esperar un cuadro antes de continuar
        }
         // Detener la atracción cuando las moléculas están lo suficientemente cerca
        o2Rb1.velocity = Vector3.zero;
        o2Rb2.velocity = Vector3.zero;
        o2Rb1.velocity = new Vector3(0, 12.0f, 0);
        o2Rb2.velocity = new Vector3(0, 12.0f, 0);
    }

    void TriggerDecomposition(GameObject kiObject, GameObject h2o2Object)
    {
        // Buscar los átomos dentro del H₂O₂ (solo en el objeto de colisión)
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
            // Añadir un Rigidbody a las moléculas de oxígeno para simular su ascenso
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

            // Iniciar la corrutina de atracción
            StartCoroutine(AttractNearbyMolecules(o2_m1, o2_m2, 3f));

            // Establecer la velocidad en el eje Y para simular la subida de O₂
            o2Rb1.velocity = new Vector3(0, 12.0f, 0);
            o2Rb2.velocity = new Vector3(0, 12.0f, 0);
        }
        else
        {
            Debug.LogWarning("No se encontraron todas las moléculas necesarias para la reacción en los objetos colisionados.");
        }
    }
}
