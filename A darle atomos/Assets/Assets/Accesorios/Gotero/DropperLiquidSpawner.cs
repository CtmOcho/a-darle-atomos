using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropperLiquidSpawner : MonoBehaviour
{
    public GameObject objectToSpawn;
    public GameObject dropperLiquid;
    public float objectQuantity; // Número total de gotas
    public bool isFull;
    public float liquidInitScale  = 1f;
    public float liquidPH;
    public bool hasPHDetector;
    public bool isPHExp; // Nueva variable para determinar si es un experimento ph
    public float dropAmmount; // Nueva variable pública para el valor del drop
    public bool isInValidZone;
    public bool isRainExp; 
    public float temp;
    public string elementData;
    
    public bool isChameleonExp;
    public Color liquidColor;
    

    public bool controllerPotasiumRainExp = false;

    // Nuevas variables
    public float maxDropperVolume = 100f; // Volumen máximo del gotero
    public float currentDropperVolume = 0f; // Volumen actual del gotero
    public int currentObjectsToSpawn = 0; // Número de objetos actuales en el gotero (cantidad de gotas)

    public int subCounter;
    public float decreaseAmount;

    void Start()
    {
        isFull = false;
        decreaseAmount = liquidInitScale / objectQuantity;
        SetLiquidScale(0);
        isInValidZone = false;
        currentDropperVolume = 0f; // Inicializamos el volumen actual en 0
        currentObjectsToSpawn = 0; // Inicializamos la cantidad de objetos en 0
        dropAmmount = decreaseAmount * maxDropperVolume;
        if (isRainExp)
        {
            subCounter = 0;
        }
        
    }

    void Update()
    {
        // Control del experimento de lluvia
        if (isRainExp)
        {
            if (Mathf.Abs(Vector3.Dot(transform.up, Vector3.down)) < 1f && currentObjectsToSpawn > 0 && subCounter > 30 && dropperLiquid.transform.localScale.z > 0.002f && isInValidZone && !controllerPotasiumRainExp)
            {
                // Reducimos la escala del líquido (vaciado)
                SetLiquidScale(dropperLiquid.transform.localScale.z - decreaseAmount);
                currentDropperVolume -= decreaseAmount * maxDropperVolume; // Reducimos el volumen actual según el vaciado
                
                // Creamos la posición donde instanciar el drop
                Vector3 pos = new Vector3(transform.position.x, transform.position.y - 0.01f, transform.position.z);
                
                // Instanciamos el drop
                GameObject drop = Instantiate(objectToSpawn, pos, Quaternion.identity);
                
                // Actualizamos la información del drop (gota)
                DropInformation dropInfo = drop.GetComponent<DropInformation>();
                if (dropInfo != null)
                {
                    dropInfo.dropAmmount = dropAmmount; 
                    dropInfo.isRainExp = isRainExp;
                    dropInfo.temp = temp;
                    dropInfo.elementData = elementData;
                }

                Destroy(drop, 0.5f);
                currentObjectsToSpawn--; // Disminuimos la cantidad de objetos en el gotero
                subCounter = 0;
                SetDropperFalse();
            }


            subCounter++;
        }

        // Control del experimento de pH
        if (isPHExp)
        {
            if (Mathf.Abs(Vector3.Dot(transform.up, Vector3.down)) < 1f && currentObjectsToSpawn > 0 && dropperLiquid.transform.localScale.z > 0.002f && isInValidZone)
            {
                // Reducimos la escala del líquido (vaciado)
                SetLiquidScale(dropperLiquid.transform.localScale.z - decreaseAmount);
                currentDropperVolume -= decreaseAmount * maxDropperVolume; // Reducimos el volumen actual según el vaciado

                // Creamos la posición donde instanciar el drop
                Vector3 pos = new Vector3(transform.position.x, transform.position.y - 0.01f, transform.position.z);

                // Instanciamos el drop
                GameObject drop = Instantiate(objectToSpawn, pos, Quaternion.identity);

                // Actualizamos la información del drop (gota)
                DropInformation dropInfo = drop.GetComponent<DropInformation>();
                if (dropInfo != null)
                {
                    dropInfo.liquidPH = liquidPH;
                    dropInfo.hasPHDetector = hasPHDetector;
                    dropInfo.isPHExp = isPHExp;
                    dropInfo.dropAmmount = dropAmmount;
                }

                Destroy(drop, 1);
                currentObjectsToSpawn--; // Disminuimos la cantidad de objetos en el gotero
                SetDropperFalse();

            }

        }

        if(isChameleonExp){
            if (Mathf.Abs(Vector3.Dot(transform.up, Vector3.down)) < 1f && currentObjectsToSpawn > 0 && dropperLiquid.transform.localScale.z > 0.002f && isInValidZone)
            {
                // Reducimos la escala del líquido (vaciado)
                SetLiquidScale(dropperLiquid.transform.localScale.z - decreaseAmount);
                currentDropperVolume -= decreaseAmount * maxDropperVolume; // Reducimos el volumen actual según el vaciado

                // Creamos la posición donde instanciar el drop
                Vector3 pos = new Vector3(transform.position.x, transform.position.y - 0.01f, transform.position.z);

                // Instanciamos el drop
                GameObject drop = Instantiate(objectToSpawn, pos, Quaternion.identity);
                Renderer dropRenderer = drop.GetComponent<Renderer>();
                if (dropRenderer != null)
                {
                    // Asignamos el color del líquido al drop
                    dropRenderer.material.SetColor("_Color", liquidColor);
                }
                // Actualizamos la información del drop (gota)
                DropInformation dropInfo = drop.GetComponent<DropInformation>();
                if (dropInfo != null)
                {
                    dropInfo.liquidColor = liquidColor;
                    dropInfo.elementData = elementData;
                    dropInfo.dropAmmount = dropAmmount;
                    dropInfo.isChameleonExp = isChameleonExp;
                }

                Destroy(drop, 1);
                currentObjectsToSpawn--; // Disminuimos la cantidad de objetos en el gotero
                SetDropperFalse();

            }

        }


        // Verificamos si el gotero está lleno
        if (currentObjectsToSpawn >= objectQuantity)
        {
            isFull = true;
        }
    }

    // Método para ajustar la escala del líquido
    public void SetLiquidScale(float scale)
    {
        dropperLiquid.transform.localScale = new Vector3(1, 1, scale);
    }

    public void SetLiquidPH(float ph)
    {
        liquidPH = ph;
    }

    // Método para llenar el gotero sin corrutinas
    public void FillDropper()
    {
        if (!isFull)
        {
            IncreaseVolumeAndObjects();
        }
    }

    // Método que aumenta el volumen y la cantidad de objetos cada vez que se llama
    private void IncreaseVolumeAndObjects()
    {
        if (currentDropperVolume < maxDropperVolume)
        {
            // Aumentamos el volumen actual y los objetos de forma directa
            currentDropperVolume += decreaseAmount * maxDropperVolume; 
            currentObjectsToSpawn++; 
            SetLiquidScale(currentDropperVolume / maxDropperVolume); // Ajustamos la escala del líquido

            // Si llegamos al volumen máximo, marcamos como lleno
            if (currentDropperVolume >= maxDropperVolume)
            {
                currentDropperVolume = maxDropperVolume;
                isFull = true; 
            }
        }
    }

    public void SetPHExp(bool value)
    {
        isPHExp = value;
    }

    public void SetDropperFalse()
    {
        isFull = false;
    }
}
