using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PHcontroller : MonoBehaviour
{
    public bool isPbZone = false;
    private void OnTriggerEnter(Collider other){
        DropperLiquidSpawner gotero = other.gameObject.GetComponent<DropperLiquidSpawner>();
        DropperPolvitoSpawner cuchara = other.gameObject.GetComponent<DropperPolvitoSpawner>();
        if(gotero != null){
            if(isPbZone){
                gotero.controllerPotasiumRainExp = false;
            }
            gotero.isInValidZone = true;
        }
        if(cuchara != null){
            cuchara.isInValidZone = true;
        }
    }


    private void OnTriggerExit(Collider other){
        DropperLiquidSpawner gotero = other.gameObject.GetComponent<DropperLiquidSpawner>();
        DropperPolvitoSpawner cuchara = other.gameObject.GetComponent<DropperPolvitoSpawner>();
        if(gotero != null){
            gotero.isInValidZone = false;
        }
        if(cuchara != null){
            cuchara.isInValidZone = false;
        }
    }
}
