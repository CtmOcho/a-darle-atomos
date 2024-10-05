using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    public Renderer objectRenderer;
    public Renderer objectRendererTop;
    public Color targetColor;
    public float transitionSpeed = .01f; // Speed of color change
    private float changeTime = 1f;

    public void ColorChange()
    {
        StartCoroutine(ChangeColorCorroutine(objectRenderer, objectRendererTop, targetColor, transitionSpeed));
    }

    IEnumerator ChangeColorCorroutine(Renderer objectRenderer, Renderer objectRendererTop, Color targetColor, float transitionSpeed)
    {
        changeTime = 0;
        while (changeTime < 1)
        {
            Color initialColor = objectRenderer.material.color;
            Color initialColorTop = objectRendererTop.material.color;
            objectRenderer.material.color = Color.Lerp(initialColor, targetColor, changeTime);
            objectRendererTop.material.color = Color.Lerp(initialColorTop, targetColor, changeTime);

            changeTime += Time.deltaTime * transitionSpeed;

            yield return new WaitForEndOfFrame();
        }
    }
}
