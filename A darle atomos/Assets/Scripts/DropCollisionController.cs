using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropCollisionController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Verificamos si el objeto que entra es una gota (drop) con el script DropInformation
        DropInformation dropInfo = other.gameObject.GetComponent<DropInformation>();

        if (dropInfo != null)
        {
            // Hacemos un log de la información almacenada en el drop
            Debug.Log("Drop Collided! Info: ");
            Debug.Log("pH: " + dropInfo.liquidPH);
            Debug.Log("Has Phenolphtalein: " + dropInfo.hasPhenolphtalein);
            Debug.Log("Is Chameleon Experiment: " + dropInfo.isPHExp);
            Debug.Log("Drop Ammount: " + dropInfo.dropAmmount);

            // Aumentamos la escala en el eje Z del objeto que tiene este script
            Vector3 newScale = transform.localScale;
            newScale.z += dropInfo.dropAmmount;
            transform.localScale = newScale;
        }
    }
}
