using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MolGasProgress : MonoBehaviour
{
    public Slider[] sliders;
    private bool[] sliderUsed = new bool[3];
    public bool labCompleted = false; 
    private Login  login_script; // Referencia al otro script que contiene OnPutStudentProgress

    // Start is called before the first frame update
    void Start()
    {
        login_script = FindObjectOfType<Login>(); // Cambia 'Login' al nombre de tu script
                // Asigna un listener a cada slider para detectar cuando su valor cambia
        
        for (int i = 0; i < sliders.Length; i++)
        {
        
            int index = i; // Captura el índice en una variable local para usarlo en la lambda
            sliders[i].onValueChanged.AddListener((value) => OnSliderValueChanged(index));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!labCompleted){
            if(CheckCompletition()){
                login_script.OnPutStudentProgress(17);
                labCompleted = true;
            }
        }
    }
    void OnSliderValueChanged(int index)
    {
        sliderUsed[index] = true;
    }
    bool CheckCompletition(){
        foreach (var slide in sliderUsed)
        {   
            if(!slide){
                return false;
            }   
        }
        return true;
    }
}
