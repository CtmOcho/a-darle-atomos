using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuTrigger : MonoBehaviour
{
    public GameObject pauseMenu;
    public float holdTime = 2.0f; // Tiempo en segundos para activar el menú
    private float timer = 0.0f;
    private bool isTouching = false;
    private bool canActivateMenu = true; // Nueva variable para controlar si se puede activar el menú

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8 && canActivateMenu) // Verifica si el objeto está en la capa 8 y si se puede activar el menú
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


            // Permitir activar el menú solo después de que las manos hayan salido del trigger
            canActivateMenu = true;
        }
    }

    private IEnumerator HoldToActivateMenu()
    {
        while (isTouching)
        {
            // Usamos Time.unscaledDeltaTime para asegurarnos de que la corrutina funcione incluso cuando Time.timeScale es 0
            timer += Time.unscaledDeltaTime;

            if (timer >= holdTime)
            {
                ActivatePauseMenu();
                yield break; // Termina la corrutina después de activar el menú
            }

            yield return null;
        }
    }

    private void ActivatePauseMenu()
    {
        if (pauseMenu != null)
        {
            pauseMenu.SetActive(true);

            canActivateMenu = false; // Evitar que el menú se reactive inmediatamente



            Debug.Log("Pause menu activated, hands deactivated, and game paused.");
        }
        else
        {
            Debug.LogWarning("No se ha asignado el objeto del menú de pausa.");
        }
    }

    public void ResumeGame()
    {
        if (pauseMenu != null)
        {
            pauseMenu.SetActive(false);



            Debug.Log("Game resumed and hands reactivated.");

            // Reiniciar el estado del script para permitir nuevas activaciones del menú de pausa
            ResetState();
        }
        else
        {
            Debug.LogWarning("No se ha asignado el objeto del menú de pausa.");
        }
    }

    private void ResetState()
    {
        // Reinicia todas las variables relevantes
        timer = 0.0f;
        isTouching = false;

        Debug.Log("State reset.");
    }

}
