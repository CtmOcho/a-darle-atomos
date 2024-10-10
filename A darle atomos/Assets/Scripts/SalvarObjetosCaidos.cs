using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SalvarObjetosCaidos : MonoBehaviour
{
    private Vector3 initialPosition; // Para almacenar la posición inicial del objeto
    private Quaternion initialRotation; // Para almacenar la rotación inicial del objeto
    private Vector3 initialScale; // Para almacenar la escala inicial del objeto

    public float minYValue = -10f; // Valor mínimo de la posición en Y, después de lo cual se reiniciará

    // Start se llama antes del primer frame
    void Start()
    {
        // Guardamos la posición, rotación y escala iniciales del objeto
        initialPosition = transform.position;
        initialRotation = transform.rotation;
        initialScale = transform.localScale;
    }

    // Update se llama una vez por frame
    void Update()
    {
        // Verificamos si la posición en Y cae por debajo de un cierto valor
        if (transform.position.y < minYValue)
        {
            // Reiniciamos la posición, rotación y escala del objeto a sus valores iniciales
            transform.position = initialPosition;
            transform.rotation = initialRotation;
            transform.localScale = initialScale;
        }
    }
}
