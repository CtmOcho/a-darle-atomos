using UnityEngine;

public class Electron : MonoBehaviour
{
    public float orbitSpeed = 100f;
    private Vector3 orbitCenter;
    private float orbitRadius;

    public void SetOrbit(Vector3 center, float radius)
    {
        orbitCenter = center;
        orbitRadius = radius;
    }

    void Update()
    {
        Orbit();
    }

    void Orbit()
    {
        transform.RotateAround(orbitCenter, Vector3.up, orbitSpeed * Time.deltaTime);
    }
}
