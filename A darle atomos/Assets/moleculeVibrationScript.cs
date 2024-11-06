using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moleculeVibrationScript : MonoBehaviour
{


    private Rigidbody[] particleRigidbodies;

    public float vibrationIntensity = 0f; // Intensidad de la vibración
    public float maxVibrationIntensity = 5f; // Máxima intensidad de la vibración
    public float vibrationSpeed = 5f; // Velocidad de la vibración
    public bool isVibrating = false; // Estado de vibración de la molécula

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
            //rb.velocity *= 0.9f;
            rb.MovePosition(rb.position + vibration);
        }
    }

    public void StartVibration()
    {
        isVibrating = true;
        vibrationIntensity = Mathf.Clamp(Mathf.Lerp(0f, maxVibrationIntensity, 0.5f), 0f, maxVibrationIntensity);
    }


}

