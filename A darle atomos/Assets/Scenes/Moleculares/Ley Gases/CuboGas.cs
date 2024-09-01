using UnityEngine;
using UnityEngine.UI;

public class CubeScaleController : MonoBehaviour
{
    public Slider scaleSlider; // Asigna el slider desde el inspector
    private Transform cubeTransform;
    private Vector3 targetScale;
    private Vector3 currentScale;
    public float lerpSpeed = 5f;  // Velocidad de interpolación

    void Start()
    {
        cubeTransform = transform;
        currentScale = cubeTransform.localScale;
        targetScale = new Vector3(scaleSlider.value, scaleSlider.value, scaleSlider.value);
        scaleSlider.onValueChanged.AddListener(OnScaleChanged);
        UpdateScale(scaleSlider.value); // Inicializa el tamaño del cubo
    }

    void Update()
    {
        // Interpola la escala actual hacia la escala objetivo
        currentScale = Vector3.Lerp(currentScale, targetScale, Time.deltaTime * lerpSpeed);
        cubeTransform.localScale = currentScale;
    }

    void OnScaleChanged(float value)
    {
        // Actualiza la escala objetivo cuando el slider cambia
        targetScale = new Vector3(value, value, value);
    }

    void UpdateScale(float value)
    {
        // Inicializa la escala del cubo
        cubeTransform.localScale = new Vector3(value, value, value);
        currentScale = cubeTransform.localScale;
    }
}
