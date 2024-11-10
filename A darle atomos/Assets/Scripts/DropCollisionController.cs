using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class DropCollisionController : MonoBehaviour
{
    public float actualLiquidVolume; // Volumen actual de la solución en mililitros (ml)
    public float actualPHvalue; // pH actual de la solución
    public bool hasPHDetector; // Indica si el detector universal está presente
    private float totalMolesHPlus; // Moles totales de H+ en la solución
    public bool isPHExp;
    public ChangeColor changeColorScript;
    private SolutionPHCalculator phCalculator; // Referencia al controlador SolutionPHCalculator
    public bool isRainExp;
    public bool rainExpDustController = false;
    public float temp;
    public string elementData;
    public DustRemovalControllerRainLab dustRemoverScript;
    public bool RainLabCompleted = false;
    
    public bool PHlabCompleted = false;

    public bool isChameleonExp;
    public Color liquidColor;
    private Renderer thisRenderer;
    public bool hasHidrox = false;
    public bool hasPermang = false;
    public TMP_Text explanationText;

    public bool hasPotasiumSol;


    public bool isErlenmeyerliquid = false;

    public bool weirdTransformBool;
    public LiquidsGoteroController liquidControllerScript;
    public Glass glassScript;


    public float initialMaxVolume = 300f;


    public bool isElephantExp = false;
    public FoamReaction foamScript;
    public bool hasSoap = false;
    public bool hasPeroxide = false;
    public bool elephantLabCompleted = false;

    void Start()
    {
        // Calculamos el volumen inicial de la solución
        if(!isElephantExp){
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
                if(weirdTransformBool){
                    Vector3 newScale = new Vector3(transform.localScale.x, -1f, transform.localScale.z );
                    transform.localScale = newScale;
                }
            }
            if(isChameleonExp){
                thisRenderer = GetComponent<Renderer>(); 
                changeColorScript = GetComponent<ChangeColor>();

            }
        }else{
            foamScript = foamScript.GetComponent<FoamReaction>();
        }

    }

    void Update()
    {
        if(isRainExp){
            if(dustRemoverScript.disolvedDust && !rainExpDustController){
               if(!isErlenmeyerliquid){
                liquidControllerScript.elementData = elementData;
                liquidControllerScript.decreaseAmount = liquidControllerScript.initialScaleZ;
               }
               elementData =  dustRemoverScript.elementData;
               rainExpDustController = true;
            }
            temp = glassScript.temperature;
        }   
        if(isChameleonExp){
            if(weirdTransformBool){
                Vector3 newScale = new Vector3(transform.localScale.x, 0f, transform.localScale.z );
                transform.localScale = newScale;
                weirdTransformBool = false;
            }

        }
        if(isElephantExp){
            if(hasPeroxide && hasSoap && !elephantLabCompleted)
            {
                foamScript.foamColor = liquidColor;
                foamScript.startReaction = true;
                elephantLabCompleted = true;
            }
        }
    }

    

    private void OnTriggerEnter(Collider other)
    {
        // Verificamos si el objeto que entra es una gota (drop) con el script DropInformation
        DropInformation dropInfo = other.gameObject.GetComponent<DropInformation>();
        if (dropInfo != null)
        {
            if(!isElephantExp)
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
                            changeColorScript.ColorChange(); // Cambia el color según el pH actual, pero sin modificar el pH

                        }
                        else
                        {
                            // Calculamos el nuevo pH utilizando el SolutionPHCalculator solo si no es el detector de pH
                            actualPHvalue = phCalculator.CalculateNewPH(actualPHvalue, actualLiquidVolume, dropInfo.liquidPH, addedVolume);
                            

                            // Aseguramos que el pH esté entre 0 y 14
                            actualPHvalue = Mathf.Clamp(actualPHvalue, 0f, 14f);

                            if (hasPHDetector)
                            {
                                changeColorScript = GetComponent<ChangeColor>();
                                changeColorScript.ColorChange(); // Cambia el color según el pH actual, pero sin modificar el pH
                                PHlabCompleted = true;
                            }

                            // Mostramos el nuevo valor de pH en la consola
                            //Debug.Log("Nuevo valor de pH: " + actualPHvalue);
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
                        hasPotasiumSol = true;
                        elementData = dustRemoverScript.elementData;
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
                        RainLabCompleted = true;
                    }
                    dustRemoverScript.ReduceScale();
                }
                if(isChameleonExp){
                    elementData = dropInfo.elementData;
                    liquidColor = dropInfo.liquidColor;
                    if(elementData == "permanganatopotasio"){
                        explanationText.text = "KMnO4 + NaOH";
                        hasPermang = true;
                    }else{
                        explanationText.text = "NaOH";
                        hasHidrox = true;
                    }
                    
                }
            }else{
                if(dropInfo.isSoap){
                    hasSoap = true;
                }

                if(dropInfo.isDust){
                    hasPeroxide = true;
                    hasSoap = true;
                }
                
                if (dropInfo.isColorant){
                    liquidColor = dropInfo.liquidColor;
                }
            }
        }

    }
}
