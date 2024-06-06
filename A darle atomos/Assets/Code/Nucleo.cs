using UnityEngine;

public class Atom : MonoBehaviour
{
    public GameObject protonPrefab;
    public GameObject neutronPrefab;
    public GameObject electronPrefab;
    public int numberOfProtons = 1;
    public int numberOfNeutrons = 0;
    public int numberOfElectrons = 1;
    public float electronOrbitRadius = 2f;  // Ajusta este valor para cambiar el radio de las órbitas de los electrones
    public float electronOrbitSpeed = 100f;  // Ajusta este valor para cambiar la velocidad de las órbitas de los electrones

    void Start()
    {
        // Verificar que los prefabs estén asignados
        if (protonPrefab == null)
        {
            Debug.LogWarning("Proton Prefab is not assigned in the Inspector");
            return;
        }
        if (neutronPrefab == null)
        {
            Debug.LogWarning("Neutron Prefab is not assigned in the Inspector");
            return;
        }
        if (electronPrefab == null)
        {
            Debug.LogWarning("Electron Prefab is not assigned in the Inspector");
            return;
        }

        // Crear el núcleo
        CreateNucleus();

        // Crear órbitas para los electrones
        CreateOrbits();

        // Crear los electrones
        CreateElectrons();
    }

    void CreateNucleus()
    {
        for (int i = 0; i < numberOfProtons; i++)
        {
            Vector3 position = Random.insideUnitSphere * 0.5f;
            GameObject proton = Instantiate(protonPrefab, position, Quaternion.identity, transform);
            DisableCollisions(proton);
        }
        for (int i = 0; i < numberOfNeutrons; i++)
        {
            Vector3 position = Random.insideUnitSphere * 0.05f;
            GameObject neutron = Instantiate(neutronPrefab, position, Quaternion.identity, transform);
            DisableCollisions(neutron);
        }
    }

    void CreateOrbits()
    {
        int[] energyLevels = new int[] { 2, 8, 8, 18 }; // Asegurarse de tener al menos cuatro niveles de energía
        for (int level = 0; level < energyLevels.Length; level++)
        {
            DrawOrbit(electronOrbitRadius + level * 2f);
        }
    }

    void DrawOrbit(float radius)
    {
        GameObject orbit = new GameObject("Orbit");
        orbit.transform.parent = transform;
        LineRenderer lineRenderer = orbit.AddComponent<LineRenderer>();
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.positionCount = 100;
        lineRenderer.useWorldSpace = false;

        float angleStep = 360f / lineRenderer.positionCount;
        for (int i = 0; i < lineRenderer.positionCount; i++)
        {
            float angle = i * angleStep * Mathf.Deg2Rad;
            Vector3 position = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * radius;
            lineRenderer.SetPosition(i, position);
        }

        DisableCollisions(orbit);
    }

    void CreateElectrons()
    {
        int[] energyLevels = new int[] { 2, 8, 8, 18 };  // Niveles de energía y su capacidad máxima de electrones
        int remainingElectrons = numberOfElectrons;

        for (int level = 0; level < energyLevels.Length; level++)
        {
            if (remainingElectrons <= 0)
            {
                break;
            }

            int electronsInThisLevel = Mathf.Min(energyLevels[level], remainingElectrons);
            remainingElectrons -= electronsInThisLevel;

            float angleStep = 360f / electronsInThisLevel;
            for (int i = 0; i < electronsInThisLevel; i++)
            {
                GameObject electron = Instantiate(electronPrefab, transform);
                float angle = angleStep * i;
                Vector3 position = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), 0, Mathf.Sin(angle * Mathf.Deg2Rad)) * (electronOrbitRadius + level * 2f);
                electron.transform.localPosition = position;

                Electron electronScript = electron.GetComponent<Electron>();
                if (electronScript != null)
                {
                    electronScript.orbitSpeed = electronOrbitSpeed*(1-level*0.1f);
                    electronScript.SetOrbit(transform.position, electronOrbitRadius + level * 2f);
                }
                else
                {
                    Debug.LogWarning("Electron prefab does not have an Electron component attached");
                }

                DisableCollisions(electron);
            }
        }
    }

    void DisableCollisions(GameObject obj)
    {
        Collider collider = obj.GetComponent<Collider>();
        if (collider != null)
        {
            collider.enabled = false;
        }
    }
}
