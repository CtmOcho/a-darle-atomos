using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class yodoProgressScript : MonoBehaviour
{
    public GameObject termometro;

    private Login  login_script; // Referencia al otro script que contiene OnPutStudentProgress
    private ThermometerController tempController;
    private bool alreadyDone = false;
    // Start is called before the first frame update
    
    void Start()
    {
        tempController = termometro.GetComponent<ThermometerController>();     
        login_script = FindObjectOfType<Login>(); // Cambia 'Login' al nombre de tu script

    }

    // Update is called once per frame
    void Update()
    {
        if(!alreadyDone)
        {
            if (tempController.temperature > 113f){
                login_script.OnPutStudentProgress(6);
                alreadyDone = true;
            }
        }
    }
}
