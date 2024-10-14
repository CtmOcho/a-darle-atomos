using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SodiumLabProgressController : MonoBehaviour
{
    private bool labCompleted = false;
    private Login  login_script; // Referencia al otro script que contiene OnPutStudentProgress
    public bool sodiumComsumed = false;
    public BlinkImage imageToBlink;


    //AGREGAR LOS BOTONES PARA PODER HACER LA LOGICA CULIA DE PROGRESO.
    // Start is called before the first frame update
    void Start()
    {
        login_script = FindObjectOfType<Login>(); // Cambia 'Login' al nombre de tu script
    }

    public void blinkingIntermediaryStart(){
        imageToBlink.StartBlinkingImage();
    }
    public void blinkingIntermediaryEnd(){
        imageToBlink.shouldBlink = false;  // Asegura que el parpadeo se detiene
        imageToBlink.StopBlinkingImage();  // Método para detener el parpadeo
        imageToBlink.DeactivateImage();
    }


        // Update is called once per frame
    void Update()
    {
        if(!labCompleted && sodiumComsumed){
            login_script.OnPutStudentProgress(26);
            labCompleted = true;
        }
        
    }
}
