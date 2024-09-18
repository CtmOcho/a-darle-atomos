using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StirrerSwitchTrigger : MonoBehaviour
{
    public Stirrer_controller controller;

    bool onOff = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8) // Verifica si el objeto está en la capa 8
        {
            onOff = !onOff;
            controller.isActive = onOff;
        }
    }
}
