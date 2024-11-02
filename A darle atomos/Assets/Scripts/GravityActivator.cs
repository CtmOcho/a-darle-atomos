using UnityEngine;

public class GravityActivator : MonoBehaviour
{
    private Rigidbody rb;

    void Start()
    {
        // Obtén el componente Rigidbody del GameObject
        rb = GetComponent<Rigidbody>();

        // Verifica si el Rigidbody existe
        if (rb == null)
        {
            Debug.LogWarning("No se encontró un Rigidbody en este GameObject.");
        }
    }

    // Método para activar la gravedad
    public void ActivateGravity()
    {
        if (rb != null)
        {
            rb.useGravity = true;
            Debug.Log("Gravedad activada en el Rigidbody.");
        }
        else
        {
            Debug.LogWarning("No se puede activar la gravedad porque el Rigidbody no está asignado.");
        }
    }
}