using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NewBehaviourScript : MonoBehaviour
{
    public ParticleBehaviour particleBehaviour;
    public float holdTime = 2.0f; // Tiempo en segundos para activar el menú
    private float timer = 0.0f;
    private bool isTouching = false;
    public bool positive;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(HoldToActivateMenu());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8) // Verifica si el objeto está en la capa 8
        {
            isTouching = true;
            StartCoroutine(HoldToActivateMenu());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 8) // Verifica si el objeto está en la capa 8
        {
            isTouching = false;
            StopAllCoroutines(); // Asegura que no haya corrutinas activas
        }
    }

    private IEnumerator HoldToActivateMenu()
    {
        while (isTouching)
        {
            timer += Time.deltaTime;

            if (timer >= holdTime)
            {
                if (positive)
                {
                    particleBehaviour.value += 0.05f;
                }
                else
                {
                    particleBehaviour.value -= 0.05f;
                }
                yield return new WaitForSeconds(1);
            }

            yield return new WaitForUpdate();
        }
    }


}
