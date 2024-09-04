using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlusVolumeButton : MonoBehaviour
{
    public ParticleBehaviour particleBehaviour;
    public float holdTime = 2.0f; // Tiempo en segundos para activar el menú
    private float timer = 0.0f;
    private bool isTouching = false;
    public bool positive;
    public bool buttonPressedPlus = false;
    public bool buttonPressedMinus = false;


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
                    particleBehaviour.AddParticle();
                    particleBehaviour.pressureOffset -= 10;
                    buttonPressedPlus = true;
                }
                else
                {
                    particleBehaviour.RemoveParticle();
                    particleBehaviour.pressureOffset += 10;
                    buttonPressedMinus = true;
                }
                yield return new WaitForSeconds(1);
            }

            yield return new WaitForUpdate();
        }
    }
}
