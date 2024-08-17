using UnityEngine;
using UnityEngine.UI;

public class CubeScaleController : MonoBehaviour
{
    public Slider scaleSlider; // Asigna el slider desde el inspector
    private Transform cubeTransform;

    void Start()
    {
        cubeTransform = transform;
        scaleSlider.onValueChanged.AddListener(UpdateScale);
        UpdateScale(scaleSlider.value); // Inicializa el tamaño del cubo
    }

    void UpdateScale(float value)
    {
        cubeTransform.localScale = new Vector3(value, value, value);
    }
}
