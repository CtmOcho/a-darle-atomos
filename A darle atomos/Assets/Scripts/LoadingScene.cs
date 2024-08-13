using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour
{
    public GameObject LoadingScreen;
    public Image LoadingBarFill;
    public GameObject ScrollView; // Referencia al ScrollView
    public float delayAfterLoad = 1f; // Tiempo de espera adicional después de que la barra se llena
    public float fillSpeed = 0.5f; // Velocidad de llenado

    public void LoadScene(int sceneId)
    {
        // Ocultar el ScrollView antes de cargar la escena
        ScrollView.SetActive(false);

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

                // Activar la escena
                operation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
