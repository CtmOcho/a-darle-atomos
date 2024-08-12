using UnityEngine;
using UnityEngine.SceneManagement;

public class CambiarEscena : MonoBehaviour
{

    public GameObject cam;
    public void Goto()
    {
        cam.GetComponent<Login>().OnPutStudentProgress(2);
        
        SceneManager.LoadScene("Laboratorio");
    }
}
