using UnityEngine;
using UnityEngine.UI;

public class TriggerImage : MonoBehaviour
{
    // Arrastra la referencia de la imagen desde el inspector
    public GameObject imageObject;

    // Rotación inicial del objeto
    private Quaternion initialRotation;

    // Velocidad de rotación
    public float rotationSpeed = 100f;

    // Bandera para saber si debe rotar
    private bool isRotating = false;

    void Start()
    {
        // Guardar la rotación inicial del objeto
        initialRotation = imageObject.transform.rotation;
    }

    void Update()
    {
        // Si está en el área de colisión, rotar el objeto en los ejes x y z
        if (isRotating)
        {
            imageObject.transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
        }
    }

    // Este método se llama cuando otro objeto entra en el área del colisionador
    private void OnTriggerEnter(Collider other)
    {   
        if (other.gameObject.layer == 8) // Verifica si el objeto está en la capa 8
        {
            Debug.Log("Manos ha entrado en el collider");
            imageObject.SetActive(true);
            isRotating = true;
        }
    }

    // Este método se llama cuando otro objeto sale del área del colisionador
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 8) // Verifica si el objeto está en la capa 8
        {
            Debug.Log("Manos ha salido del collider");
            imageObject.SetActive(false);
            isRotating = false;
            transform.rotation = initialRotation;
        }
    }
}

