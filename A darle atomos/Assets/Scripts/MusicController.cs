using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicController : MonoBehaviour
{
    private static MusicController instance;
    private AudioSource audioSource;

    // Lista de escenas en las que la música debe sonar
    public string[] scenesToPlay;

    void Awake()
    {
        // Si ya existe una instancia de este objeto, la destruimos para evitar duplicados
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Evitamos que se destruya al cambiar de escena
            audioSource = GetComponent<AudioSource>(); // Obtenemos el componente AudioSource
        }
        else
        {
            Destroy(gameObject); // Si ya existe una instancia, destruimos el duplicado
        }
    }

    void OnEnable()
    {
        // Suscribirnos al evento de cambio de escena
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        // Desuscribirnos del evento de cambio de escena
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // Este método se ejecuta cada vez que se carga una escena nueva
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Verificar si la escena actual está en la lista de escenas en las que debe sonar la música
        bool shouldPlayMusic = false;
        foreach (string sceneName in scenesToPlay)
        {
            if (scene.name == sceneName)
            {
                shouldPlayMusic = true; // Si la escena está en la lista, activamos la bandera para reproducir música
                break;
            }
        }

        // Si la música debe sonar en esta escena
        if (shouldPlayMusic)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play(); // Si la música no está sonando, la reproducimos
            }
        }
        else
        {
            // Si no está en la lista de escenas permitidas, detener la música
            if (audioSource.isPlaying)
            {
                audioSource.Stop(); // Detenemos la música si está sonando
            }
        }
    }
}
