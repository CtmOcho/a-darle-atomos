using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour
{
    public GameObject LoadingScreen;
    public Image LoadingBarFill;
    public GameObject[] coursePanels; // Referencia a los paneles de cursos
    public GameObject navbar; // Referencia a la barra de navegación
    public GameObject basePanel; // Referencia al panel base del Canvas

    public float delayAfterLoad = 1f; // Tiempo de espera adicional después de que la barra se llena
    public float fillSpeed = 0.5f; // Velocidad de llenado

    // Método para iniciar la carga de una escena
    public void LoadScene(string sceneName)
    {
        // Reiniciar la barra de progreso al 0 cada vez que se carga una nueva escena
        LoadingBarFill.fillAmount = 0f;

        // Ocultar los paneles de cursos, la barra de navegación y el panel base antes de cargar la escena
        foreach (var panel in coursePanels)
        {
            panel.SetActive(false);
        }
        navbar.SetActive(false);
        basePanel.SetActive(false);

        // Iniciar la pantalla de carga
        StartCoroutine(LoadSceneAsync(sceneName));
    }

    // Corrutina que maneja la carga asíncrona de la escena
    IEnumerator LoadSceneAsync(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        LoadingScreen.SetActive(true);
        operation.allowSceneActivation = false; // Evitar que la escena se active inmediatamente

        while (!operation.isDone)
        {
            // Llenar la barra de manera gradual
            LoadingBarFill.fillAmount = Mathf.MoveTowards(LoadingBarFill.fillAmount, 1f, fillSpeed * Time.deltaTime);

            // Solo permitir la activación de la escena cuando la barra esté llena
            if (LoadingBarFill.fillAmount >= 1f && operation.progress >= 0.9f)
            {
                // Esperar un poco antes de activar la escena
                yield return new WaitForSeconds(delayAfterLoad);

                // Activar la escena
                operation.allowSceneActivation = true;
            }

            yield return null;
        }

        // Reiniciar la barra de progreso y ocultar la pantalla de carga cuando se complete
        LoadingScreen.SetActive(false);
    }
}
