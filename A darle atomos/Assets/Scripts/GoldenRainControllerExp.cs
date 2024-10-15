using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldenRainControllerExp : MonoBehaviour
{

    public DropCollisionController dropCollisionScript;
    public ParticleSystem rainFX;
    private Renderer liquidRenderer;
    public Color goldenColor;
    private Login  login_script; // Referencia al otro script que contiene OnPutStudentProgress

    public bool alreadyDone = false;
    public bool requirementsMet = false;
    public float desiredTemp;
    public bool hasCorrectTemp = false;
    public bool hasPbDisolved = false;
    public bool hasKDisolved = false;

    void Start()
    {
        login_script = FindObjectOfType<Login>(); // Cambia 'Login' al nombre de tu script
        rainFX.Stop();

    }
    // Update is called once per frame
    void Update()
    {
        if(dropCollisionScript.elementData == "nitratoplomo" && !alreadyDone){
            hasPbDisolved = true;
        }

        if(dropCollisionScript.hasPotasiumSol  && !alreadyDone){
            hasKDisolved = true;
        }

        if(dropCollisionScript.transform.localScale.y > 0 && dropCollisionScript.temp <= desiredTemp && !alreadyDone){
            hasCorrectTemp = true;
        }


        if(!requirementsMet && hasKDisolved && hasPbDisolved && dropCollisionScript.RainLabCompleted && hasCorrectTemp && !alreadyDone){
            requirementsMet = true;
        }    

        if(requirementsMet && !alreadyDone){
            //Debug.Log("Acá debiese accionarse el efecto de la lluvia.");
            liquidRenderer = dropCollisionScript.GetComponent<Renderer>();
            if(liquidRenderer != null){
                liquidRenderer.material.SetColor("_Color", goldenColor);
            }
            rainFX.Play();

            alreadyDone = true;
            login_script.OnPutStudentProgress(36);
        }
    
    }


}
