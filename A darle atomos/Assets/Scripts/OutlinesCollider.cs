using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlinesCollider : MonoBehaviour
{
    
    // Update is called once per frame
    void Update()
    {
        void OnCollisionStay(Collision collision){
            // Aquí puedes agregar una condición para detectar si el objeto específico está tocando otro
            if (collision.gameObject.CompareTag("Selectable")) // Usa el tag que quieras identificar
            {
                Debug.Log("Los objetos están tocándose.");
            }
        }
    }
}
