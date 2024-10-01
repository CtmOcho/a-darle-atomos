using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropperLiquidSpawner : MonoBehaviour
{
    public GameObject objectToSpawn;
    public GameObject dropperLiquid;
    public float objectQuantity;
    public bool isFull;
    public float liquidInitScale = 1;
    public float liquidPH;
    public bool hasPhenolphtalein;
    public bool isPHExp; // Nueva variable para determinar si es un experimento camaleón
    public float dropAmmount; // Nueva variable pública para el valor del drop

    int counter;
    int subCounter;
    float decreaseAmount;
    // Start is called before the first frame update
    void Start()
    {   
        isFull = false;
        decreaseAmount = 1/objectQuantity;
        SetLiquidScale(liquidInitScale);
        counter = 0;
        subCounter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(Vector3.Dot(transform.up, Vector3.down)) < 0.3f && counter < objectQuantity && subCounter > 50 && dropperLiquid.transform.localScale.z > 0.002f)
        {
            // Reducimos la escala del líquido
            SetLiquidScale(dropperLiquid.transform.localScale.z - decreaseAmount);

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
            Destroy(drop, 2);

            // Aumentamos el contador
            counter++;
            subCounter = 0;
        }
        else if (dropperLiquid.transform.localScale.z <= 0.002f)
        {
            SetDropperFalse();
        }

        // Incrementamos el subcontador
        subCounter++;
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
            isFull = true;
        }
    }

    public void SetChameleonExp(bool value){
        isPHExp = value;
    }
    public void SetDropperFalse()
    {
        isFull = false;
    }
}
