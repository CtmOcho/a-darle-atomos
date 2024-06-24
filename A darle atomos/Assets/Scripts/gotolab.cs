using UnityEngine;
using UnityEngine.SceneManagement;

public class CambiarEscena : MonoBehaviour
{

    public GameObject camera;
    public void Goto()
    {
        camera.GetComponent<Login>().OnPutStudentProgress();
        
        SceneManager.LoadScene("Laboratorio");
    }
}
