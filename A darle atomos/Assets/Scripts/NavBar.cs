using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NavbarController : MonoBehaviour
{
    // Paneles de la navbar
    public GameObject panel7moBasico;
    public GameObject panel8voBasico;
    public GameObject panel1roMedio;
    public GameObject panel2doMedio;

    // Contenedor de todos los subpaneles
    public GameObject panelsContainer;

    // RawImage del panel padre
    public RawImage rawImage;  // Asume que ya tienes un campo RawImage asignado en el Inspector

    // Tamaños y posiciones de los subpaneles
    public Vector2 defaultSize = new Vector2(300, 400); // Tamaño por defecto de los subpaneles
    public Vector2 enlargedSize = new Vector2(1200, 1000); // Tamaño agrandado de los subpaneles
    public Vector2 centeredPosition = Vector2.zero; // Posición en el centro de la pantalla

    // Tamaño y posición originales del RawImage
    private Vector2 originalRawImageSize;
    private Vector2 originalRawImagePosition;

    // Diccionario para almacenar los subpaneles y sus posiciones originales
    private Dictionary<string, RectTransform> subPanelsDict = new Dictionary<string, RectTransform>();
    private Dictionary<string, Vector2> originalPositions = new Dictionary<string, Vector2>();

    // Variable para rastrear el subpanel actualmente agrandado
    private string currentlyEnlargedPanel = null;

    void Start()
    {
        // Guardar el tamaño y la posición originales del RawImage
        if (rawImage != null)
        {
            RectTransform rawImageRectTransform = rawImage.GetComponent<RectTransform>();
            originalRawImageSize = rawImageRectTransform.sizeDelta;
            originalRawImagePosition = rawImageRectTransform.anchoredPosition;
        }

        // Inicializar el diccionario de subpaneles, guardar posiciones originales y asignar eventos a los botones hijos
        foreach (Transform panel in panelsContainer.transform)
        {
            RectTransform rectTransform = panel.GetComponent<RectTransform>();
            if (rectTransform != null)
            {
                subPanelsDict.Add(panel.name, rectTransform);
                originalPositions.Add(panel.name, rectTransform.anchoredPosition);
                
                // Asignar el ExpandButton para agrandar el panel
                Button expandButton = panel.Find("ExpandButton")?.GetComponent<Button>();
                if (expandButton != null)
                {
                    string panelName = panel.name;
                    expandButton.onClick.AddListener(() => ToggleSubPanel(panelName));
                }

                // Asignar el BackButton para restaurar el panel
                Button backButton = panel.Find("BackButton")?.GetComponent<Button>();
                if (backButton != null)
                {
                    backButton.onClick.AddListener(() => ResetSubPanel(panel.name));
                    backButton.gameObject.SetActive(false); // Inicialmente oculto
                }
            }
        }
    }

    // Métodos para mostrar diferentes paneles de la navbar
    public void Show7moBasicoPanel()
    {
        ActivatePanel(panel7moBasico);
    }

    public void Show8voBasicoPanel()
    {
        ActivatePanel(panel8voBasico);
    }

    public void Show1roMedioPanel()
    {
        ActivatePanel(panel1roMedio);
    }

    public void Show2doMedioPanel()
    {
        ActivatePanel(panel2doMedio);
    }

    // Método genérico para activar un panel y desactivar los demás
    private void ActivatePanel(GameObject panelToActivate)
    {
        // Restablece todos los subpaneles antes de cambiar de panel
        ResetAllSubPanels();

        // Restablecer el RawImage del panel padre
        ResetRawImage();

        panel7moBasico.SetActive(false);
        panel8voBasico.SetActive(false);
        panel1roMedio.SetActive(false);
        panel2doMedio.SetActive(false);

        panelToActivate.SetActive(true);
    }

    // Método para alternar el agrandamiento y centrado de un subpanel, y dividir el texto en los componentes requeridos
    public void ToggleSubPanel(string panelName)
    {
        if (currentlyEnlargedPanel == panelName)
        {
            // Si el subpanel ya está agrandado, volver al tamaño y posición original
            ResetSubPanel(panelName);
        }
        else
        {
            // Restaurar cualquier subpanel previamente agrandado
            if (currentlyEnlargedPanel != null && subPanelsDict.ContainsKey(currentlyEnlargedPanel))
            {
                ResetSubPanel(currentlyEnlargedPanel);
            }

            // Agrandar y centrar el subpanel seleccionado
            if (subPanelsDict.ContainsKey(panelName))
            {
                RectTransform panelRect = subPanelsDict[panelName];
                panelRect.SetAsLastSibling(); // Asegurar que esté en el frente
                panelRect.sizeDelta = enlargedSize;
                panelRect.anchoredPosition = centeredPosition; // Mover al centro de la pantalla

                // Mover la imagen al área inferior izquierda
                Transform imageTransform = panelRect.Find("Image");
                if (imageTransform != null)
                {
                    RectTransform imageRect = imageTransform.GetComponent<RectTransform>();
                    imageRect.sizeDelta = new Vector2(525, 400); // Tamaño ajustado
                    imageRect.anchoredPosition = new Vector2(-468, 15); // Ajustar la posición más abajo
                }

                // Mostrar y ajustar los componentes de texto
                Transform descripcionTitleTransform = panelRect.Find("Descripcion");
                Transform descripcionTextTransform = panelRect.Find("Text");
                Transform objetivosTitleTransform = panelRect.Find("Objetivos");
                Transform textObjetivosTransform = panelRect.Find("TextObjetivo");

                if (descripcionTitleTransform != null && descripcionTextTransform != null)
                {
                    RectTransform descripcionTitleRect = descripcionTitleTransform.GetComponent<RectTransform>();
                    RectTransform descripcionTextRect = descripcionTextTransform.GetComponent<RectTransform>();

                    descripcionTitleRect.anchoredPosition = new Vector2(285, 190); // Posición para el título de la descripción
                    descripcionTextRect.anchoredPosition = new Vector2(342, 135); // Posición para el texto de la descripción

                    descripcionTitleTransform.gameObject.SetActive(true); // Mostrar el título de la descripción
                    descripcionTextTransform.gameObject.SetActive(true); // Mostrar el texto de la descripción
                }

                if (objetivosTitleTransform != null && textObjetivosTransform != null)
                {
                    RectTransform objetivosTitleRect = objetivosTitleTransform.GetComponent<RectTransform>();
                    RectTransform textObjetivosRect = textObjetivosTransform.GetComponent<RectTransform>();

                    objetivosTitleRect.anchoredPosition = new Vector2(285, 15); // Posición para el título de los objetivos
                    textObjetivosRect.anchoredPosition = new Vector2(342, -48); // Posición para el texto de los objetivos

                    objetivosTitleTransform.gameObject.SetActive(true); // Mostrar el título de los objetivos
                    textObjetivosTransform.gameObject.SetActive(true); // Mostrar el texto de los objetivos
                }

                // Mostrar el nuevo botón en el área azul
                Transform newButtonTransform = panelRect.Find("NewButton");
                if (newButtonTransform != null)
                {
                    RectTransform newButtonRect = newButtonTransform.GetComponent<RectTransform>();
                    newButtonRect.anchoredPosition = new Vector2(200, -300); // Ajustar la posición al área inferior derecha
                    newButtonRect.sizeDelta = new Vector2(300, 100); // Tamaño ajustado para el área azul
                    newButtonTransform.gameObject.SetActive(true); // Mostrar el nuevo botón
                }

                // Mostrar el botón de volver en la parte izquierda del nuevo botón
                Transform backButtonTransform = panelRect.Find("BackButton");
                if (backButtonTransform != null)
                {
                    RectTransform backButtonRect = backButtonTransform.GetComponent<RectTransform>();
                    backButtonRect.anchoredPosition = new Vector2(-200, -300); // Ajustar la posición a la izquierda del nuevo botón
                    backButtonRect.sizeDelta = new Vector2(300, 100); // Tamaño ajustado para el back button
                    backButtonTransform.gameObject.SetActive(true); // Mostrar el botón de volver
                }

                // Ocultar el botón de agrandar
                Transform expandButtonTransform = panelRect.Find("ExpandButton");
                if (expandButtonTransform != null)
                {
                    expandButtonTransform.gameObject.SetActive(false); // Ocultar el botón de agrandar
                }

                currentlyEnlargedPanel = panelName;
                //Debug.Log(panelName + " agrandado, con imagen, texto y botones ajustados.");
            }
            else
            {
                //Debug.LogWarning("El subpanel con el nombre " + panelName + " no fue encontrado.");
            }
        }
    }

    // Método para restablecer un subpanel al tamaño, posición, y distribución originales
    private void ResetSubPanel(string panelName)
    {
        if (subPanelsDict.ContainsKey(panelName))
        {
            RectTransform panelRect = subPanelsDict[panelName];
            panelRect.sizeDelta = defaultSize; // Volver al tamaño original
            panelRect.anchoredPosition = originalPositions[panelName]; // Restablecer a la posición original

            // Restaurar la imagen a su tamaño y posición original
            Transform imageTransform = panelRect.Find("Image");
            if (imageTransform != null)
            {
                RectTransform imageRect = imageTransform.GetComponent<RectTransform>();
                imageRect.sizeDelta = new Vector2(525, 400); // Tamaño original de la imagen
                imageRect.anchoredPosition = new Vector2(0,30); // Volver la imagen a su posición original
            }

            // Ocultar los textos y títulos al restablecer el subpanel
            Transform descripcionTitleTransform = panelRect.Find("Descripcion");
            Transform descripcionTextTransform = panelRect.Find("Text");
            Transform objetivosTitleTransform = panelRect.Find("Objetivos");
            Transform textObjetivosTransform = panelRect.Find("TextObjetivo");

            if (descripcionTitleTransform != null)
            {
                descripcionTitleTransform.gameObject.SetActive(false); // Ocultar el título de la descripción
            }
            if (descripcionTextTransform != null)
            {
                descripcionTextTransform.gameObject.SetActive(false); // Ocultar el texto de la descripción
            }
            if (objetivosTitleTransform != null)
            {
                objetivosTitleTransform.gameObject.SetActive(false); // Ocultar el título de los objetivos
            }
            if (textObjetivosTransform != null)
            {
                textObjetivosTransform.gameObject.SetActive(false); // Ocultar el texto de los objetivos
            }

            // Ocultar el nuevo botón
            Transform newButtonTransform = panelRect.Find("NewButton");
            if (newButtonTransform != null)
            {
                newButtonTransform.gameObject.SetActive(false); // Ocultar el nuevo botón
            }

            // Ocultar el botón de volver
            Transform backButtonTransform = panelRect.Find("BackButton");
            if (backButtonTransform != null)
            {
                backButtonTransform.gameObject.SetActive(false); // Ocultar el botón de volver
            }

            // Mostrar el botón de agrandar
            Transform expandButtonTransform = panelRect.Find("ExpandButton");
            if (expandButtonTransform != null)
            {
                expandButtonTransform.gameObject.SetActive(true); // Mostrar el botón de agrandar
            }

            currentlyEnlargedPanel = null;
            //Debug.Log(panelName + " reducido al tamaño, posición, y distribución originales.");
        }
    }

    // Método para restablecer todos los subpaneles al tamaño, posición, y distribución originales
    private void ResetAllSubPanels()
    {
        foreach (var subPanel in subPanelsDict.Values)
        {
            subPanel.sizeDelta = defaultSize;
            subPanel.anchoredPosition = originalPositions[subPanel.name];

            // Restaurar la imagen a su posición original
            Transform imageTransform = subPanel.Find("Image");
            if (imageTransform != null)
            {
                RectTransform imageRect = imageTransform.GetComponent<RectTransform>();
                imageRect.sizeDelta = new Vector2(525, 400); // Tamaño original
                imageRect.anchoredPosition = Vector2.zero; // Volver la imagen a su posición original
            }

            // Ocultar los textos y títulos en todos los subpaneles
            Transform descripcionTitleTransform = subPanel.Find("Descripcion");
            Transform descripcionTextTransform = subPanel.Find("Text");
            Transform objetivosTitleTransform = subPanel.Find("Objetivos");
            Transform textObjetivosTransform = subPanel.Find("TextObjetivo");

            if (descripcionTitleTransform != null)
            {
                descripcionTitleTransform.gameObject.SetActive(false); // Ocultar el título de la descripción
            }
            if (descripcionTextTransform != null)
            {
                descripcionTextTransform.gameObject.SetActive(false); // Ocultar el texto de la descripción
            }
            if (objetivosTitleTransform != null)
            {
                objetivosTitleTransform.gameObject.SetActive(false); // Ocultar el título de los objetivos
            }
            if (textObjetivosTransform != null)
            {
                textObjetivosTransform.gameObject.SetActive(false); // Ocultar el texto de los objetivos
            }

            Transform newButtonTransform = subPanel.Find("NewButton");
            if (newButtonTransform != null)
            {
                newButtonTransform.gameObject.SetActive(false); // Ocultar el nuevo botón
            }

            Transform backButtonTransform = subPanel.Find("BackButton");
            if (backButtonTransform != null)
            {
                backButtonTransform.gameObject.SetActive(false); // Ocultar el botón de volver
            }

            Transform expandButtonTransform = subPanel.Find("ExpandButton");
            if (expandButtonTransform != null)
            {
                expandButtonTransform.gameObject.SetActive(true); // Mostrar el botón de agrandar
            }
        }
        currentlyEnlargedPanel = null; // Resetear la variable de seguimiento

        // Restablecer el RawImage del panel padre
        ResetRawImage();
    }

    // Método para restablecer el RawImage del panel padre
    private void ResetRawImage()
    {
        if (rawImage != null)
        {
            RectTransform rawImageRectTransform = rawImage.GetComponent<RectTransform>();
            rawImageRectTransform.sizeDelta = originalRawImageSize;
            rawImageRectTransform.anchoredPosition = originalRawImagePosition;
            //Debug.Log("RawImage restablecido a tamaño y posición originales.");
        }
    }
}
