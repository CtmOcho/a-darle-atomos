using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public Image progressBarFill;

    void Start()
    {
        if (progressBarFill != null)
        {
            // Suponiendo que el progreso está en un rango de 0.0 a 1.0
            float progress = Mathf.Clamp(SessionData.progreso, 0f, 1f);
            progressBarFill.fillAmount = progress;
        }
        else
        {
            Debug.LogError("Progress bar fill image reference is not set.");
        }
    }
}
