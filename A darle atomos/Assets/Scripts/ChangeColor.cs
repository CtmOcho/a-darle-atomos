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

    public void ColorChange()
    {
        targetColor = PhToColor((int)liquidProperties.actualPHvalue);
        StartCoroutine(ChangeColorCorroutine(objectRenderer, objectRendererTop, targetColor, transitionSpeed));
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

    Color PhToColor(int ph)
    {
        switch (ph)
        {
            case 0:
                return Color.red;
            case 1:
                return new Color(255 / 255, 90 / 255, 0);
            case 2:
                return new Color(255 / 255, 180 / 255, 0);
            case 3:
                return new Color(255 / 255, 255 / 255, 0);
            case 4:
                return new Color(180 / 255, 230 / 255, 0);
            case 5:
                return new Color(90 / 255, 230 / 255, 0);
            case 6:
                return new Color(0, 230 / 255, 0);
            case 7:
                return new Color(0, 230 / 255, 80 / 255);
            case 8:
                return new Color(0, 230 / 255, 160 / 255);
            case 9:
                return new Color(0, 230 / 255, 255 / 255);
            case 10:
                return new Color(0, 130 / 255, 255 / 255);
            case 11:
                return new Color(0, 80 / 255, 255 / 255);
            case 12:
                return new Color(50 / 255, 50 / 255, 255 / 255);
            case 13:
                return new Color(100 / 255, 90 / 255, 255 / 255);
            case 14:
                return new Color(100 / 255, 50 / 255, 255 / 255);
            default:
                return Color.white;
        }
    }
}
