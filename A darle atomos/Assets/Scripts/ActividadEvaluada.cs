using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActividadEvaluada : MonoBehaviour
{
    public bool[] respuestasCorrectas;
    public GameObject camera;
    // Start is called before the first frame update
    void Start()
    {
        respuestasCorrectas = new bool[3];
        for (int i=0; i<3; i++){
            respuestasCorrectas[i] = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void RespuestaCorrecta(int id){
        respuestasCorrectas[id] = true;
    }
    
    public void SendToDatabase(){
        camera.GetComponent<Login>().OnPutStudentProgress();
    }
}