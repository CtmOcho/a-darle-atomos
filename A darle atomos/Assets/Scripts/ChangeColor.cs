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
        targetColor = PhToColor(liquidProperties.actualPHvalue);
        //Debug.Log(liquidProperties.actualPHvalue);
        if (targetColor != objectRenderer.material.color && boolfenoftaleina)
        {

            Debug.Log("aaaa");
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

    // Función que devuelve el color dependiendo del intervalo del pH
    Color PhToColor(float ph)
    {
        // Definimos los valores de color correspondientes a los valores de pH
        Color[] phColors = new Color[]
        {
            new Color(238/255f,28/255f,37/255f),  // pH 0
            new Color(242/255f, 103 / 255f, 36/255f),  // pH 1
            new Color(249/255f, 197 / 255f, 17/255f),  // pH 2
            new Color(245/255f, 237/255f, 28/255f),  // pH 3
            new Color(181/255f, 211/255f, 51/255f),  // pH 4
            new Color(132/255f, 195 / 255f, 65/255f),  // pH 5
            new Color(77/255f, 183/255f, 73/255f),  // pH 6
            new Color(51/255f, 169/255f, 75 / 255f),  // pH 7
            new Color(34/255f, 180/255f, 107/255f),  // pH 8
            new Color(11/255f, 184/255f, 182/255f),  // pH 9
            new Color(70/255f, 144 / 255f, 205/255f),  // pH 10
            new Color(56/255f, 83 / 255f, 164/255f),  // pH 11
            new Color(90/255f, 81/255f, 162/255f),  // pH 12
            new Color(99/255f, 69 / 255f, 157/255f),  // pH 13
            new Color(70/255f, 44/255f, 131/255f)   // pH 14
        };

        // Calculamos los índices para la interpolación entre los valores más cercanos
        int lowerIndex = Mathf.FloorToInt(ph);  // Valor entero menor o igual
        int upperIndex = Mathf.CeilToInt(ph);   // Valor entero mayor o igual

        // Asegurarnos de que los índices están dentro de los límites del array
        lowerIndex = Mathf.Clamp(lowerIndex, 0, phColors.Length - 1);
        upperIndex = Mathf.Clamp(upperIndex, 0, phColors.Length - 1);

        // Si el pH es un valor entero, devolvemos el color correspondiente directamente
        if (lowerIndex == upperIndex)
        {
            return phColors[lowerIndex];
        }

        // Interpolamos entre los dos colores correspondientes a los valores de pH cercanos
        float t = ph - lowerIndex;  // Valor decimal entre 0 y 1 que indica qué tan cerca está del siguiente valor
        return Color.Lerp(phColors[lowerIndex], phColors[upperIndex], t);
    }
}
