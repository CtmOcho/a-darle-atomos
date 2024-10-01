using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiquidsGoteroController : MonoBehaviour
{
    // Variables públicas para ajustar los valores
    public GameObject targetObject; // El objeto al que llamaremos la función
    public float decreaseAmount = 0.1f; // Valor para disminuir la escala en el eje Z
    public bool isPHExp; // Nueva variable pública para detectar si es un experimento camaleón
    public float liquidPH; // Nueva variable pública para el pH del líquido
    public bool isPhenolphtalein; // Nueva variable pública para verificar si hay fenolftaleína

    private BoxCollider boxCollider; // El BoxCollider que debe ser isTrigger

    // Start se llama antes del primer frame
    void Start()
    {
        // Verificamos que el BoxCollider esté presente y sea un trigger
        boxCollider = GetComponent<BoxCollider>();
        if (boxCollider == null || !boxCollider.isTrigger)
        {
            Debug.LogError("El objeto no tiene un BoxCollider o no está configurado como Trigger.");
        }
    }

    // Este método se llama cuando algo entra en el trigger
    void OnTriggerEnter(Collider other)
    {
        // Verificamos si el objeto con el que interactuamos es el objeto objetivo
        if (other.gameObject == targetObject)
        {
            DropperLiquidSpawner dropper = targetObject.GetComponent<DropperLiquidSpawner>();

            if(!dropper.isFull)
            {
                dropper.isFull = true;

                // Llamamos a la función pública del objeto objetivo con el valor 1
                dropper.SetLiquidScale(1.0f);

                // Reducimos la escala del objeto en el eje Z según el valor público
                Vector3 newScale = transform.localScale;
                newScale.z -= decreaseAmount;
                if (newScale.z < 0) newScale.z = 0; // Para evitar que la escala sea negativa
                transform.localScale = newScale;
                dropper.dropAmmount = decreaseAmount / dropper.objectQuantity;

                // Verificamos si es un experimento tipo camaleón
                if (isPHExp)
                {
                    dropper.SetLiquidPH(liquidPH); // Configuramos el pH
                    dropper.SetChameleonExp(isPHExp);
                }

                // Actualizamos el valor de hasPhenolphtalein
                dropper.hasPhenolphtalein = isPhenolphtalein;
            }
        }
    }
}
