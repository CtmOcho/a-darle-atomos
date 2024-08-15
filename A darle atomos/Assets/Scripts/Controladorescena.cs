using UnityEngine;

public class ControladorEscena : MonoBehaviour
{
    public GameObject Subexperience; // Asume que quieres desactivar este Canvas


    void Start()
    {
        ResetScene();
    }

    public void ResetScene()
    {
        // Desactiva el Canvas de Subexperience si está activo
        if (Subexperience != null && Subexperience.activeSelf)
        {
            Subexperience.SetActive(false);
        }

    }
}
