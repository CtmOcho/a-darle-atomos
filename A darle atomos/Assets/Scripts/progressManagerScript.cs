using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;


public class ProgressManagerScript : MonoBehaviour
{
public GameObject llamaColorLlama; // El objeto cuyo componente quieres verificar
//public string componentNameColorLlama; // El nombre del componente que contiene el valor numérico de "Target Color"
public int[] predefinedValues; // Los valores predefinidos que quieres verificar
private bool[] valuesPassed; // Array de booleanos para cada valor predefinido
private Login  login_script; // Referencia al otro script que contiene OnPutStudentProgress
private VisualEffect vfx;

void Start()
{
    vfx = llamaColorLlama.GetComponent<VisualEffect>();
    valuesPassed = new bool[predefinedValues.Length];
    login_script = FindObjectOfType<Login>(); // Cambia 'Login' al nombre de tu script
}

void Update()
{
    if (llamaColorLlama == null)
        return;

    // Obtener el componente en el objeto objetivo
    int targetComponent = vfx.GetInt("Target Color");


        // Obtener la propiedad Target Color usando reflection y convertirla a int
        //int currentValue = (int)targetComponent.GetType().GetProperty("TargetColor").GetValue(targetComponent);

        for (int i = 0; i < predefinedValues.Length; i++)
        {
            if (targetComponent == predefinedValues[i] && !valuesPassed[i])
            {
                valuesPassed[i] = true;
                //Debug.Log("DENTRO DEL IF RARO PARA EL COLOR EN EL ARRAY");

                CheckAllValuesPassed();
            }
        
    }
}

void CheckAllValuesPassed()
{
    foreach (bool passed in valuesPassed)
    {
        if (!passed)
            return; // Si algún valor no ha pasado, salir del método
    }

    // Si todos los valores han pasado
    if ( login_script != null)
    {
            Debug.Log("TODOS OK ");
            Debug.Log(valuesPassed);
            login_script.OnPutStudentProgress(1);
    }
}
}
