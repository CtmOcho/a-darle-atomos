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
    public float currentLiquidVolume;
    private BoxCollider boxCollider; // El BoxCollider que debe ser isTrigger
    public float initialVolume = 200f;
    public bool isRainExp;
    public float temp;
    public string elementData;

    public float initialScaleZ = 1f; // Corregimos para usar el valor Z del localScale inicial
    public float scaleController;

    private Glass glassScript;

    

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
        scaleController = transform.localScale.z;
        if (isPHExp)
        {
            actualLiquidVolume = initialVolume / initialScaleZ; // Basamos el cálculo en el Z inicial
            currentLiquidVolume = initialVolume;
        }

        if (isRainExp)
        {
            glassScript = GetComponent<Glass>();
            if (transform.localScale.z < 0)
            {
                actualLiquidVolume = transform.localScale.z + 0.01f;
                initialScaleZ = transform.localScale.z;
                currentLiquidVolume = actualLiquidVolume;
            }
            else
            {
                actualLiquidVolume = initialVolume / initialScaleZ; // Usamos el Z inicial corregido
                currentLiquidVolume = initialVolume;
            }
        }
    }

    // Este método se llama mientras el objeto permanece dentro del trigger
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject == targetObject)
        {
            DropperLiquidSpawner dropper = targetObject.GetComponent<DropperLiquidSpawner>();
            decreaseAmount = dropper.dropAmmount/actualLiquidVolume;
             
            if (currentLiquidVolume > 0)
            {
                if (!dropper.isFull)
                {
                    dropper.FillDropper();

                    if (isPHExp)
                    {
                        dropper.SetLiquidPH(liquidPH);
                        dropper.SetPHExp(isPHExp);
                        dropper.hasPHDetector = isPHDetector;
                    }

                    if (isRainExp)
                    {
                        if(elementData == "ioduropotasio"){
                            dropper.controllerPotasiumRainExp = false;
                        }
                        dropper.temp = glassScript.temperature;
                        dropper.elementData = elementData;
                        dropper.isRainExp = isRainExp;
                    }

                    // Aquí reducimos directamente la escala sin corrutinas
                    DecreaseScale();
                }
            }
        }
    }

    // Método para disminuir la escala directamente cada vez que se llama
    public void DecreaseScale()
    {
        // Actualizamos la escala del gotero en el eje Z
        Vector3 currentScale = transform.localScale;
        currentScale.z -= decreaseAmount;

        // Aseguramos que la escala no sea negativa
        if (currentScale.z < 0)
        {
            currentScale.z = 0;
        }

        // Aplicamos la nueva escala al objeto
        transform.localScale = currentScale;

        // Actualizamos el volumen de líquido
        currentLiquidVolume -= decreaseAmount*actualLiquidVolume;
    }
}
