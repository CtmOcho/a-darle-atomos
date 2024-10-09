using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    public DropCollisionController liquidProperties;
    public Renderer objectRenderer;
    public Renderer objectRendererTop;
    public Color targetColor;
    public float transitionSpeed = .01f; // Speed of color change
    public float changeTime = 1f;
    public bool boolfenoftaleina;
    public void ColorChange()
    {
        targetColor = PhToColor((int)liquidProperties.actualPHvalue);
        if (targetColor != objectRenderer.material.color && boolfenoftaleina)
        {
            StartCoroutine(ChangeColorCorroutine(objectRenderer, objectRendererTop, targetColor, transitionSpeed));
        }
    }

    IEnumerator ChangeColorCorroutine(Renderer objectRenderer, Renderer objectRendererTop, Color targetColor, float transitionSpeed)
    {
        changeTime = 0;
        Color initialColor = objectRenderer.material.color;
        Color initialColorTop = objectRendererTop.material.color;
        while (changeTime < 1)
        {
            objectRenderer.material.color = Color.Lerp(initialColor, targetColor, changeTime);
            objectRendererTop.material.color = Color.Lerp(initialColorTop, targetColor, changeTime);

            changeTime += Time.deltaTime * transitionSpeed;

            yield return new WaitForEndOfFrame();
        }
    }

    // TODO: 8chito pongale los colores correctos
    Color PhToColor(int ph)
    {
        switch (ph)
        {
            case 0:
                return Color.red;
            case 1:
                return new Color(1, 90 / 255, 0);
            case 2:
                return new Color(1, 180 / 255, 0);
            case 3:
                return new Color(1, 1, 0);
            case 4:
                return new Color(180 / 255, 0.9f, 0);
            case 5:
                return new Color(90 / 255, 0.9f, 0);
            case 6:
                return new Color(0, 0.9f, 7);
            case 7:
                return new Color(0, 0.9f, 80 / 255);
            case 8:
                return new Color(0, 0.9f, 160 / 255);
            case 9:
                return new Color(0, 0.9f, 1);
            case 10:
                return new Color(0, 130 / 255, 1);
            case 11:
                return new Color(0, 80 / 255, 1);
            case 12:
                return new Color(0.2f, 0.2f, 1);
            case 13:
                return new Color(0.4f, 90 / 255, 1);
            case 14:
                return new Color(0.4f, 0.2f, 1);
            default:
                return Color.white;
        }
    }
}
