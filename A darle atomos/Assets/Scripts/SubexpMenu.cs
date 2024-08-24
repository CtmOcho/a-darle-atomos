using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
            Debug.Log("Subexperience menu activated and game paused.");
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
            Debug.Log("Game resumed.");

            // Reiniciar el estado del script para permitir nuevas activaciones del menú de pausa
            ResetState();

            // Reposicionar las manos a una posición segura
            RepositionHands();
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
