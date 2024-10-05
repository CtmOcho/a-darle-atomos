using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiquidsGoteroController : MonoBehaviour
{
    // Variables públicas para ajustar los valores
    public GameObject targetObject; // El objeto al que llamaremos la función
    public float decreaseAmount; // Valor para disminuir la escala en el eje Z
    public bool isPHExp; // Nueva variable pública para detectar si es un experimento camaleón
    public float liquidPH; // Nueva variable pública para el pH del líquido
    public bool isPhenolphtalein; // Nueva variable pública para verificar si hay fenolftaleína
    public float actualLiquidVolume;
    private BoxCollider boxCollider; // El BoxCollider que debe ser isTrigger
    public float decreaseDuration = 1.0f; // Tiempo que tardará en disminuir la escala

    // Start se llama antes del primer frame
    void Start()
    {
        // Verificamos que el BoxCollider esté presente y sea un trigger
        boxCollider = GetComponent<BoxCollider>();

        if (boxCollider == null || !boxCollider.isTrigger)
        {
            Debug.LogError("El objeto no tiene un BoxCollider o no está configurado como Trigger.");
        }

        // Calculamos el volumen inicial
        actualLiquidVolume = transform.localScale.z * 200;
    }

    // Este método se llama cuando algo entra en el trigger
    void OnTriggerEnter(Collider other)
    {
        // Verificamos si el objeto con el que interactuamos es el objeto objetivo
        if (other.gameObject == targetObject)
        {
            DropperLiquidSpawner dropper = targetObject.GetComponent<DropperLiquidSpawner>();

            if (!dropper.isFull)
            {
                dropper.isFull = true;

                // Llamamos a la función pública del objeto objetivo con el valor 1
                dropper.SetDropperFull();

                // Iniciamos la corrutina para reducir la escala gradualmente
                StartCoroutine(DecreaseScaleOverTime());

                // Calculamos la cantidad de drop
                dropper.dropAmmount = decreaseAmount * 200 / dropper.objectQuantity;
                dropper.actualDropperVolume = decreaseAmount * 200;

                // Verificamos si es un experimento tipo camaleón
                if (isPHExp)
                {
                    dropper.SetLiquidPH(liquidPH); // Configuramos el pH
                    dropper.SetPHExp(isPHExp);
                }

                // Actualizamos el valor de hasPhenolphtalein
                dropper.hasPhenolphtalein = isPhenolphtalein;
            }
        }
    }

    // Corrutina para reducir la escala de manera gradual
    private IEnumerator DecreaseScaleOverTime()
    {
        Vector3 initialScale = transform.localScale; // Escala inicial
        Vector3 targetScale = new Vector3(initialScale.x, initialScale.y, initialScale.z - decreaseAmount); // Nueva escala

        // Asegurarnos de que el valor no sea negativo
        if (targetScale.z < 0)
        {
            targetScale.z = 0;
        }

        float elapsedTime = 0;

        // Reducimos la escala de manera gradual durante decreaseDuration segundos
        while (elapsedTime < decreaseDuration)
        {
            elapsedTime += Time.deltaTime;
            transform.localScale = Vector3.Lerp(initialScale, targetScale, elapsedTime / decreaseDuration);
            actualLiquidVolume = transform.localScale.z * 200; // Actualizamos el volumen de líquido
            yield return null; // Esperamos al siguiente frame
        }

        // Aseguramos que la escala final sea exactamente la deseada
        transform.localScale = targetScale;
        actualLiquidVolume = transform.localScale.z * 200;
    }
}
