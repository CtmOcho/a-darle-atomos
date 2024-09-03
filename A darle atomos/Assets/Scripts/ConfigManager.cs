using System.IO;
using UnityEngine;

public class ConfigManager : MonoBehaviour
{
    public static ConfigManager Instance { get; private set; }

    public string BaseBackendUrl { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // No destruir este GameObject al cambiar de escena
            LoadConfig();
        }
        else
        {
            Destroy(gameObject);  // Destruir instancias duplicadas
        }
    }

    private void LoadConfig()
    {
        string configPath = Path.Combine(Application.streamingAssetsPath, "config.json");
        if (File.Exists(configPath))
        {
            string jsonText = File.ReadAllText(configPath);
            ConfigData configData = JsonUtility.FromJson<ConfigData>(jsonText);

            BaseBackendUrl = configData.baseBackendUrl;
        }
        else
        {
            Debug.LogError("Config file not found!");
        }
    }

    [System.Serializable]
    public class ConfigData
    {
        public string baseBackendUrl;
    }
}
