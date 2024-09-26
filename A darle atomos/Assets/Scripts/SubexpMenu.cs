using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SubexpMenuTrigger : MonoBehaviour
{
    public GameObject Subexperience;
    public GameObject Manos; // Referencia al objeto "manos"
    public float holdTime = 2.0f; // Tiempo en segundos para activar el menú
    private float timer = 0.0f;
    private bool isTouching = false;
    private bool menuActive = false; // Nueva variable para controlar si el menú está activo
    private bool canActivateMenu = true; // Control para evitar reactivar el menú inmediatamente

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8 && canActivateMenu && !menuActive) // Verifica si las manos están en la capa 8 y si se puede activar el menú
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

            // Restablecer la posibilidad de activar el menú cuando las manos salen del trigger
            if (menuActive)
            {
                menuActive = false; // Permitir que el trigger se pueda volver a activar
                canActivateMenu = true; // Ahora que las manos salieron, se puede volver a activar el menú
                Debug.Log("Manos salieron del trigger, menú puede reactivarse.");
            }
        }
    }

    private IEnumerator HoldToActivateMenu()
    {
        while (isTouching)
        {
            timer += Time.deltaTime;

            if (timer >= holdTime)
            {
                ActivateSubexperienceMenu();
                yield break; // Termina la corrutina después de activar el menú
            }

            yield return null;
        }
    }

    private void ActivateSubexperienceMenu()
    {
        if (Subexperience != null)
        {
            Subexperience.SetActive(true);
            Time.timeScale = 0f; // Pausar el juego
            ToggleHands(false); // Desactivar las manos cuando el menú está activo
            menuActive = true; // El menú ahora está activo
            canActivateMenu = false; // Evitar que el menú se vuelva a activar inmediatamente
            Debug.Log("Subexperience menu activated, hands deactivated and game paused.");
        }
        else
        {
            Debug.LogWarning("No se ha asignado el objeto de la Subexperience.");
        }
    }

    public void ResumeGame()
    {
        if (Subexperience != null)
        {
            Subexperience.SetActive(false);
            Time.timeScale = 1f; // Reanudar el juego
            ToggleHands(true); // Reactivar las manos al reanudar el juego
            Debug.Log("Game resumed and hands reactivated.");

            // Reiniciar el estado del script para permitir nuevas activaciones del menú de pausa
            ResetState();
        }
        else
        {
            Debug.LogWarning("No se ha asignado el objeto de la Subexperience.");
        }
    }

    public void LoadScene(string sceneName)
    {
        // Guardar el nombre de la escena actual en PlayerPrefs antes de cambiar
        PlayerPrefs.SetString("LastScene", SceneManager.GetActiveScene().name);
        PlayerPrefs.Save();

        // Reanudar el tiempo antes de cambiar de escena      
        Time.timeScale = 1f;
        Debug.Log("Loading scene: " + sceneName);

        // Cargar la nueva escena de manera asíncrona
        StartCoroutine(LoadSceneAsync(sceneName));
    }

    private IEnumerator LoadSceneAsync(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        // Espera hasta que la escena esté completamente cargada
        while (!asyncLoad.isDone)
        {
            yield return null;
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

    private void ToggleHands(bool isActive)
    {
        if (Manos != null)
        {
            Manos.SetActive(isActive);
            Debug.Log("Hands set to " + (isActive ? "active" : "inactive"));
        }
        else
        {
            Debug.LogWarning("No se ha asignado el objeto 'manos'.");
        }
    }
}
