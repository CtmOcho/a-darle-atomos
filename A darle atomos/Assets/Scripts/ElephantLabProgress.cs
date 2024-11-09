using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElephantLabProgress : MonoBehaviour
{
    private bool labCompleted = false;
    private Login  login_script; // Referencia al otro script que contiene OnPutStudentProgress
    public DropCollisionController collisionControllerScript; 

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
            if(collisionControllerScript.elephantLabCompleted ){
                login_script.OnPutStudentProgress(46);
                labCompleted = true;
            }
        }
        
    }
}
