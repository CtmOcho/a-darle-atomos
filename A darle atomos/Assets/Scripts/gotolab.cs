using UnityEngine;
using UnityEngine.SceneManagement;

public class CambiarEscena : MonoBehaviour
{
    public void Goto()
    {
        SceneManager.LoadScene("Laboratorio");
    }
}
