using UnityEngine;
using UnityEngine.UI;

public class Atom : MonoBehaviour
{
    public GameObject protonPrefab;
    public GameObject neutronPrefab;
    public GameObject electronPrefab;
    public int numberOfProtons = 11;
    public int numberOfNeutrons = 12;
    public int numberOfElectrons = 11;
    public float electronOrbitRadius = 2f;
    public float electronOrbitSpeed = 100f;
    public Material orbitMaterial;

    private Electron lastCreatedElectron;  // Track the last created electron
    public Image flashImage;
    public Text explanationText;  // Reference to the UI Text component

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
        int[] energyLevels = new int[] { 2, 8, 8, 18 };
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
        lineRenderer.material = orbitMaterial;

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
        int[] energyLevels = new int[] { 2, 8, 8, 18 };
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
                GameObject electronGO = Instantiate(electronPrefab, transform);
                float angle = angleStep * i;
                Vector3 position = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), 0, Mathf.Sin(angle * Mathf.Deg2Rad)) * (electronOrbitRadius + level * 2f);
                electronGO.transform.localPosition = position;

                Electron electronScript = electronGO.GetComponent<Electron>();
                if (electronScript != null)
                {
                    // Asegurar que flashImage esté asignada antes de asignarla al Electron
                    if (flashImage != null)
                    {
                        electronScript.flashImage = flashImage;  // Asignar la referencia de la imagen al electron
                    }
                    else
                    {
                        Debug.LogWarning("Flash Image is not assigned to Atom script.");
                    }

                    // Asegurar que explanationText esté asignado antes de asignarlo al Electron
                    if (explanationText != null)
                    {
                        electronScript.explanationText = explanationText;  // Asignar la referencia del texto al electron
                    }
                    else
                    {
                        Debug.LogWarning("Explanation Text is not assigned to Atom script.");
                    }

                    electronScript.orbitSpeed = electronOrbitSpeed * (1 - level * 0.1f);
                    electronScript.SetOrbit(transform.position, electronOrbitRadius + level * 2f);

                    lastCreatedElectron = electronScript;  // Track the last created electron
                }
                else
                {
                    Debug.LogWarning("Electron prefab does not have an Electron component attached");
                }

                DisableCollisions(electronGO);
            }
        }
    }

    public void ExciteLastElectron()
    {
        if (lastCreatedElectron != null)
        {
            lastCreatedElectron.ExciteElectron();
        }
    }

    public void ReleaseLastElectron()
    {
        if (lastCreatedElectron != null)
        {
            lastCreatedElectron.ReleaseElectron();
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
