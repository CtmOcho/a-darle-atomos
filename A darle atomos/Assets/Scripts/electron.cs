using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Electron : MonoBehaviour
{
    public float orbitSpeed = 100f;
    private Vector3 orbitCenter;
    private float orbitRadius;
    private float initialOrbit;

    public Image flashImage;
    public float flashDuration = 5f; // Longer duration for more noticeable expansion
    public float maxScale = 20f; // Increased scale to ensure full screen coverage

    private float originalAlpha;

    public void SetOrbit(Vector3 center, float radius)
    {
        orbitCenter = center;
        orbitRadius = radius;
        initialOrbit = radius;

        originalAlpha = flashImage.color.a;
        flashImage.color = new Color(1, 0.92f, 0.016f, 0);
    }

    public void ExciteElectron()
    {
        if (orbitRadius == initialOrbit)
        {
        orbitRadius += 2f;
        }
    }

    public void ReleaseElectron()
    {
        if (orbitRadius > initialOrbit)
        {
            orbitRadius -= 2f;
            StartCoroutine(ExpandFlash());
        }
    }

    void Update()
    {
        Orbit();
    }

    void Orbit()
    {
        transform.RotateAround(orbitCenter, Vector3.up, orbitSpeed * Time.deltaTime);
        transform.position = orbitCenter + (transform.position - orbitCenter).normalized * orbitRadius;
    }

    private IEnumerator ExpandFlash()
    {
        RectTransform rectTransform = flashImage.rectTransform;
        Vector3 originalScale = rectTransform.localScale;
        Color originalColor = flashImage.color;

        // Set initial small scale and full opacity
        rectTransform.localScale = Vector3.zero;
        flashImage.color = new Color(originalColor.r, originalColor.g, originalColor.b, 1);

        // Set initial position of flashImage to match the electron's position
        rectTransform.position = Camera.main.WorldToScreenPoint(transform.position);
        Debug.Log("Flash position: " + rectTransform.position);

        float elapsedTime = 0f;

        while (elapsedTime < (10*flashDuration))
        {
            elapsedTime += Time.deltaTime;
            float progress = elapsedTime / (10*flashDuration);

            // Expand the image
            rectTransform.localScale = Vector3.Lerp(Vector3.zero, new Vector3(maxScale, maxScale, maxScale), progress);
            Debug.Log("Flash scale: " + rectTransform.localScale);

            // Fade out the image
            flashImage.color = new Color(originalColor.r, originalColor.g, originalColor.b, 1 - progress);
            Debug.Log("Flash alpha: " + flashImage.color.a);

            yield return null;
        }

        // Ensure the image is fully transparent at the end
        flashImage.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0);
        rectTransform.localScale = originalScale;
    }
}
