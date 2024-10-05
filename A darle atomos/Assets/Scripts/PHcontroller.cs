using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PHcontroller : MonoBehaviour
{
    private void OnTriggerEnter(Collider other){
        DropperLiquidSpawner gotero = other.gameObject.GetComponent<DropperLiquidSpawner>();
        if(gotero != null){
            gotero.isInValidZone = true;
        }
    }
    private void OnTriggerExit(Collider other){
        DropperLiquidSpawner gotero = other.gameObject.GetComponent<DropperLiquidSpawner>();
        if(gotero != null){
            gotero.isInValidZone = false;
        }
    }
}
