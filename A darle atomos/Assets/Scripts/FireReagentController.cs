using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireReagentController : MonoBehaviour
{
    [SerializeField]
    private Color colorLlama;
    [SerializeField]
    private bool estaMojado = false;

    private void Start()
    {
        colorLlama = Color.black;
        estaMojado = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        Reagent reactivo = other.GetComponent<ReagentProperties>().getReagent();
        if (reactivo != null)
        {
            if (!reactivo.IsSolid)
            {
                estaMojado = true;
            }
            else if (reactivo.IsSolid && estaMojado)
            {
                colorLlama = reactivo.FlameColor;
            }
        }
    }
}
