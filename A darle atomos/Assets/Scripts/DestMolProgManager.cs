using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DestMolProgManager : MonoBehaviour
{   public Slider slider; // Referencia al Slider
    private float limitValue = 99f; // Valor límite para comparación


    // Referencia al script donde está el método que deseas llamar
    private Login login_script;
    private bool alreadyDone = false;
    void Start()
    {
        //slider = GameObject.Find("Slider").GetComponent<Slider>();  
        login_script = FindObjectOfType<Login>(); // Cambia 'Login' al nombre de tu script
    }
    // Update se llama una vez por frame
    void Update()
    {
        if(!alreadyDone){
            // Comprueba si el valor del slider supera el valor límite
            if (slider.value >= limitValue)
            {
                // Llama al método en el otro script
                login_script.OnPutStudentProgress(12);
                alreadyDone = true;
            }
        }
    }
}

