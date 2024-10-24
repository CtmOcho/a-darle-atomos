using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeyGasesOllaProgress : MonoBehaviour
{
    private bool labCompleted = false;
    private Login  login_script; // Referencia al otro script que contiene OnPutStudentProgress
    public NewBehaviourScript controllerButtonsPlus;
    public NewBehaviourScript controllerButtonsMinus; 
    bool controllerPlus; 
    bool controllerMinus;
    //AGREGAR LOS BOTONES PARA PODER HACER LA LOGICA CULIA DE PROGRESO.
    // Start is called before the first frame update
    void Start()
    {
        login_script = FindObjectOfType<Login>(); // Cambia 'Login' al nombre de tu script
        
    }

    // Update is called once per frame
    void Update()
    {

        if(!labCompleted){
            controllerPlus = controllerButtonsPlus.buttonPressedPlus; // Obtenemos el valor actual
            controllerMinus = controllerButtonsMinus.buttonPressedMinus; // Obtenemos el valor actual

            if(controllerPlus && controllerMinus){
                //Debug.Log("LISTO EL LAB DE MIERDA");
                login_script.OnPutStudentProgress(16);
                labCompleted = true;
            }
        }
        
    }
}
