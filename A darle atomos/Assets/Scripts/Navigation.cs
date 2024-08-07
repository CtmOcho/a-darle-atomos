using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

namespace Navigation1{

    public class Navigation : MonoBehaviour
        
        {

            public GameObject cam;
        public void LoadScene(string sceneName)
            {
            // Guardar el nombre de la escena actual en PlayerPrefs antes de cambiar
            PlayerPrefs.SetString("LastScene", SceneManager.GetActiveScene().name);
            PlayerPrefs.Save();
            SceneManager.LoadScene(sceneName);
            }

        public string GetLastSceneName()
            {
            // Obtener el nombre de la última escena desde PlayerPrefs
            return PlayerPrefs.GetString("LastScene", ""); // Retorna una cadena vacía si no hay valor guardado
            }


 public void SendToDatabase(){
        cam.GetComponent<Login>().OnPutStudentProgress();
    }
        public void BackFromLab(){
            
            if (SessionData.type == "E"){
                SceneManager.LoadScene("Experiencias_alumnos");
            }else{
                SceneManager.LoadScene("Experiencia_profesores");
            }
        }
        }  

}


