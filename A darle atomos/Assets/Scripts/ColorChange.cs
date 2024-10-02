using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChange : MonoBehaviour
{
    public Renderer[] objectRenderer;
    public Color targetColor;
    public float transitionSpeed = .01f; // Speed of color change
    public float changeTime = 1f;

    // Start is called before the first frame update
    void Start()
    {
        print(objectRenderer);
    }

    // Update is called once per frame
    void Update()
    {
        Color initialColor = objectRenderer[0].material.color;
        for (int i = 0; i < objectRenderer.Length; i++)
        {
            objectRenderer[i].material.color = Color.Lerp(initialColor, targetColor, changeTime);
        }

        changeTime += Time.deltaTime * transitionSpeed;
    }
}
