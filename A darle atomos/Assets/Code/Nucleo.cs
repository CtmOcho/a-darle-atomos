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
    public float electronOrbitSpeed = 30f;  // Ajusta este valor para cambiar la velocidad de las órbitas de los electrones

    void Start()
    {
        // Check for null assignments
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
        for (int i = 0; i < numberOfProtons; i++)
        {
            Instantiate(protonPrefab, Random.insideUnitSphere * 0.5f, Quaternion.identity, transform);
        }
        for (int i = 0; i < numberOfNeutrons; i++)
        {
            Instantiate(neutronPrefab, Random.insideUnitSphere * 0.5f, Quaternion.identity, transform);
        }

        // Crear los electrones
        int[] energyLevels = new int[] { 2, 8, 8, 18, 18, 32 };  // Niveles de energía y su capacidad máxima de electrones
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
                    electronScript.orbitSpeed = electronOrbitSpeed * (1 - level * 0.1f);
                }
                else
                {
                    Debug.LogWarning("Electron prefab does not have an Electron component attached");
                }
            }
        }
    }
}