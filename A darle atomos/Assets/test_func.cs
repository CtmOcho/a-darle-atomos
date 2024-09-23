using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class Test_func : MonoBehaviour
{
    public GameObject tableElementPrefab; // Prefab del elemento de la tabla
    public Transform gridParent; // Parent de la grid (puede ser un Canvas o un objeto en la escena)
    public Vector2 gridSpacing = new Vector2(50, 50); // Espaciado entre elementos de la tabla

    private Dictionary<int, Element> elements;
    public GameObject coreProtonPrefab;
    public GameObject coreNeutronPrefab;
    public GameObject orbitElectronPrefab;
    public int orbitElectrons = 0;
    public float electronOrbitDistance = 2f;
    public float electronRotationSpeed = 100f;
    public Material electronOrbitMaterial;
    public TMP_Text uiText;

    private Dictionary<int, Vector2> customElementPositions;
    private GameObject lastSelectedElement;
   void Start()
    {
        InitializePeriodicTable();
        InitializeCustomPositions();
        CreatePeriodicTable();
    
    }

    void InitializePeriodicTable()
    {
        elements = new Dictionary<int, Element>
        {
        { 1, new Element("Hidrógeno", "H", 1.008f, 1, 1, "No Metal") },
        { 2, new Element("Helio", "He", 4.0026f, 18, 1, "Gas Noble") },
        { 3, new Element("Litio", "Li", 6.94f, 1, 2, "Metal Alcalino") },
        { 4, new Element("Berilio", "Be", 9.0122f, 2, 2, "Metal Alcalinotérreo") },
        { 5, new Element("Boro", "B", 10.81f, 13, 2, "Metaloide") },
        { 6, new Element("Carbono", "C", 12.01f, 14, 2, "No Metal") },
        { 7, new Element("Nitrógeno", "N", 14.01f, 15, 2, "No Metal") },
        { 8, new Element("Oxígeno", "O", 16.00f, 16, 2, "No Metal") },
        { 9, new Element("Flúor", "F", 19.00f, 17, 2, "Halógeno") },
        { 10, new Element("Neón", "Ne", 20.18f, 18, 2, "Gas Noble") },
        { 11, new Element("Sodio", "Na", 22.990f, 1, 3, "Metal Alcalino") },
        { 12, new Element("Magnesio", "Mg", 24.305f, 2, 3, "Metal Alcalinotérreo") },
        { 13, new Element("Aluminio", "Al", 26.982f, 13, 3, "Metal") },
        { 14, new Element("Silicio", "Si", 28.085f, 14, 3, "Metaloide") },
        { 15, new Element("Fósforo", "P", 30.974f, 15, 3, "No Metal") },
        { 16, new Element("Azufre", "S", 32.06f, 16, 3, "No Metal") },
        { 17, new Element("Cloro", "Cl", 35.45f, 17, 3, "Halógeno") },
        { 18, new Element("Argón", "Ar", 39.948f, 18, 3, "Gas Noble") },
        { 19, new Element("Potasio", "K", 39.10f, 1, 4, "Metal Alcalino") },
        { 20, new Element("Calcio", "Ca", 40.08f, 2, 4, "Metal Alcalinotérreo") },
        { 21, new Element("Escandio", "Sc", 44.96f, 3, 4, "Metal de Transición") },
        { 22, new Element("Titanio", "Ti", 47.87f, 4, 4, "Metal de Transición") },
        { 23, new Element("Vanadio", "V", 50.94f, 5, 4, "Metal de Transición") },
        { 24, new Element("Cromo", "Cr", 51.996f, 6, 4, "Metal de Transición") },
        { 25, new Element("Manganeso", "Mn", 54.94f, 7, 4, "Metal de Transición") },
        { 26, new Element("Hierro", "Fe", 55.85f, 8, 4, "Metal de Transición") },
        { 27, new Element("Cobalto", "Co", 58.93f, 9, 4, "Metal de Transición") },
        { 28, new Element("Níquel", "Ni", 58.69f, 10, 4, "Metal de Transición") },
        { 29, new Element("Cobre", "Cu", 63.55f, 11, 4, "Metal de Transición") },
        { 30, new Element("Zinc", "Zn", 65.38f, 12, 4, "Metal de Transición") },
        { 31, new Element("Galio", "Ga", 69.72f, 13, 4, "Metal") },
        { 32, new Element("Germanio", "Ge", 72.63f, 14, 4, "Metaloide") },
        { 33, new Element("Arsénico", "As", 74.92f, 15, 4, "Metaloide") },
        { 34, new Element("Selenio", "Se", 78.97f, 16, 4, "No Metal") },
        { 35, new Element("Bromo", "Br", 79.90f, 17, 4, "Halógeno") },
        { 36, new Element("Kriptón", "Kr", 83.80f, 18, 4, "Gas Noble") },
        { 37, new Element("Rubidio", "Rb", 85.47f, 1, 5, "Metal Alcalino") },
        { 38, new Element("Estroncio", "Sr", 87.62f, 2, 5, "Metal Alcalinotérreo") },
        { 39, new Element("Itrio", "Y", 88.91f, 3, 5, "Metal de Transición") },
        { 40, new Element("Zirconio", "Zr", 91.22f, 4, 5, "Metal de Transición") },
        { 41, new Element("Niobio", "Nb", 92.91f, 5, 5, "Metal de Transición") },
        { 42, new Element("Molibdeno", "Mo", 95.94f, 6, 5, "Metal de Transición") },
        { 43, new Element("Tecnecio", "Tc", 98.00f, 7, 5, "Metal de Transición") },
        { 44, new Element("Rutenio", "Ru", 101.07f, 8, 5, "Metal de Transición") },
        { 45, new Element("Rodio", "Rh", 102.91f, 9, 5, "Metal de Transición") },
        { 46, new Element("Paladio", "Pd", 106.42f, 10, 5, "Metal de Transición") },
        { 47, new Element("Plata", "Ag", 107.87f, 11, 5, "Metal de Transición") },
        { 48, new Element("Cadmio", "Cd", 112.41f, 12, 5, "Metal de Transición") },
        { 49, new Element("Indio", "In", 114.82f, 13, 5, "Metal") },
        { 50, new Element("Estaño", "Sn", 118.71f, 14, 5, "Metal") },
        { 51, new Element("Antimonio", "Sb", 121.76f, 15, 5, "Metaloide") },
        { 52, new Element("Telurio", "Te", 127.60f, 16, 5, "Metaloide") },
        { 53, new Element("Yodo", "I", 126.90f, 17, 5, "Halógeno") },
        { 54, new Element("Xenón", "Xe", 131.29f, 18, 5, "Gas Noble") },
        { 55, new Element("Cesio", "Cs", 132.91f, 1, 6, "Metal Alcalino") },
        { 56, new Element("Bario", "Ba", 137.33f, 2, 6, "Metal Alcalinotérreo") },
        { 57, new Element("Lantano", "La", 138.91f, 3, 6, "Lantánido") },
        { 58, new Element("Cerio", "Ce", 140.12f, 4, 6, "Lantánido") },
        { 59, new Element("Praseodimio", "Pr", 140.91f, 5, 6, "Lantánido") },
        { 60, new Element("Neodimio", "Nd", 144.24f, 6, 6, "Lantánido") },
        { 61, new Element("Prometio", "Pm", 145.00f, 7, 6, "Lantánido") },
        { 62, new Element("Samario", "Sm", 150.36f, 8, 6, "Lantánido") },
        { 63, new Element("Europio", "Eu", 151.96f, 9, 6, "Lantánido") },
        { 64, new Element("Gadolinio", "Gd", 157.25f, 10, 6, "Lantánido") },
        { 65, new Element("Terbio", "Tb", 158.93f, 11, 6, "Lantánido") },
        { 66, new Element("Disprosio", "Dy", 162.50f, 12, 6, "Lantánido") },
        { 67, new Element("Holmio", "Ho", 164.93f, 13, 6, "Lantánido") },
        { 68, new Element("Erbio", "Er", 167.26f, 14, 6, "Lantánido") },
        { 69, new Element("Tulio", "Tm", 168.93f, 15, 6, "Lantánido") },
        { 70, new Element("Iterbio", "Yb", 173.04f, 16, 6, "Lantánido") },
        { 71, new Element("Lutecio", "Lu", 174.97f, 17, 6, "Lantánido") },
        { 72, new Element("Hafnio", "Hf", 178.49f, 4, 6, "Metal de Transición") },
        { 73, new Element("Tántalo", "Ta", 180.95f, 5, 6, "Metal de Transición") },
        { 74, new Element("Wolframio", "W", 183.84f, 6, 6, "Metal de Transición") },
        { 75, new Element("Renio", "Re", 186.21f, 7, 6, "Metal de Transición") },
        { 76, new Element("Osmio", "Os", 190.23f, 8, 6, "Metal de Transición") },
        { 77, new Element("Iridio", "Ir", 192.22f, 9, 6, "Metal de Transición") },
        { 78, new Element("Platino", "Pt", 195.08f, 10, 6, "Metal de Transición") },
        { 79, new Element("Oro", "Au", 196.97f, 11, 6, "Metal de Transición") },
        { 80, new Element("Mercurio", "Hg", 200.59f, 12, 6, "Metal de Transición") },
        { 81, new Element("Talio", "Tl", 204.38f, 13, 6, "Metal") },
        { 82, new Element("Plomo", "Pb", 207.2f, 14, 6, "Metal") },
        { 83, new Element("Bismuto", "Bi", 208.98f, 15, 6, "Metal") },
        { 84, new Element("Polonio", "Po", 209.00f, 16, 6, "Metaloide") },
        { 85, new Element("Astato", "At", 210.00f, 17, 6, "Halógeno") },
        { 86, new Element("Radón", "Rn", 222.00f, 18, 6, "Gas Noble") },
        { 87, new Element("Francio", "Fr", 223.00f, 1, 7, "Metal Alcalino") },
        { 88, new Element("Radio", "Ra", 226.00f, 2, 7, "Metal Alcalinotérreo") },
        { 89, new Element("Actinio", "Ac", 227.00f, 3, 7, "Actínido") },
        { 90, new Element("Torio", "Th", 232.04f, 4, 7, "Actínido") },
        { 91, new Element("Protactinio", "Pa", 231.04f, 5, 7, "Actínido") },
        { 92, new Element("Uranio", "U", 238.03f, 6, 7, "Actínido") },
        { 93, new Element("Neptunio", "Np", 237.00f, 7, 7, "Actínido") },
        { 94, new Element("Plutonio", "Pu", 244.00f, 8, 7, "Actínido") },
        { 95, new Element("Americio", "Am", 243.00f, 9, 7, "Actínido") },
        { 96, new Element("Curio", "Cm", 247.00f, 10, 7, "Actínido") },
        { 97, new Element("Berkelio", "Bk", 247.00f, 11, 7, "Actínido") },
        { 98, new Element("Californio", "Cf", 251.00f, 12, 7, "Actínido") },
        { 99, new Element("Einsteinio", "Es", 252.00f, 13, 7, "Actínido") },
        { 100, new Element("Fermio", "Fm", 257.00f, 14, 7, "Actínido") },
        { 101, new Element("Mendelevio", "Md", 258.00f, 15, 7, "Actínido") },
        { 102, new Element("Nobelio", "No", 259.00f, 16, 7, "Actínido") },
        { 103, new Element("Lawrencio", "Lr", 262.00f, 17, 7, "Actínido") },
        { 104, new Element("Rutherfordio", "Rf", 267.00f, 4, 7, "Metal de Transición") },
        { 105, new Element("Dubnio", "Db", 270.00f, 5, 7, "Metal de Transición") },
        { 106, new Element("Seaborgio", "Sg", 271.00f, 6, 7, "Metal de Transición") },
        { 107, new Element("Bohrio", "Bh", 270.00f, 7, 7, "Metal de Transición") },
        { 108, new Element("Hassio", "Hs", 277.00f, 8, 7, "Metal de Transición") },
        { 109, new Element("Meitnerio", "Mt", 278.00f, 9, 7, "Metal de Transición") },
        { 110, new Element("Darmstadtio", "Ds", 281.00f, 10, 7, "Metal de Transición") },
        { 111, new Element("Roentgenio", "Rg", 282.00f, 11, 7, "Metal de Transición") },
        { 112, new Element("Copernicio", "Cn", 285.00f, 12, 7, "Metal de Transición") },
        { 113, new Element("Nihonio", "Nh", 286.00f, 13, 7, "Metal") },
        { 114, new Element("Flerovio", "Fl", 289.00f, 14, 7, "Metal") },
        { 115, new Element("Moscovio", "Mc", 290.00f, 15, 7, "Metal") },
        { 116, new Element("Livermorio", "Lv", 293.00f, 16, 7, "Metal") },
        { 117, new Element("Tenesino", "Ts", 294.00f, 17, 7, "Halógeno") },
        { 118, new Element("Oganesón", "Og", 294.00f, 18, 7, "Gas Noble") }
    };
    }
void InitializeCustomPositions()
{
    customElementPositions = new Dictionary<int, Vector2>
    {
        // Primera fila
        { 1, new Vector2(0, 0) },   // Hidrógeno
        { 2, new Vector2(17, 0) },  // Helio

        // Segunda fila
        { 3, new Vector2(0, -1) },  // Litio
        { 4, new Vector2(1, -1) },  // Berilio
        { 5, new Vector2(12, -1) }, // Boro
        { 6, new Vector2(13, -1) }, // Carbono
        { 7, new Vector2(14, -1) }, // Nitrógeno
        { 8, new Vector2(15, -1) }, // Oxígeno
        { 9, new Vector2(16, -1) }, // Flúor
        { 10, new Vector2(17, -1) },// Neón

        // Tercera fila
        { 11, new Vector2(0, -2) },  // Sodio
        { 12, new Vector2(1, -2) },  // Magnesio
        { 13, new Vector2(12, -2) }, // Aluminio
        { 14, new Vector2(13, -2) }, // Silicio
        { 15, new Vector2(14, -2) }, // Fósforo
        { 16, new Vector2(15, -2) }, // Azufre
        { 17, new Vector2(16, -2) }, // Cloro
        { 18, new Vector2(17, -2) }, // Argón

        // Cuarta fila
        { 19, new Vector2(0, -3) },  // Potasio
        { 20, new Vector2(1, -3) },  // Calcio
        { 21, new Vector2(2, -3) },  // Escandio
        { 22, new Vector2(3, -3) },  // Titanio
        { 23, new Vector2(4, -3) },  // Vanadio
        { 24, new Vector2(5, -3) },  // Cromo
        { 25, new Vector2(6, -3) },  // Manganeso
        { 26, new Vector2(7, -3) },  // Hierro
        { 27, new Vector2(8, -3) },  // Cobalto
        { 28, new Vector2(9, -3) },  // Níquel
        { 29, new Vector2(10, -3) }, // Cobre
        { 30, new Vector2(11, -3) }, // Zinc
        { 31, new Vector2(12, -3) }, // Galio
        { 32, new Vector2(13, -3) }, // Germanio
        { 33, new Vector2(14, -3) }, // Arsénico
        { 34, new Vector2(15, -3) }, // Selenio
        { 35, new Vector2(16, -3) }, // Bromo
        { 36, new Vector2(17, -3) }, // Kriptón

        // Quinta fila
        { 37, new Vector2(0, -4) },  // Rubidio
        { 38, new Vector2(1, -4) },  // Estroncio
        { 39, new Vector2(2, -4) },  // Itrio
        { 40, new Vector2(3, -4) },  // Zirconio
        { 41, new Vector2(4, -4) },  // Niobio
        { 42, new Vector2(5, -4) },  // Molibdeno
        { 43, new Vector2(6, -4) },  // Tecnecio
        { 44, new Vector2(7, -4) },  // Rutenio
        { 45, new Vector2(8, -4) },  // Rodio
        { 46, new Vector2(9, -4) },  // Paladio
        { 47, new Vector2(10, -4) }, // Plata
        { 48, new Vector2(11, -4) }, // Cadmio
        { 49, new Vector2(12, -4) }, // Indio
        { 50, new Vector2(13, -4) }, // Estaño
        { 51, new Vector2(14, -4) }, // Antimonio
        { 52, new Vector2(15, -4) }, // Telurio
        { 53, new Vector2(16, -4) }, // Yodo
        { 54, new Vector2(17, -4) }, // Xenón

        // Sexta fila
        { 55, new Vector2(0, -5) },  // Cesio
        { 56, new Vector2(1, -5) },  // Bario
        { 57, new Vector2(2, -7) },  // Lantano (Lantánidos)
        { 72, new Vector2(3, -5) },  // Hafnio
        { 73, new Vector2(4, -5) },  // Tantalio
        { 74, new Vector2(5, -5) },  // Wolframio
        { 75, new Vector2(6, -5) },  // Renio
        { 76, new Vector2(7, -5) },  // Osmio
        { 77, new Vector2(8, -5) },  // Iridio
        { 78, new Vector2(9, -5) },  // Platino
        { 79, new Vector2(10, -5) }, // Oro
        { 80, new Vector2(11, -5) }, // Mercurio
        { 81, new Vector2(12, -5) }, // Talio
        { 82, new Vector2(13, -5) }, // Plomo
        { 83, new Vector2(14, -5) }, // Bismuto
        { 84, new Vector2(15, -5) }, // Polonio
        { 85, new Vector2(16, -5) }, // Astato
        { 86, new Vector2(17, -5) }, // Radón

        // Séptima fila
        { 87, new Vector2(0, -6) },  // Francio
        { 88, new Vector2(1, -6) },  // Radio
        { 89, new Vector2(2, -8) },  // Actinio (Actínidos)
        { 104, new Vector2(3, -6) }, // Rutherfordio
        { 105, new Vector2(4, -6) }, // Dubnio
        { 106, new Vector2(5, -6) }, // Seaborgio
        { 107, new Vector2(6, -6) }, // Bohrio
        { 108, new Vector2(7, -6) }, // Hassio
        { 109, new Vector2(8, -6) }, // Meitnerio
        { 110, new Vector2(9, -6) }, // Darmstadio
        { 111, new Vector2(10, -6) },// Roentgenio
        { 112, new Vector2(11, -6) },// Copernicio
        { 113, new Vector2(12, -6) },// Nihonio
        { 114, new Vector2(13, -6) },// Flerovio
        { 115, new Vector2(14, -6) },// Moscovio
        { 116, new Vector2(15, -6) },// Livermorio
        { 117, new Vector2(16, -6) },// Tenesino
        { 118, new Vector2(17, -6) },// Oganesón

        // Lantánidos (Sexta fila)
        { 58, new Vector2(3, -7) },  // Cerio
        { 59, new Vector2(4, -7) },  // Praseodimio
        { 60, new Vector2(5, -7) },  // Neodimio
        { 61, new Vector2(6, -7) },  // Prometio
        { 62, new Vector2(7, -7) },  // Samario
        { 63, new Vector2(8, -7) },  // Europio
        { 64, new Vector2(9, -7) },  // Gadolinio
        { 65, new Vector2(10, -7) }, // Terbio
        { 66, new Vector2(11, -7) }, // Disprosio
        { 67, new Vector2(12, -7) }, // Holmio
        { 68, new Vector2(13, -7) }, // Erbio
        { 69, new Vector2(14, -7) }, // Tulio
        { 70, new Vector2(15, -7) }, // Iterbio
        { 71, new Vector2(16, -7) }, // Lutecio

        // Actínidos (Séptima fila)
        { 90, new Vector2(3, -8) },  // Torio
        { 91, new Vector2(4, -8) },  // Protactinio
        { 92, new Vector2(5, -8) },  // Uranio
        { 93, new Vector2(6, -8) },  // Neptunio
        { 94, new Vector2(7, -8) },  // Plutonio
        { 95, new Vector2(8, -8) },  // Americio
        { 96, new Vector2(9, -8) },  // Curio
        { 97, new Vector2(10, -8) }, // Berkelio
        { 98, new Vector2(11, -8) }, // Californio
        { 99, new Vector2(12, -8) }, // Einstenio
        { 100, new Vector2(13, -8) },// Fermio
        { 101, new Vector2(14, -8) },// Mendelevio
        { 102, new Vector2(15, -8) },// Nobelio
        { 103, new Vector2(16, -8) } // Lawrencio
    };
}


void CreatePeriodicTable()
{
    // Desactivar el prefab original
    tableElementPrefab.SetActive(false);

    // Obtener las dimensiones del prefab para calcular el desplazamiento
    RectTransform prefabRect = tableElementPrefab.GetComponent<RectTransform>();
    Vector2 elementSize = new Vector2(prefabRect.rect.width, prefabRect.rect.height);

    foreach (KeyValuePair<int, Element> entry in elements)
    {
        Element element = entry.Value;
        int atomicNumber = entry.Key;

        // Crear instancia del prefab
        GameObject elementGO = Instantiate(tableElementPrefab, gridParent);
        // Asegurar que la instancia esté activa
        elementGO.SetActive(true);

        // Obtener la posición personalizada del elemento
        Vector2 customPosition = customElementPositions[atomicNumber];

        // Calcular la posición final considerando el tamaño del elemento y el espaciado
        Vector2 finalPosition = new Vector2(
            customPosition.x * (elementSize.x + gridSpacing.x),
            customPosition.y * (elementSize.y + gridSpacing.y)
        );

        // Posicionar el elemento en la grilla usando RectTransform
        RectTransform rt = elementGO.GetComponent<RectTransform>();
        rt.anchoredPosition = finalPosition;

        // Configurar el texto TMP para el símbolo del elemento
        TMP_Text symbolText = elementGO.GetComponentInChildren<TMP_Text>();
        if (symbolText != null)
        {
            symbolText.text = element.Symbol;
        }

        // Configurar el texto y panel de información
        Transform panel = elementGO.transform.Find("Panel");
        if (panel != null)
        {
            // Obtener el componente de Image del panel
            Image panelImage = panel.GetComponent<Image>();
            
            // Cambiar el color del panel basado en el tipo de elemento
            if (panelImage != null)
            {
                SetPanelColor(panelImage, element.Type);

                // Cambiar el color del Trapezoid al mismo color que el panel
                Transform trapezoid = elementGO.transform.Find("Trapezoid");
                if (trapezoid != null)
                {
                    MeshRenderer trapezoidRenderer = trapezoid.GetComponent<MeshRenderer>();
                    if (trapezoidRenderer != null)
                    {
                        // Crear un nuevo material basado en el material original
                        Material newMaterial = new Material(trapezoidRenderer.material);
                        newMaterial.color = panelImage.color; // Cambiar el color del material
                        trapezoidRenderer.material = newMaterial; // Asignar el nuevo material al MeshRenderer
                    }
                }
            }

            // Configurar el texto de información en el panel
            TMP_Text panelText = panel.GetComponentInChildren<TMP_Text>();
            if (panelText != null)
            {
                panelText.text = $"{element.Symbol}\n{element.AtomicMass}";
            }
        }

        // Agregar eventos de UI para hover y clic
        AddUIEvents(elementGO, atomicNumber);
    }
}


// Método para establecer el color del panel basado en el tipo de elemento
private void SetPanelColor(Image panelImage, string elementType)
{
    //ACTUALICE LOS COLORES A LA TABLA DEL ASSET PARA CONSISTENCIA :)
    Dictionary<string, Color> elementTypeColors = new Dictionary<string, Color>
    {
        { "Metal Alcalino", new Color(0.000f, 0.000f, 0.502f) }, 
        { "Metal Alcalinotérreo", new Color(0.800f, 0.400f, 0.000f) },
        { "Lantánido", new Color(0.439f, 0.678f, 0.278f) }, 
        { "Actínido", new Color(0.133f, 0.545f, 0.133f) }, 
        { "Metal de Transición", new Color(0.000f, 0.459f, 0.776f) }, 
        { "Metal", new Color(0.502f, 0.000f, 0.502f) }, 
        { "Metaloide", new Color(0.545f, 0.000f, 0.000f) }, 
        { "No Metal", new Color(0.545f, 0.271f, 0.075f) }, 
        { "Halógeno", new Color(0.576f, 0.655f, 0.059f) }, 
        { "Gas Noble", new Color(0.000f, 0.392f, 0.000f) } 
    };

    if (elementTypeColors.TryGetValue(elementType, out Color color))
    {
        panelImage.color = color;
    }
    else
    {
        panelImage.color = Color.white; // Color por defecto si el tipo no es reconocido
    }
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

            //RectTransform uiTextRectTransform = uiText.GetComponent<RectTransform>();
            //uiTextRectTransform.position = new Vector3(screenPosition.x + 150f, screenPosition.y - 25f, screenPosition.z);
        }
        else
        {
            uiText.text = "Elemento no encontrado.";
        }
    }

    void CreateCore()
    {
        if (orbitElectrons <= 0)
        {
            Debug.LogError("El número de electrones en órbita no es válido.");
            return;
        }

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
    public string Name { get; private set; }
    public string Symbol { get; private set; }
    public float AtomicMass { get; private set; }
    public int Group { get; private set; }
    public int Period { get; private set; }
    public string Type { get; private set; }  // Nuevo campo para el tipo de elemento

    public Element(string name, string symbol, float atomicMass, int group, int period, string type)
    {
        Name = name;
        Symbol = symbol;
        AtomicMass = atomicMass;
        Group = group;
        Period = period;
        Type = type;
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

    void AddUIEvents(GameObject elementGO, int atomicNumber)
    {
        EventTrigger trigger = elementGO.AddComponent<EventTrigger>();

        // Evento de Hover (Pointer Enter)
        EventTrigger.Entry entryHoverEnter = new EventTrigger.Entry();
        entryHoverEnter.eventID = EventTriggerType.PointerEnter;
        entryHoverEnter.callback.AddListener((eventData) => { OnHoverEnter(elementGO); });
        trigger.triggers.Add(entryHoverEnter);

        // Evento de Hover Salida (Pointer Exit)
        EventTrigger.Entry entryHoverExit = new EventTrigger.Entry();
        entryHoverExit.eventID = EventTriggerType.PointerExit;
        entryHoverExit.callback.AddListener((eventData) => { OnHoverExit(elementGO); });
        trigger.triggers.Add(entryHoverExit);

        // Evento de Click Izquierdo
        EventTrigger.Entry entryLeftClick = new EventTrigger.Entry();
        entryLeftClick.eventID = EventTriggerType.PointerClick;
        entryLeftClick.callback.AddListener((eventData) =>
        {
            PointerEventData pointerData = eventData as PointerEventData;
            if (pointerData.button == PointerEventData.InputButton.Left)
            {
                OnLeftClick(elementGO, atomicNumber);
            }
        });
        trigger.triggers.Add(entryLeftClick);
    }

   void OnHoverEnter(GameObject elementGO)
{
    Image image = elementGO.GetComponentInChildren<Image>();
    if (image != null)
    {
        Color color = image.color;
        color.a = 0.3f; // Reducir opacidad
        image.color = color;
    }

    Outline outline = elementGO.GetComponent<Outline>();
    if (outline == null)
    {
        outline = elementGO.AddComponent<Outline>();
        outline.effectColor = Color.yellow;
        outline.effectDistance = new Vector2(5, 5);
    }

    // Aplicar escala animada
    elementGO.transform.localScale = Vector3.one * 1.1f; // Aumentar ligeramente el tamaño
}

void OnHoverExit(GameObject elementGO)
{
    Image image = elementGO.GetComponentInChildren<Image>();
    if (image != null)
    {
        Color color = image.color;
        color.a = 1f; // Restaurar opacidad
        image.color = color;
    }

    Outline outline = elementGO.GetComponent<Outline>();
    if (outline != null)
    {
        Destroy(outline); // Quitar el borde al salir del hover
    }

    // Restaurar escala
    elementGO.transform.localScale = Vector3.one;
}

void OnLeftClick(GameObject elementGO, int atomicNumber)
{
    // Restaurar el último elemento seleccionado si existe
    if (lastSelectedElement != null && lastSelectedElement != elementGO)
    {
        RectTransform rtLast = lastSelectedElement.GetComponent<RectTransform>();
        rtLast.localScale = Vector3.one;
        rtLast.localPosition -= Vector3.forward * 10f;

        Image lastImage = lastSelectedElement.GetComponentInChildren<Image>();
        if (lastImage != null)
        {
            Color lastColor = lastImage.color;
            lastColor.a = 1f; // Restaurar opacidad
            lastImage.color = lastColor;
        }

        Outline lastOutline = lastSelectedElement.GetComponent<Outline>();
        if (lastOutline != null)
        {
            Destroy(lastOutline); // Quitar el borde
        }
    }

    // Aplicar los efectos al elemento actual
    RectTransform rt = elementGO.GetComponent<RectTransform>();
    rt.localScale = Vector3.one * 1.2f;
    rt.localPosition += Vector3.forward * 10f;

    Image image = elementGO.GetComponentInChildren<Image>();
    if (image != null)
    {
        Color color = image.color;
        color.a = 0.3f; // Reducir opacidad
        image.color = color;
    }

    Outline outline = elementGO.GetComponent<Outline>();
    if (outline == null)
    {
        outline = elementGO.AddComponent<Outline>();
        outline.effectColor = Color.yellow;
        outline.effectDistance = new Vector2(5, 5);
    }

    // Actualizar el último elemento seleccionado
    lastSelectedElement = elementGO;

    // Actualizar la visualización de la estructura atómica
    orbitElectrons = atomicNumber;
    ClearPreviousVisuals();
    CreateCore();
    CreateElectronOrbits();
    CreateOrbitingElectrons();
    DisplayElementProperties();
}

    void ClearPreviousVisuals()
    {
        // Destruye todos los hijos bajo el transform de la entidad principal
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

}
