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
    public bool isPHDetector; // Nueva variable pública para verificar si hay fenolftaleína
    public float actualLiquidVolume;
    private BoxCollider boxCollider; // El BoxCollider que debe ser isTrigger
    public float decreaseDuration = 1.0f; // Tiempo que tardará en disminuir la escala
    public float initialVolume = 200f;
    public bool isRainExp;
    public float temp;
    public string elementData;

    public float initialScaleZ = 1f; // Corregimos para usar el valor Z del localScale inicial



    private Glass glassScript;
    // Start se llama antes del primer frame
    void Start()
    {
        // Verificamos que el BoxCollider esté presente y sea un trigger
        boxCollider = GetComponent<BoxCollider>();

        if (boxCollider == null || !boxCollider.isTrigger)
        {
            Debug.LogError("El objeto no tiene un BoxCollider o no está configurado como Trigger.");
        }
        
        // Calculamos el volumen inicial basándonos en la escala Z del objeto
        initialScaleZ = transform.localScale.z;

        if (isPHExp)
        {
            actualLiquidVolume = initialVolume / initialScaleZ; // Basamos el cálculo en el Z inicial
        }

        if (isRainExp)
        {
            glassScript = GetComponent<Glass>();
            if (transform.localScale.z < 0)
            {
                actualLiquidVolume = transform.localScale.z + 0.01f;
                initialScaleZ = transform.localScale.z;
            }
            else
            {
                actualLiquidVolume = initialVolume / initialScaleZ; // Usamos el Z inicial corregido
            }
        }
    }


    // Este método se llama cuando algo entra en el trigger
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == targetObject)
        {
            DropperLiquidSpawner dropper = targetObject.GetComponent<DropperLiquidSpawner>();

            if (actualLiquidVolume > 0)
            {
                if (!dropper.isFull)
                {
                    dropper.isFull = true;
                    dropper.SetDropperFull();

                    // Iniciamos la corrutina para reducir la escala gradualmente
                    StartCoroutine(DecreaseScaleOverTime());

                    // Calculamos la cantidad de drop
                    dropper.dropAmmount = decreaseAmount * (initialVolume/initialScaleZ) / dropper.objectQuantity;

                    if (isPHExp)
                    {
                        dropper.SetLiquidPH(liquidPH);
                        dropper.SetPHExp(isPHExp);
                        dropper.hasPHDetector = isPHDetector;
                    }
                    
                    if (isRainExp)
                    {
                        dropper.temp = glassScript.temperature;
                        dropper.elementData = elementData;
                        dropper.isRainExp = isRainExp;
                        
                    }
                }
            }
        }
    }

    // Corrutina para reducir la escala de manera gradual
    private IEnumerator DecreaseScaleOverTime()
    {
        Vector3 initialScale = transform.localScale; // Escala inicial
        Vector3 targetScale = new Vector3(initialScale.x, initialScale.y, initialScale.z - decreaseAmount); // Nueva escala


        float elapsedTime = 0;

        // Reducimos la escala de manera gradual durante decreaseDuration segundos
        while (elapsedTime < decreaseDuration)
        {
            elapsedTime += Time.deltaTime;
            transform.localScale = Vector3.Lerp(initialScale, targetScale, elapsedTime / decreaseDuration);
            
            // Actualizamos el volumen de líquido
            actualLiquidVolume = transform.localScale.z * initialVolume / initialScaleZ;
            
            yield return null; // Esperamos al siguiente frame
        }

        // Aseguramos que la escala final sea exactamente la deseada
        transform.localScale = targetScale;
        actualLiquidVolume = transform.localScale.z * initialVolume / initialScaleZ; // Actualizamos el volumen de líquido
        transform.localScale.z = -0.01f;
    }
}
