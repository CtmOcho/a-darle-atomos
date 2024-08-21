using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Atomperiodica : MonoBehaviour
{
    public GameObject coreProtonPrefab;
    public GameObject coreNeutronPrefab;
    public GameObject orbitElectronPrefab;
    public int orbitElectrons = 11;
    public float electronOrbitDistance = 2f;
    public float electronRotationSpeed = 100f;
    public Material electronOrbitMaterial;
    public TMP_Text uiText;

    private Dictionary<int, Element> elements;

    void Start()
    {
        InitializePeriodicTable();
        CreateCore();
        CreateElectronOrbits();
        CreateOrbitingElectrons();
        DisplayElementProperties();
    }

    void InitializePeriodicTable()
    {
        elements = new Dictionary<int, Element>
        {
            { 1, new Element("Hidrógeno", "H", 1.008f, 1, 1) },
            { 2, new Element("Helio", "He", 4.0026f, 18, 1) },
            { 3, new Element("Litio", "Li", 6.94f, 1, 2) },
            { 4, new Element("Berilio", "Be", 9.0122f, 2, 2) },
            { 5, new Element("Boro", "B", 10.81f, 13, 2) },
            { 6, new Element("Carbono", "C", 12.01f, 14, 2) },
            { 7, new Element("Nitrógeno", "N", 14.01f, 15, 2) },
            { 8, new Element("Oxígeno", "O", 16.00f, 16, 2) },
            { 9, new Element("Flúor", "F", 19.00f, 17, 2) },
            { 10, new Element("Neón", "Ne", 20.18f, 18, 2) },
            { 11, new Element("Sodio", "Na", 22.990f, 1, 3) },
            { 12, new Element("Magnesio", "Mg", 24.305f, 2, 3) },
            { 13, new Element("Aluminio", "Al", 26.982f, 13, 3) },
            { 14, new Element("Silicio", "Si", 28.085f, 14, 3) },
            { 15, new Element("Fósforo", "P", 30.974f, 15, 3) },
            { 16, new Element("Azufre", "S", 32.06f, 16, 3) },
            { 17, new Element("Cloro", "Cl", 35.45f, 17, 3) },
            { 18, new Element("Argón", "Ar", 39.948f, 18, 3) },
            { 19, new Element("Potasio", "K", 39.10f, 1, 4) },
            { 20, new Element("Calcio", "Ca", 40.08f, 2, 4) },
            { 21, new Element("Escandio", "Sc", 44.96f, 3, 4) },
            { 22, new Element("Titanio", "Ti", 47.87f, 4, 4) },
            { 23, new Element("Vanadio", "V", 50.94f, 5, 4) },
            { 24, new Element("Cromo", "Cr", 51.996f, 6, 4) },
            { 25, new Element("Manganeso", "Mn", 54.94f, 7, 4) },
            { 26, new Element("Hierro", "Fe", 55.85f, 8, 4) },
            { 27, new Element("Cobalto", "Co", 58.93f, 9, 4) },
            { 28, new Element("Níquel", "Ni", 58.69f, 10, 4) },
            { 29, new Element("Cobre", "Cu", 63.55f, 11, 4) },
            { 30, new Element("Zinc", "Zn", 65.38f, 12, 4) },
            { 31, new Element("Galio", "Ga", 69.72f, 13, 4) },
            { 32, new Element("Germanio", "Ge", 72.63f, 14, 4) },
            { 33, new Element("Arsénico", "As", 74.92f, 15, 4) },
            { 34, new Element("Selenio", "Se", 78.97f, 16, 4) },
            { 35, new Element("Bromo", "Br", 79.90f, 17, 4) },
            { 36, new Element("Kriptón", "Kr", 83.80f, 18, 4) },
            { 37, new Element("Rubidio", "Rb", 85.47f, 1, 5) },
            { 38, new Element("Estroncio", "Sr", 87.62f, 2, 5) },
            { 39, new Element("Itrio", "Y", 88.91f, 3, 5) },
            { 40, new Element("Zirconio", "Zr", 91.22f, 4, 5) },
            { 41, new Element("Niobio", "Nb", 92.91f, 5, 5) },
            { 42, new Element("Molibdeno", "Mo", 95.94f, 6, 5) },
            { 43, new Element("Tecnecio", "Tc", 98.00f, 7, 5) },
            { 44, new Element("Rutenio", "Ru", 101.07f, 8, 5) },
            { 45, new Element("Rodio", "Rh", 102.91f, 9, 5) },
            { 46, new Element("Paladio", "Pd", 106.42f, 10, 5) },
            { 47, new Element("Plata", "Ag", 107.87f, 11, 5) },
            { 48, new Element("Cadmio", "Cd", 112.41f, 12, 5) },
            { 49, new Element("Indio", "In", 114.82f, 13, 5) },
            { 50, new Element("Estaño", "Sn", 118.71f, 14, 5) },
            { 51, new Element("Antimonio", "Sb", 121.76f, 15, 5) },
            { 52, new Element("Telurio", "Te", 127.60f, 16, 5) },
            { 53, new Element("Yodo", "I", 126.90f, 17, 5) },
            { 54, new Element("Xenón", "Xe", 131.29f, 18, 5) },
            { 55, new Element("Cesio", "Cs", 132.91f, 1, 6) },
            { 56, new Element("Bario", "Ba", 137.33f, 2, 6) },
            { 57, new Element("Lantano", "La", 138.91f, 3, 6) },
            { 58, new Element("Cerio", "Ce", 140.12f, 4, 6) },
            { 59, new Element("Praseodimio", "Pr", 140.91f, 5, 6) },
            { 60, new Element("Neodimio", "Nd", 144.24f, 6, 6) },
            { 61, new Element("Prometio", "Pm", 145.00f, 7, 6) },
            { 62, new Element("Samario", "Sm", 150.36f, 8, 6) },
            { 63, new Element("Europio", "Eu", 151.96f, 9, 6) },
            { 64, new Element("Gadolinio", "Gd", 157.25f, 10, 6) },
            { 65, new Element("Terbio", "Tb", 158.93f, 11, 6) },
            { 66, new Element("Disprosio", "Dy", 162.50f, 12, 6) },
            { 67, new Element("Holmio", "Ho", 164.93f, 13, 6) },
            { 68, new Element("Erbio", "Er", 167.26f, 14, 6) },
            { 69, new Element("Tulio", "Tm", 168.93f, 15, 6) },
            { 70, new Element("Iterbio", "Yb", 173.04f, 16, 6) },
            { 71, new Element("Lutecio", "Lu", 174.97f, 17, 6) },
            { 72, new Element("Hafnio", "Hf", 178.49f, 4, 6) },
            { 73, new Element("Tantalio", "Ta", 180.95f, 5, 6) },
            { 74, new Element("Wolframio", "W", 183.84f, 6, 6) },
            { 75, new Element("Renio", "Re", 186.21f, 7, 6) },
            { 76, new Element("Osmio", "Os", 190.23f, 8, 6) },
            { 77, new Element("Iridio", "Ir", 192.22f, 9, 6) },
            { 78, new Element("Platino", "Pt", 195.08f, 10, 6) },
            { 79, new Element("Oro", "Au", 196.97f, 11, 6) },
            { 80, new Element("Mercurio", "Hg", 200.59f, 12, 6) },
            { 81, new Element("Talio", "Tl", 204.38f, 13, 6) },
            { 82, new Element("Plomo", "Pb", 207.2f, 14, 6) },
            { 83, new Element("Bismuto", "Bi", 209.0f, 15, 6) },
            { 84, new Element("Polonio", "Po", 209.0f, 16, 6) },
            { 85, new Element("Astato", "At", 210.0f, 17, 6) },
            { 86, new Element("Radón", "Rn", 222.0f, 18, 6) },
            { 87, new Element("Francio", "Fr", 223.0f, 1, 7) },
            { 88, new Element("Radio", "Ra", 226.0f, 2, 7) },
            { 89, new Element("Actinio", "Ac", 227.0f, 3, 7) },
            { 90, new Element("Torio", "Th", 232.04f, 4, 7) },
            { 91, new Element("Protactinio", "Pa", 231.04f, 5, 7) },
            { 92, new Element("Urano", "U", 238.03f, 6, 7) },
            { 93, new Element("Neptunio", "Np", 237.0f, 7, 7) },
            { 94, new Element("Plutonio", "Pu", 244.0f, 8, 7) },
            { 95, new Element("Americio", "Am", 243.0f, 9, 7) },
            { 96, new Element("Curio", "Cm", 247.0f, 10, 7) },
            { 97, new Element("Berkelio", "Bk", 247.0f, 11, 7) },
            { 98, new Element("Californio", "Cf", 251.0f, 12, 7) },
            { 99, new Element("Einstenio", "Es", 252.0f, 13, 7) },
            { 100, new Element("Fermio", "Fm", 257.0f, 14, 7) },
            { 101, new Element("Mendelevio", "Md", 258.0f, 15, 7) },
            { 102, new Element("Nobelio", "No", 259.0f, 16, 7) },
            { 103, new Element("Lawrencio", "Lr", 266.0f, 17, 7) },
            { 104, new Element("Rutherfordio", "Rf", 267.0f, 4, 7) },
            { 105, new Element("Dubnio", "Db", 270.0f, 5, 7) },
            { 106, new Element("Seaborgio", "Sg", 271.0f, 6, 7) },
            { 107, new Element("Bohrio", "Bh", 270.0f, 7, 7) },
            { 108, new Element("Hassium", "Hs", 277.0f, 8, 7) },
            { 109, new Element("Meitnerio", "Mt", 278.0f, 9, 7) },
            { 110, new Element("Darmstadio", "Ds", 281.0f, 10, 7) },
            { 111, new Element("Roentgenio", "Rg", 282.0f, 11, 7) },
            { 112, new Element("Copernicio", "Cn", 285.0f, 12, 7) },
            { 113, new Element("Nihonio", "Nh", 286.0f, 13, 7) },
            { 114, new Element("Flerovio", "Fl", 289.0f, 14, 7) },
            { 115, new Element("Moscovio", "Mc", 290.0f, 15, 7) },
            { 116, new Element("Livermorio", "Lv", 293.0f, 16, 7) },
            { 117, new Element("Tenesino", "Ts", 294.0f, 17, 7) },
            { 118, new Element("Oganesón", "Og", 294.0f, 18, 7) }
        };
    }

    void DisplayElementProperties()
    {
        int atomicNumber = orbitElectrons;

        if (elements.ContainsKey(atomicNumber))
        {
            Element element = elements[atomicNumber];

            string propertiesText = $"Elemento: {element.Name}\n" +
                                    $"Símbolo: {element.Symbol}\n" +
                                    $"Número atómico: {atomicNumber}\n" +
                                    $"Masa atómica: {element.AtomicMass}\n" +
                                    $"Grupo: {element.Group}\n" +
                                    $"Período: {element.Period}";

            uiText.text = propertiesText;

            Vector3 bohrModelWorldPosition = transform.position;
            Vector3 screenPosition = Camera.main.WorldToScreenPoint(bohrModelWorldPosition);

            RectTransform uiTextRectTransform = uiText.GetComponent<RectTransform>();
            uiTextRectTransform.position = new Vector3(screenPosition.x + 150f, screenPosition.y - 25f, screenPosition.z);
        }
        else
        {
            uiText.text = "Elemento no encontrado.";
        }
    }

     void CreateCore()
    {
        int coreProtons = orbitElectrons;
        int coreNeutrons = Mathf.RoundToInt(elements[coreProtons].AtomicMass) - coreProtons;

        for (int i = 0; i < coreProtons; i++)
        {
            Vector3 position = Random.insideUnitSphere * 0.1f;
            GameObject proton = Instantiate(coreProtonPrefab, transform.position + position, Quaternion.identity, transform);
            DisableCollisions(proton);
        }
        for (int i = 0; i < coreNeutrons; i++)
        {
            Vector3 position = Random.insideUnitSphere * 0.1f;
            GameObject neutron = Instantiate(coreNeutronPrefab, transform.position + position, Quaternion.identity, transform);
            DisableCollisions(neutron);
        }
    }
    void CreateElectronOrbits()
    {
        int[] energyLevels = new int[] { 2, 8, 18, 32, 32, 18, 8 };
        int remainingElectrons = orbitElectrons;

        for (int level = 0; level < energyLevels.Length && remainingElectrons > 0; level++)
        {
            int electronsInThisLevel = Mathf.Min(energyLevels[level], remainingElectrons);
            if (electronsInThisLevel > 0)
            {
                DrawOrbitPath(electronOrbitDistance + level * 2f);
            }
            remainingElectrons -= electronsInThisLevel;
        }
    }

     void DrawOrbitPath(float radius)
    {
        GameObject orbit = new GameObject("OrbitPath");
        orbit.transform.parent = transform;
        orbit.transform.localPosition = Vector3.zero;
        LineRenderer lineRenderer = orbit.AddComponent<LineRenderer>();
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.positionCount = 100;
        lineRenderer.useWorldSpace = false;
        lineRenderer.material = electronOrbitMaterial;

        float angleStep = 360f / lineRenderer.positionCount;
        for (int i = 0; i < lineRenderer.positionCount; i++)
        {
            float angle = i * angleStep * Mathf.Deg2Rad;
            Vector3 position = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * radius;
            lineRenderer.SetPosition(i, position);
        }

        DisableCollisions(orbit);
    }
    void CreateOrbitingElectrons()
    {
        int[] energyLevels = new int[] { 2, 8, 18, 32, 32, 18, 8 };
        int remainingElectrons = orbitElectrons;

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
                GameObject electronGO = Instantiate(orbitElectronPrefab, transform);
                float angle = angleStep * i;
                Vector3 position = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad), 0) * (electronOrbitDistance + level * 2f);
                electronGO.transform.localPosition = position;

                SimpleElectron electronScript = electronGO.GetComponent<SimpleElectron>();
                if (electronScript != null)
                {
                    electronScript.orbitSpeed = electronRotationSpeed * (1 - level * 0.1f);
                    electronScript.SetOrbit(transform.position, electronOrbitDistance + level * 2f);
                }
                else
                {
                    Debug.LogWarning("Electron prefab does not have a SimpleElectron component attached");
                }

                DisableCollisions(electronGO);
            }
        }
    }

    private class Element
    {
        public string Name { get; }
        public string Symbol { get; }
        public float AtomicMass { get; }
        public int Group { get; }
        public int Period { get; }

        public Element(string name, string symbol, float atomicMass, int group, int period)
        {
            Name = name;
            Symbol = symbol;
            AtomicMass = atomicMass;
            Group = group;
            Period = period;
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
