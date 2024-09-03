using UnityEngine;
using System.Collections;

public class ControladorEscena : MonoBehaviour
{
    public GameObject Subexperience; // Asume que quieres desactivar este Canvas
    public CanvasGroup canvasGroup;  // Declara la variable canvasGroup como pública para asignarla en el inspector
    public CanvasGroup camara;  // Declara la variable canvasGroup como pública para asignarla en el inspector
    public float fadeDuration = 2f;  // Declara la variable fadeDuration y dale un valor por defecto
    public CanvasGroup canvasGroupDistilation;  // Declara la variable canvasGroup como pública para asignarla en el inspector




    void Start()
    {
        ResetScene();
        StartCoroutine(FadeInCanvas());
        StartCoroutine(FadeInCam());
        StartCoroutine(FadeInCum());

    }

    IEnumerator FadeInCanvas()
    {
        float elapsedTime = 0f;
        canvasGroup.alpha = 1f; // Comienza con la imagen completamente opaca

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration); // Interpola el valor de alpha
            yield return null;
        }

        canvasGroup.alpha = 0f; // Asegura que el fade in termine en transparencia total
    } 
    IEnumerator FadeInCam()
    {
        float elapsedTime = 0f;
        camara.alpha = 0f; // Comienza con la imagen completamente transparente

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            camara.alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration); // Interpola el valor de alpha de 0 a 1
            yield return null;
        }

        camara.alpha = 1f; // Asegura que el fade in termine completamente visible
    }


    IEnumerator FadeInCum()
    {
        float elapsedTime = 0f;
        canvasGroupDistilation.alpha = 0f; // Comienza con la imagen completamente transparente
        
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroupDistilation.alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration); // Interpola el valor de alpha de 0 a 1
            yield return null;
        }

        canvasGroupDistilation.alpha = 1f; // Asegura que el fade in termine completamente visible
    }

    public void ResetScene()
    {
        // Desactiva el Canvas de Subexperience si está activo
        if (Subexperience != null && Subexperience.activeSelf)
        {
            Subexperience.SetActive(false);
        }

    }

}
