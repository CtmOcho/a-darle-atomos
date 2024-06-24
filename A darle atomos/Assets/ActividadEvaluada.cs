using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActividadEvaluad : MonoBehaviour
{
    public bool[] respuestas;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void PreguntaCorrecta(int id){
        respuestas[id] = true;
    }
    
    void PostResultToDB(int[] respuestas){
        //TODO: subir resultados a la base de datos
        return;
    }
}
