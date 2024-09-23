using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

namespace Navigation1
{
    public class Navigation : MonoBehaviour
    {

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


        public void BackFromLab()
        {
            // Verificar si el tiempo está pausado y reanudarlo si es necesario
            if (Time.timeScale == 0f)
            {
                Time.timeScale = 1f; // Reanudar el tiempo
                Debug.Log("Tiempo reanudado.");
            }

            // Verificar si es alumno o profesor y cargar la escena correspondiente
            if (SessionData.type == "E")
            {
                SceneManager.LoadScene("Experiencias_alumnos");
            }
            else
            {
                SceneManager.LoadScene("Experiencias_profesores");
            }
        }

        public void GoToPreviousScene()
        {
            // Obtener el nombre de la última escena desde PlayerPrefs
            string lastScene = PlayerPrefs.GetString("LastScene", "");

            if (!string.IsNullOrEmpty(lastScene))
            {
                // Cargar la escena anterior si existe
                SceneManager.LoadScene(lastScene);
            }
            else
            {
                Debug.LogWarning("No hay una escena anterior guardada en PlayerPrefs.");
            }
        }
    }
}
