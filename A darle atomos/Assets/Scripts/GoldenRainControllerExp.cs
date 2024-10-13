using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldenRainControllerExp : MonoBehaviour
{

    public DropCollisionController dropCollisionScript;
    public DropperLiquidSpawner dropInformationScript;
    
    public bool alreadyDone = false;
    public bool requirementsMet = false;
    public float desiredTemp;
    public bool hasCorrectTemp = false;
    public bool hasPbDisolved = false;
    public bool hasKDisolved = false;


    // Update is called once per frame
    void Update()
    {
        if(dropCollisionScript.elementData == "nitratoplomo" && !alreadyDone){
            hasPbDisolved = true;
        }

        if(dropInformationScript.elementData == "ioduropotasio" && !alreadyDone){
            hasKDisolved = true;
        }

        if(dropCollisionScript.temp >= desiredTemp && !alreadyDone){
            hasCorrectTemp = true;
        }


        if(!requirementsMet && hasKDisolved && hasPbDisolved && hasCorrectTemp && !alreadyDone){
            requirementsMet = true;
        }    

        if(requirementsMet && !alreadyDone){
            Debug.Log("Acá debiese accionarse el efecto de la lluvia.");
            alreadyDone = true;
        }
    
    }


}
