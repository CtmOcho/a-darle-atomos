using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropCollisionController : MonoBehaviour
{
    public float actualLiquidVolume; // Volumen actual de la solución en mililitros (ml)
    public float actualPHvalue; // pH actual de la solución
    public bool hasPHDetector; // Indica si el detector universal está presente
    private float totalMolesHPlus; // Moles totales de H+ en la solución
    public bool isPHExp;
    private ChangeColor changeColorScript;
    private SolutionPHCalculator phCalculator; // Referencia al controlador SolutionPHCalculator
    public bool isRainExp;
    public bool rainExpDustController = false;
    public float temp;
    public string elementData;
    public DustRemovalControllerRainLab dustRemoverScript;
    
    public bool isChameleonExp;
    public Color liquidColor;
    private Renderer thisRenderer;
    private bool hasHidrox = false;
    private bool hasPermang = false;
   


    public bool isErlenmeyerliquid = false;

    public bool weirdTransformBool = false;
    public LiquidsGoteroController liquidControllerScript;
    public Glass glassScript;

    public float initialMaxVolume = 300f;

    void Start()
    {
        // Calculamos el volumen inicial de la solución

        if(isPHExp){
            actualLiquidVolume = transform.localScale.y * initialMaxVolume; // Asumiendo que el factor de escala a volumen es 300 ml
            // Calculamos los moles iniciales de H+ en la solución
            float initialHPlusConcentration = Mathf.Pow(10, -actualPHvalue); // Concentración inicial de H+ en mol/L
            totalMolesHPlus = initialHPlusConcentration * (actualLiquidVolume / 1000f); // Convertimos ml a L
            
            // Obtenemos la referencia del script ChangeColor y SolutionPHCalculator
            changeColorScript = GetComponent<ChangeColor>();
            phCalculator = GetComponent<SolutionPHCalculator>(); // Busca el controlador en el mismo GameObject
        }
        if(isRainExp){
            if(!isErlenmeyerliquid){
                liquidControllerScript = GetComponent<LiquidsGoteroController>();
            }
            glassScript = GetComponent<Glass>();
        }
        if(isChameleonExp){
            thisRenderer = GetComponent<Renderer>(); 
            changeColorScript = GetComponent<ChangeColor>();

        }
    }

    void Update()
    {
        if (isPHExp)
        {
            if (hasPHDetector)
            {
                changeColorScript = GetComponent<ChangeColor>();
                changeColorScript.ColorChange(); // Cambia el color según el pH actual, pero sin modificar el pH
            }
        }
        if(isRainExp){
            if(dustRemoverScript.disolvedDust && !rainExpDustController){
               if(!isErlenmeyerliquid){
                liquidControllerScript.elementData = elementData;
                liquidControllerScript.decreaseAmount = liquidControllerScript.initialScaleZ;
               }
               elementData =  dustRemoverScript.elementData;
               rainExpDustController = true;
            }
            if(weirdTransformBool){
                Vector3 newScale = new Vector3(transform.localScale.x, -1f, transform.localScale.z );
                transform.localScale = newScale;
                weirdTransformBool = false;
            }
        }   
        if(isChameleonExp){
            if(weirdTransformBool){
                Vector3 newScale = new Vector3(transform.localScale.x, 0f, transform.localScale.z );
                transform.localScale = newScale;
                weirdTransformBool = false;
            }
            if(hasHidrox && hasPermang){
                
                changeColorScript.ColorChange();
            }
        }
    }

    

    private void OnTriggerEnter(Collider other)
    {
        // Verificamos si el objeto que entra es una gota (drop) con el script DropInformation
        DropInformation dropInfo = other.gameObject.GetComponent<DropInformation>();
        if (dropInfo != null)
        {
            // Aumentamos la escala en el eje Z del objeto que tiene este script
            Vector3 newScale = transform.localScale;
            float addedVolume = dropInfo.dropAmmount; // Volumen de la gota en ml
            actualLiquidVolume += addedVolume;
            
            if(isPHExp){
                newScale.y += (addedVolume / initialMaxVolume); // Actualizamos la escala basada en el volumen agregado
            }

            if(isRainExp){
                if(!isErlenmeyerliquid){
                    if(transform.localScale.z < 0){
                        newScale.z = 0;
                        actualLiquidVolume = 0f;
                    }
                    newScale.z += (addedVolume / initialMaxVolume);
                }else{
                    if(transform.localScale.y < 0){
                        newScale.y = 0;
                        actualLiquidVolume = 0f;
                    }
                    newScale.y += (addedVolume / initialMaxVolume);
                }
            }

            if(isChameleonExp){
                newScale.y += (addedVolume / initialMaxVolume); // Actualizamos la escala basada en el volumen agregado
            }

            transform.localScale = newScale;

            // Actualizamos el volumen total de la solución 
            if(isPHExp){
                
                if (dropInfo.hasPHDetector)
                    {
                        // Si la gota que se está añadiendo es el detector de pH, no modificamos el pH
                        hasPHDetector = true;
                        changeColorScript.boolPHDetector = true;
                    }
                    else
                    {
                        // Calculamos el nuevo pH utilizando el SolutionPHCalculator solo si no es el detector de pH
                        actualPHvalue = phCalculator.CalculateNewPH(actualPHvalue, actualLiquidVolume, dropInfo.liquidPH, addedVolume);

                        // Aseguramos que el pH esté entre 0 y 14
                        actualPHvalue = Mathf.Clamp(actualPHvalue, 0f, 14f);

                        // Mostramos el nuevo valor de pH en la consola
                        Debug.Log("Nuevo valor de pH: " + actualPHvalue);
                    }
            }
            if(isRainExp){
                
                if(dropInfo.elementData != "agua"){
                    if(!isErlenmeyerliquid){
                        liquidControllerScript.temp = temp;
                        liquidControllerScript.elementData = elementData;
                        liquidControllerScript.initialVolume = actualLiquidVolume;
                        liquidControllerScript.actualLiquidVolume = actualLiquidVolume;
                        liquidControllerScript.currentLiquidVolume = actualLiquidVolume;
                        liquidControllerScript.isRainExp = isRainExp;
                        liquidControllerScript.initialScaleZ = liquidControllerScript.transform.localScale.z;
                    }
                    temp = dropInfo.temp;
                    elementData = dropInfo.elementData;
                    glassScript.temperature = temp;
                    rainExpDustController = false;
                }else{
                    if(!isErlenmeyerliquid){
                        liquidControllerScript.temp = temp;
                        liquidControllerScript.elementData = elementData;
                        liquidControllerScript.initialVolume = actualLiquidVolume;
                        liquidControllerScript.actualLiquidVolume = actualLiquidVolume;
                        liquidControllerScript.currentLiquidVolume = actualLiquidVolume;
                        liquidControllerScript.isRainExp = isRainExp;
                        liquidControllerScript.initialScaleZ = liquidControllerScript.transform.localScale.z;
                    }
                    temp = dropInfo.temp;
                    elementData = dustRemoverScript.elementData;
                    glassScript.temperature = temp;
                    rainExpDustController = false;
                }
                dustRemoverScript.ReduceScale();
            }
            if(isChameleonExp){
                elementData = dropInfo.elementData;
                liquidColor = dropInfo.liquidColor;
                if(elementData == "permanganatopotasio"){
                    hasPermang = true;
                }else{
                    hasHidrox = true;
                }
                
            }
        }
    }
}
