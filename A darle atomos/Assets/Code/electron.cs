using UnityEngine;

public class Electron : MonoBehaviour
{
    public float orbitSpeed = 1f;
    private LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;
        lineRenderer.positionCount = 100;
        DrawOrbit();
    }

    void Update()
    {
        transform.RotateAround(transform.parent.position, Vector3.up, orbitSpeed * Time.deltaTime);
    }

    void DrawOrbit()
    {
        for (int i = 0; i < lineRenderer.positionCount; i++)
        {
            float angle = i * 360f / lineRenderer.positionCount;
            Vector3 position = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), 0, Mathf.Sin(angle * Mathf.Deg2Rad)) * transform.localPosition.magnitude;
            lineRenderer.SetPosition(i, position);
        }
    }
}
