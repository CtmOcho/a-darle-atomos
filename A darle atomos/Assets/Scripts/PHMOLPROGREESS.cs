using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PHMOLPROGREESS : MonoBehaviour
{
    // Start is called before the first frame update
   private bool labCompleted = false;
    private Login  login_script; // Referencia al otro script que contiene OnPutStudentProgress
    public pHArranger arrangerScript;

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
            if(arrangerScript.labCompleted){
                login_script.OnPutStudentProgress(42);
                labCompleted = true;
            }
        }
        
    }
}
