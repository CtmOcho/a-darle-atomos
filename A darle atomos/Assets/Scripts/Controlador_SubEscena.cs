using UnityEngine;
using System.Collections;

public class Controlador_SubEscena : MonoBehaviour
{

    public CanvasGroup canvasGroup;  // Declara la variable canvasGroup como pública para asignarla en el inspector
    public CanvasGroup canvasGroup1;  // Declara la variable canvasGroup como pública para asignarla en el inspector
    public float fadeDuration = 2f;  // Declara la variable fadeDuration y dale un valor por defecto





    void Start()
    {
        StartCoroutine(FadeInCanvas());
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

        canvasGroup.alpha = 0f;
        canvasGroup.interactable = false;  // Desactiva la interacción
        canvasGroup.blocksRaycasts = false; // Evita que bloquee otros objetos
        canvasGroup.gameObject.SetActive(false); // Descarga el canvas sin destruirlo
        // Asegura que el fade in termine en transparencia total

    }
    IEnumerator FadeInCum()
    {
        float elapsedTime = 0f;
        canvasGroup1.alpha = 0f; // Comienza con la imagen completamente transparente

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup1.alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration); // Interpola el valor de alpha de 0 a 1
            yield return null;
        }

        canvasGroup1.alpha = 1f; // Asegura que el fade in termine completamente visible
    }

}

