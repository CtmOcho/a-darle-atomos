using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropperLiquidSpawner : MonoBehaviour
{
    public GameObject objectToSpawn;
    public GameObject dropperLiquid;
    public float objectQuantity;
    public bool isFull;
    public float liquidInitScale;
    public float liquidPH;
    public bool hasPhenolphtalein;
    public bool isPHExp; // Nueva variable para determinar si es un experimento camaleón
    public float dropAmmount; // Nueva variable pública para el valor del drop
    public bool isInValidZone;
    public float fillDuration = 1.0f;

    public float actualDropperVolume;

    int counter;
    //int subCounter;
    float decreaseAmount;
    // Start is called before the first frame update
    void Start()
    {   
        isFull = false;
        decreaseAmount = 1/objectQuantity;
        SetLiquidScale(liquidInitScale);
        counter = 0;
        isInValidZone = false;
        //subCounter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(Vector3.Dot(transform.up, Vector3.down)) < 0.3f && dropperLiquid.transform.localScale.z > 0.002f && isFull && isInValidZone)
        {
            // Reducimos la escala del líquido
            SetLiquidScale(dropperLiquid.transform.localScale.z - decreaseAmount);
            actualDropperVolume = dropperLiquid.transform.localScale.z * 20;
            // Creamos la posición donde instanciar el drop
            Vector3 pos = new Vector3(transform.position.x, transform.position.y - 0.01f, transform.position.z);
            
            // Instanciamos el drop
            GameObject drop = Instantiate(objectToSpawn, pos, Quaternion.identity);
            
            if(isPHExp){
                // Pasamos la información relevante al drop
                DropInformation dropInfo = drop.GetComponent<DropInformation>();
                if (dropInfo != null)
                {
                 dropInfo.liquidPH = liquidPH;
                 dropInfo.hasPhenolphtalein = hasPhenolphtalein;
                 dropInfo.isPHExp = isPHExp;
                 dropInfo.dropAmmount = dropAmmount; // Asignamos el valor de dropAmmount
                }

            }
            // Destruimos el drop después de 2 segundos
            Destroy(drop, 1);

            // Aumentamos el contador
            counter++;
            //subCounter = 0;
        }
        else if (counter >= objectQuantity)
        {
            SetDropperFalse();
        }

        // Incrementamos el subcontador
        //subCounter++;
    }

    public void SetLiquidScale(float scale)
    {
        dropperLiquid.transform.localScale = new Vector3(1, 1, scale);
    }

    public void SetLiquidPH(float ph)
    {
        liquidPH = ph;
    }

   public void SetDropperFull()
    {
        if (dropperLiquid.transform.localScale.z < 1)
        {
            // Llamamos a la corrutina para aumentar el tamaño de manera fluida
            StartCoroutine(FillLiquidGradually());
            isFull = true;
        counter = 0;

        }
    }

    // Corrutina para rellenar gradualmente el dropperLiquid
    private IEnumerator FillLiquidGradually()
    {
        
        float currentScale = dropperLiquid.transform.localScale.z;
        float targetScale = 1.0f;
        
        float elapsedTime = 0;

        while (elapsedTime < fillDuration)
        {
            elapsedTime += Time.deltaTime;
            float newScale = Mathf.Lerp(currentScale, targetScale, elapsedTime / fillDuration);
            SetLiquidScale(newScale);
            actualDropperVolume = dropperLiquid.transform.localScale.z;
            yield return null; // Esperar al siguiente frame
        }

        // Asegurarse de que la escala final sea exactamente 1
        SetLiquidScale(1.0f);
    }

    public void SetPHExp(bool value){
        isPHExp = value;
    }
    public void SetDropperFalse()
    {
        isFull = false;

    }
}
