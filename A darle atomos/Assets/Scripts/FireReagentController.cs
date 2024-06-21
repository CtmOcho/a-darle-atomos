using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireReagentController : MonoBehaviour
{
    public int colorLlama;
    [SerializeField]
    private bool estaMojado = false;

    private void Start()
    {
        colorLlama = 4;
        estaMojado = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        ReagentProperties reagentProperties = other.GetComponent<ReagentProperties>();
        if (reagentProperties == null){
            return;
        }
        Reagent reactivo = reagentProperties.getReagent();
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
