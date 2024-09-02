using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeyGasesOllaProgress : MonoBehaviour
{
    private bool labCompleted = false;
    private Login  login_script; // Referencia al otro script que contiene OnPutStudentProgress
    private NewBehaviourScript controllerButtons; 

    //AGREGAR LOS BOTONES PARA PODER HACER LA LOGICA CULIA DE PROGRESO.
    // Start is called before the first frame update
    void Start()
    {
        login_script = FindObjectOfType<Login>(); // Cambia 'Login' al nombre de tu script
        controllerButtons = FindObjectOfType<NewBehaviourScript>(); // Cambia 'Login' al nombre de tu script
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!labCompleted){
            if(controllerButtons.buttonPressedMinus && controllerButtons.buttonPressedMinus){
                login_script.OnPutStudentProgress(16);
                labCompleted = true;
            }
        }
        
    }
}
