using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ThermometerController : MonoBehaviour
{
    private TextMeshPro thermometerLCD;
    public float temperature;
    public GameObject glass;
    private Glass glassScript;
    public bool isDestilationLab = false;

    void Start()
    {
        thermometerLCD = GetComponentInChildren<TextMeshPro>();
        glassScript = glass.GetComponent<Glass>();
        temperature = 20;
    }

    // Update is called once per frame
    void Update()
    {
        if(isDestilationLab){
            temperature = glassScript.temperature*1.05f;
        }else{
            temperature = glassScript.temperature;
        }
        thermometerLCD.text = temperature.ToString();
    }
}
