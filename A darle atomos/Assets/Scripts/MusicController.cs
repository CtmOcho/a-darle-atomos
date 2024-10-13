using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicController : MonoBehaviour
{
    private static MusicController instance;
    private AudioSource audioSource;

    // Lista de escenas en las que debe sonar música
    public SceneMusic[] scenesWithMusic;

    [System.Serializable]
    public class SceneMusic
    {
        public string sceneName;      // Nombre de la escena
        public AudioClip musicClip;   // Música que debe sonar en esa escena
    }

    void Awake()
    {
        // Singleton para asegurar que solo haya una instancia de MusicController
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // No destruir al cambiar de escena
            audioSource = GetComponent<AudioSource>(); // Obtenemos el componente AudioSource
        }
        else
        {
            Destroy(gameObject); // Si ya existe una instancia, destruir duplicados
        }
    }

    void OnEnable()
    {
        // Suscribirse al evento de cambio de escena
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        // Desuscribirse del evento de cambio de escena
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // Este método se ejecuta cada vez que se carga una nueva escena
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Revisar si la escena actual tiene una música asociada
        foreach (SceneMusic sceneMusic in scenesWithMusic)
        {
            if (scene.name == sceneMusic.sceneName)
            {
                // Si la música actual es diferente, la cambiamos
                if (audioSource.clip != sceneMusic.musicClip)
                {
                    audioSource.clip = sceneMusic.musicClip; // Asignamos la nueva música
                    audioSource.Play(); // Reproducimos la nueva música
                }
                return;
            }
        }

        // Si no hay música asociada a la escena, detener la música
        audioSource.Stop();
    }
}
