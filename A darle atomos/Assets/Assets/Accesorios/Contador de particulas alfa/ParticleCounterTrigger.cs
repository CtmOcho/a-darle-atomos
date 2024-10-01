using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCounterTrigger : MonoBehaviour
{
    public ParticleCounterController controller;
    public int type;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8) // Verifica si el objeto está en la capa 8
        {
            if (type == 0)
            {
                controller.RotateMinus();
            }
            else if (type == 1)
            {
                controller.RotatePlus();
            }
            else
            {
                controller.OnOff();
            }
        }
    }
}
