using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Asegúrate de que usas el namespace de UI si estás trabajando con botones

public class BohrProgressController : MonoBehaviour
{
    public Button excitar;
    public Button liberar;
    private bool[] conditionsMet = new bool[3]; // Array para monitorear las condiciones
    private Login login; // Referencia al otro script que contiene la función que quieres llamar
    private bool lastPressedBtnLib = false;
    void Start()
    {
        // Inicializa el array de condiciones
        conditionsMet[0] = false;
        conditionsMet[1] = false;
        conditionsMet[2] = false;

        // Asigna los métodos a los eventos de los botones
        excitar.onClick.AddListener(OnExcitarClick);
        liberar.onClick.AddListener(OnLiberarClick);

        // Encuentra el script donde está la función que quieres llamar
        login = FindObjectOfType<Login>();
    }

    void OnExcitarClick()
    {
        conditionsMet[0] = true; 
        lastPressedBtnLib = false;
        CheckConditions();
    }

    void OnLiberarClick()
    {
        if (lastPressedBtnLib)
        {
            conditionsMet[2] = true;  
        }else{
            conditionsMet[1] = true;
        }
        lastPressedBtnLib = true;
        CheckConditions();
    }

    void CheckConditions()
    {
        // Verifica si todas las condiciones se han cumplido
        if (conditionsMet[0] && conditionsMet[1] && conditionsMet[2])
        {
            if (login != null)
            {
                login.OnPutStudentProgress(2); // Llama al método en el otro script
            }
        }
    }
}
