using UnityEngine;

public class SimpleElectron : MonoBehaviour
{
    public float orbitSpeed;
    private Vector3 orbitCenter;
    private float orbitRadius;

    public void SetOrbit(Vector3 center, float radius)
    {
        orbitCenter = center;
        orbitRadius = radius;
    }

    void Update()
    {
        OrbitAroundCenter();
    }

    void OrbitAroundCenter()
    {
        transform.RotateAround(orbitCenter, Vector3.forward, orbitSpeed * Time.deltaTime);
    }
}
