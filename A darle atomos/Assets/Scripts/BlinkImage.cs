using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlinkImage : MonoBehaviour
{
    public Image imageToBlink;  // La imagen que quieres hacer parpadear
    public float blinkSpeed = 1f;  // Velocidad del parpadeo (cuanto más pequeño, más rápido parpadea)
    public bool shouldBlink = true;  // Controla si la imagen debe seguir parpadeando

    private void Start()
    {
        if (imageToBlink != null)
        {
            StartCoroutine(Blink());
        }
    }

    private IEnumerator Blink()
    {
        while (shouldBlink)
        {
            // Hacemos que la imagen desaparezca
            yield return FadeTo(0.0f, blinkSpeed);
            // Hacemos que la imagen aparezca
            yield return FadeTo(1.0f, blinkSpeed);
        }
    }

    private IEnumerator FadeTo(float targetAlpha, float duration)
    {
        Color color = imageToBlink.color;
        float startAlpha = color.a;

        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / duration)
        {
            color.a = Mathf.Lerp(startAlpha, targetAlpha, t);
            imageToBlink.color = color;
            yield return null;
        }

        color.a = targetAlpha;
        imageToBlink.color = color;
    }
}
