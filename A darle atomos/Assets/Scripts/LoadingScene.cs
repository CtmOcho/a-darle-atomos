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
    public CanvasGroup canvasGroup; // Referencia al CanvasGroup para el fade out
    public float delayAfterLoad = 1f; // Tiempo de espera adicional después de que la barra se llena
    public float fillSpeed = 0.5f; // Velocidad de llenado
    public float fadeOutDuration = 1f; // Duración del fade out

    public void LoadScene(int sceneId)
    {
        // Ocultar los paneles de cursos, la barra de navegación y el panel base antes de cargar la escena
        foreach (var panel in coursePanels)
        {
            panel.SetActive(false);
        }
        navbar.SetActive(false);
        basePanel.SetActive(false);

        // Iniciar la pantalla de carga
        StartCoroutine(LoadSceneAsync(sceneId));
    }

    IEnumerator LoadSceneAsync(int sceneId)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneId);
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

                // Iniciar el fade out
                yield return StartCoroutine(FadeOut());

                // Activar la escena
                operation.allowSceneActivation = true;
            }

            yield return null;
        }
    }

    IEnumerator FadeOut()
    {
        Debug.Log("Iniciando Fade Out"); // Para verificar si se llama al Fade Out
        float elapsedTime = 0f;
        while (elapsedTime < fadeOutDuration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeOutDuration);
            yield return null;
        }

        canvasGroup.alpha = 0f; // Asegurarse de que el canvas esté completamente oculto al final del fade out
    }
}
