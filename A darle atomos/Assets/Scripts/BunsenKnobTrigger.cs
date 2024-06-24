using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunsenKnobTrigger : MonoBehaviour
{
    public flame flamescript;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 8) flamescript.KnobState(true);

    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.layer == 8) flamescript.KnobState(false);
    }
}
