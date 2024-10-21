using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LoadPosition : MonoBehaviour
{
    [System.Serializable]
    public class ConfigData
    {
        public Vector3Data position;
        public Vector3Data scale;
    }

    [System.Serializable]
    public class Vector3Data
    {
        public float x;
        public float y;
        public float z;
    }

    private Vector3 loadedPosition;
    private Vector3 loadedScale;

    void Start()
    {
        LoadConfigFilePos();
        SetTransformValues();
    }

    void LoadConfigFilePos()
    {
        string path = Path.Combine(Application.streamingAssetsPath, "config.json");

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            ConfigData configData = JsonUtility.FromJson<ConfigData>(json);
            loadedPosition = new Vector3(configData.position.x, configData.position.y, configData.position.z);
            loadedScale = new Vector3(configData.scale.x, configData.scale.y, configData.scale.z);

            Debug.Log($"Position loaded: {loadedPosition}");
            Debug.Log($"Scale loaded: {loadedScale}");
        }
        else
        {
            Debug.LogError("Config file not found!");
        }
    }

    void SetTransformValues()
    {
        // Set position and scale to the current GameObject
        transform.position = loadedPosition;
        transform.localScale = loadedScale;
    }
}
