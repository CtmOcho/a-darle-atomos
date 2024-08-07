using UnityEngine;
using UnityEngine.SceneManagement;

public class CambiarEscena : MonoBehaviour
{

    public GameObject cam;
    public void Goto()
    {
        cam.GetComponent<Login>().OnPutStudentProgress();
        
        SceneManager.LoadScene("Laboratorio");
    }
}
