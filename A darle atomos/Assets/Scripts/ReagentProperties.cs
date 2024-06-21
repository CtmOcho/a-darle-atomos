using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReagentProperties : MonoBehaviour
{
    public string nombreReactivo;
    public float masaMolar;
    public Color colorLlama;
    public bool esSolido = true;

    private Reagent reagent;

    void Start()
    {
        reagent = new Reagent(nombreReactivo, masaMolar, colorLlama, esSolido);
    }

    public Reagent getReagent() { return reagent; }

}
