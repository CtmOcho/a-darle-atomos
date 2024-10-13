using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PHcontroller : MonoBehaviour
{
    public bool isPbZone = false;
    private void OnTriggerEnter(Collider other){
        DropperLiquidSpawner gotero = other.gameObject.GetComponent<DropperLiquidSpawner>();
        if(gotero != null){
            if(isPbZone){
                gotero.controllerPotasiumRainExp = false;
            }
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
