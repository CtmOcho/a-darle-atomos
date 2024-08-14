using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubexpMenuTrigger : MonoBehaviour
{
    public GameObject Subexperience;
    public GameObject Manos; // Referencia al objeto "manos"
    public Vector3 safePosition = new Vector3(-1, 1, 0); // Nueva posición para las manos al reanudar el juego
    public float holdTime = 2.0f; // Tiempo en segundos para activar el menú
    private float timer = 0.0f;
    private bool isTouching = false;

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
            // Aquí no se reinicia el estado, se espera que esto se haga en el método ResumeGame
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
                ActivatePauseMenu();
                yield break; // Termina la corrutina después de activar el menú
            }

            yield return null;
        }
    }

    private void ActivatePauseMenu()
    {
        if (Subexperience != null)
        {
            Subexperience.SetActive(true);
            Time.timeScale = 0f; // Pausar el juego
            Debug.Log("Pause menu activated and game paused.");
        }
        else
        {
            Debug.LogWarning("No se ha asignado el objeto del menú de pausa.");
        }
    }

    public void ResumeGame()
    {
        if (Subexperience != null)
        {
            Subexperience.SetActive(false);
            Time.timeScale = 1f; // Reanudar el juego
            Debug.Log("Game resumed.");

            // Reiniciar el estado del script para permitir nuevas activaciones del menú de pausa
            ResetState();

            // Reposicionar las manos a una posición segura
            RepositionHands();
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
        StopAllCoroutines(); // Asegura que no haya corrutinas activas
        Debug.Log("State reset.");
    }

    private void RepositionHands()
    {
        if (Manos != null)
        {
            Manos.transform.position = safePosition;
            Debug.Log("Hands repositioned to " + safePosition);
        }
        else
        {
            Debug.LogWarning("No se ha asignado el objeto 'manos'.");
        }
    }
}
