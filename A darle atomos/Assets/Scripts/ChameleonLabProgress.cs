using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



public class ChameleonLabProgress : MonoBehaviour
{
    private bool labCompleted = false;
    private Login  login_script; // Referencia al otro script que contiene OnPutStudentProgress
    public DropCollisionController dropCollisionScript; 
    public Camaleon camaleonScript;
    public ChangeColor changeColorScript;
    public TMP_Text explanationText;
    public Renderer liquidRenderer;

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
            if(dropCollisionScript.hasPermang && dropCollisionScript.hasHidrox ){
                camaleonScript.StartCamaleonRutina();
                //login_script.OnPutStudentProgress(31);
                labCompleted = true;
            }
        }
        if(changeColorScript.targetColor == camaleonScript.colors[0]){
            explanationText.text = "MnO4";
        }
        if(changeColorScript.targetColor == camaleonScript.colors[1]){
            explanationText.text = "KMnO4";
        }
        if(changeColorScript.targetColor == camaleonScript.colors[2]){
            explanationText.text = "K2MnO4";
        }   
        if(changeColorScript.targetColor == camaleonScript.colors[3]){
            explanationText.text = "K3MnO4";
        }
        if(changeColorScript.targetColor == camaleonScript.colors[4]){
            explanationText.text = "MnO2";
        }
    }
}
