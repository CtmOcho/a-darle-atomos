using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestLabProgController : MonoBehaviour
{
    public GameObject boilingObject;
    private Login  login_script; // Referencia al otro script que contiene OnPutStudentProgress

    private BoilingBehaviour boilingScript;
    private bool alreadyDone = false;

    // Start is called before the first frame update
    void Start()
    {
        boilingScript = boilingObject.GetComponent<BoilingBehaviour>();     
        login_script = FindObjectOfType<Login>(); // Cambia 'Login' al nombre de tu script

    }

    // Update is called once per frame
    void Update()
    {
        if(!alreadyDone)
        {
            if (boilingScript.LabCompleted){
                login_script.OnPutStudentProgress(11);
                alreadyDone = true;
            }
        }
    }
}
