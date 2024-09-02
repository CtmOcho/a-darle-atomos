using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PanelHoverHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Image panelImage;
    private Color originalColor;

    void Start()
    {
        panelImage = GetComponent<Image>();
        if (panelImage != null)
        {
            originalColor = panelImage.color;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (panelImage != null)
        {
            panelImage.color = Color.yellow; // Cambiar color al hacer hover
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (panelImage != null)
        {
            panelImage.color = originalColor; // Restaurar color al salir del hover
        }
    }
}
