using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using Navigation1;
using interfaz = UnityEngine.UI;
using System.IO;


public class Login : MonoBehaviour
{

[System.Serializable]
public class LoginResponse
{
    public string username;
    public string pass;
    public int progreso;
    public string[] curso;
    public string type;
}
    [System.Serializable]
    public class ConfigData
    {
        public string baseBackendUrl;
    }

private string baseBackendUrl;


    void Start()
    {
        LoadConfigFile();
    }

   void LoadConfigFile()
    {
        string path = Path.Combine(Application.streamingAssetsPath, "config.json");

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            ConfigData configData = JsonUtility.FromJson<ConfigData>(json);
            baseBackendUrl = configData.baseBackendUrl;
            Debug.Log($"Backend URL loaded: {baseBackendUrl}");
        }
        else
        {
            Debug.LogError("Config file not found!");
        }
    }

public Navigation1.Navigation navigation;
// Usar la URL base para definir los endpoints
private string authenticationEndpointLog => $"{baseBackendUrl}/login";
private string authenticationEndpointStudent => $"{baseBackendUrl}/student";
private string authenticationEndpointTeacher => $"{baseBackendUrl}/teacher";
private string authenticationEndpointUpdateStudent => $"{baseBackendUrl}/updateStudent";
private string authenticationEndpointGetStudent => $"{baseBackendUrl}/getStudent";

private int progressValue;

[SerializeField] private TMP_InputField usernameInputField;
[SerializeField] private TMP_InputField passwordInputField;
[SerializeField] public string Flag;




public void OnLoginClick()
{
    StartCoroutine(TryLogin());
}


public void OnPutStudentProgress(int progressDetail)
{
    StartCoroutine(TryPutStudentProgress(progressDetail));
}


private IEnumerator TryLogin()
{
    string flag = Flag;
    string username = usernameInputField.text.ToUpper();
    string password = passwordInputField.text;

    // Crear el objeto JSON con las credenciales
    LoginRequest loginRequest = new LoginRequest
    {
        user = username,
        pass = password
    };

    string jsonData = JsonUtility.ToJson(loginRequest);
    string url = $"{authenticationEndpointLog}";

    UnityWebRequest request = new UnityWebRequest(url, "POST");
    byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
    request.uploadHandler = new UploadHandlerRaw(bodyRaw);
    request.downloadHandler = new DownloadHandlerBuffer();
    request.SetRequestHeader("Content-Type", "application/json");
    request.SetRequestHeader("ngrok-skip-browser-warning", "true");  // Añadir el encabezado aquí

    var handler = request.SendWebRequest();

    float startTime = 0.0f;
    while (!handler.isDone)
    {
        startTime += Time.deltaTime;
        if (startTime > 20.0f)
        {
            break;
        }
        yield return null;
    }

    long responseCode = request.responseCode;
    if (request.result == UnityWebRequest.Result.Success)
    {
        if (responseCode == 200)
        {
            string responseText = request.downloadHandler.text;
            LoginResponse loginResponse = JsonUtility.FromJson<LoginResponse>(responseText);

            SessionData.username = loginResponse.username;
            SessionData.progreso = loginResponse.progreso;
            SessionData.curso = new List<string>(loginResponse.curso);
            SessionData.type = loginResponse.type;

            if (loginResponse.type == "E")
            {   
                Debug.Log($"Alumno {username} ingresó al sistema");
                navigation = gameObject.AddComponent<Navigation>();
                navigation.LoadScene("Experiencias_alumnos");
            }
            else
            {
                Debug.Log($"Profesor {username} ingresó al sistema");
                navigation = gameObject.AddComponent<Navigation>();
                navigation.LoadScene("Experiencias_profesores");
            }
        }
        else
        {
            Debug.Log($"Error: {responseCode} Al iniciar sesión");
        }
    }
    else
    {
        Debug.Log($"Error: {responseCode} Al iniciar sesión");
    }

    yield return null;
}

// Clase para estructurar el JSON de la solicitud de login
[System.Serializable]
public class LoginRequest
{
    public string user;
    public string pass;
}


private IEnumerator TryPutStudentProgress(int progressDetail)
{
    string username = SessionData.username;

    // Paso 1: Obtener el valor del progreso en el índice específico
    string getUrl = $"{authenticationEndpointGetStudent}/{username}/prog/{progressDetail}";
    UnityWebRequest request = UnityWebRequest.Get(getUrl);
    request.SetRequestHeader("ngrok-skip-browser-warning", "true");  // Añadir el encabezado aquí

    yield return request.SendWebRequest();

    if (request.result == UnityWebRequest.Result.Success)
    {
        string jsonResponse = request.downloadHandler.text;

        ProgressResponse response = JsonUtility.FromJson<ProgressResponse>(jsonResponse);
        int progressVal = response.progressValue;

        // Paso 2: Si el progreso en el índice es 0, actualizarlo a 1
        if (progressVal == 0)
        {
            string updateUrl = $"{authenticationEndpointUpdateStudent}/{username}/prog/{progressDetail}";
            byte[] body = System.Text.Encoding.UTF8.GetBytes("{}");

            UnityWebRequest updateRequest = UnityWebRequest.Put(updateUrl, body);
            updateRequest.SetRequestHeader("Content-Type", "application/json");
            updateRequest.SetRequestHeader("ngrok-skip-browser-warning", "true");  // Añadir el encabezado aquí

            yield return updateRequest.SendWebRequest();

            if (updateRequest.result == UnityWebRequest.Result.Success)
            {
                Debug.Log($"Progreso en el índice {progressDetail} actualizado a 1");
            }
            else
            {
                Debug.LogError("Error al actualizar el progreso: " + updateRequest.error);
                yield break; // Si falla la actualización, salir del método.
            }
        }
        else
        {
            Debug.Log("Actividad ya fue realizada");
            yield break; // Si el progreso ya es 1, no continuar.
        }
    }
    else
    {
        Debug.LogError("Error al obtener el progreso: " + request.error);
    }
}



public class ProgressResponse
{
    public int progressValue;
}

}