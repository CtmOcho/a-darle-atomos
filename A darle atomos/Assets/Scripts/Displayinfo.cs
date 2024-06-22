using UnityEngine;
using TMPro; // Asegúrate de incluir el namespace de TextMesh Pro

public enum SessionDataField
{
    Username,
    Progreso,
    Curso

}






public class DisplaySessionData : MonoBehaviour
{
    public TMP_Text displayText; // Campo para el componente TMP_Text
    public SessionDataField fieldToDisplay; // Campo para seleccionar qué propiedad mostrar

    void Start()
    {
        if (displayText != null)
        {
            string textToDisplay = GetSessionDataField(fieldToDisplay);
            displayText.text = textToDisplay;
        }
        else
        {
            Debug.LogError("Text reference is not set.");
        }
    }

    private string GetSessionDataField(SessionDataField field)
    {
        switch (field)
        {
            case SessionDataField.Username:
                return SessionData.username;
            case SessionDataField.Progreso:
                return SessionData.progreso.ToString();
            case SessionDataField.Curso:    
                return SessionData.curso[0];
                
            default:
                return "Unknown Field";
        }
    }
}