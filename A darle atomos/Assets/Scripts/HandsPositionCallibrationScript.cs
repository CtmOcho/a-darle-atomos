using System.Collections;
using System.IO;
using UnityEngine;

public class HandsPositionCalibrationScript : MonoBehaviour
{
    [System.Serializable]
    public class CalibrationData
    {
        public string baseBackendUrl;
        public Vector3 position;
        public Vector3 scale;
    }

    public GameObject targetObject; // Arrastra aquí el GameObject que deseas manipular
    public float adjustmentValue = 0.01f; // Valor ajustable desde el inspector para los cambios de posición y escala

    private CalibrationData calibrationData;
    private string filePath;

    void Start()
    {
        filePath = Path.Combine(Application.streamingAssetsPath, "config.json");
        LoadData();
    }

    void LoadData()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            calibrationData = JsonUtility.FromJson<CalibrationData>(json);
            ApplyData();
        }
        else
        {
            Debug.LogError("No se encontró el archivo JSON de calibración.");
        }
    }

    void ApplyData()
    {
        targetObject.transform.localPosition = calibrationData.position;
        targetObject.transform.localScale = calibrationData.scale;
    }

    void SaveData()
    {
        string json = JsonUtility.ToJson(calibrationData, true);
        File.WriteAllText(filePath, json);
    }

    // Métodos para ajustar la posición
    public void IncreaseXPosition() { 
              AdjustPosition(0, 0, -adjustmentValue); 

        }
    public void DecreaseXPosition() {
        AdjustPosition(0, 0, adjustmentValue); 
         

        }
    public void IncreaseYPosition() {
        AdjustPosition(adjustmentValue, 0, 0); 
        


         }
    public void DecreaseYPosition() {
        AdjustPosition(-adjustmentValue, 0, 0);
        

         }

    public void IncreaseZPosition() {
         AdjustPosition(0, adjustmentValue, 0); 
         

         }
    public void DecreaseZPosition() {
         AdjustPosition(0, -adjustmentValue, 0); 

   

         }

    // Métodos para ajustar la escala
    public void IncreaseXScale() { AdjustScale(adjustmentValue, 0, 0); }
    public void DecreaseXScale() { AdjustScale(-adjustmentValue, 0, 0); }
    public void IncreaseYScale() { AdjustScale(0, adjustmentValue, 0); }
    public void DecreaseYScale() { AdjustScale(0, -adjustmentValue, 0); }
    public void IncreaseZScale() { AdjustScale(0, 0, adjustmentValue); }
    public void DecreaseZScale() { AdjustScale(0, 0, -adjustmentValue); }

    private void AdjustPosition(float x, float y, float z)
    {
        calibrationData.position += new Vector3(x, y, z);
        ApplyData();
        SaveData();
    }

    private void AdjustScale(float x, float y, float z)
    {
        calibrationData.scale += new Vector3(x, y, z);
        ApplyData();
        SaveData();
    }
}
