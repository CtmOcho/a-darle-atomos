using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using Navigation1;
using interfaz = UnityEngine.UI;



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


private string baseBackendUrl = "http://localhost:13756"; // Puedes cambiar esta URL cuando cambie la URL de Ngrok

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
    string url = $"{authenticationEndpointLog}/{username}/{password}";

    UnityWebRequest request = UnityWebRequest.Get(url);
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
        //Debug.Log(responseCode);
        if (responseCode == 200)
        {
            string responseText = request.downloadHandler.text;
            LoginResponse loginResponse = JsonUtility.FromJson<LoginResponse>(responseText);

            SessionData.username = loginResponse.username;
            SessionData.progreso = loginResponse.progreso;
            SessionData.curso = new List<string>(loginResponse.curso);
            //Debug.Log(SessionData.curso);// Convertir array a lista
            SessionData.type = loginResponse.type;
            if (loginResponse.type == "E")
            {   
                Debug.Log($"Alumno {username} ingresó al sistema");


                //Debug.Log($"{username}:{password}");

                navigation = gameObject.AddComponent<Navigation>();

                navigation.LoadScene("Experiencias_alumnos");

            }
            else
            {

                //Debug.Log($"{username}:{password}");

                Debug.Log("Profesor {username} ingresó al sistema");

                navigation = gameObject.AddComponent<Navigation>();

                navigation.LoadScene("Experiencia_profesores");

            }


        }



    }
    else
    {
        Debug.Log($"Error: {responseCode} Al iniciar sesión");
    }

    //Debug.Log($"{username}:{password}");

    yield return null;
}

private IEnumerator TryPutStudentProgress(int progressDetail)
{
    string username = SessionData.username;
    //Debug.Log(username);
    //Debug.Log(progressDetail);
    // Paso 1: Obtener el valor del progreso en el índice específico
    string getUrl = $"{authenticationEndpointGetStudent}/{username}/prog/{progressDetail}";
    //Debug.Log(getUrl);
    UnityWebRequest request = UnityWebRequest.Get(getUrl);

    yield return request.SendWebRequest();

    if (request.result == UnityWebRequest.Result.Success)
    {
        string jsonResponse = request.downloadHandler.text;
        //Debug.Log("Respuesta JSON recibida: " + jsonResponse);

        ProgressResponse response = JsonUtility.FromJson<ProgressResponse>(jsonResponse);
        int progressVal = response.progressValue;
        //Debug.Log("Valor del progreso obtenido: " + progressVal);

        // Paso 2: Si el progreso en el índice es 0, actualizarlo a 1
        if (progressVal == 0)
        {
            string updateUrl = $"{authenticationEndpointUpdateStudent}/{username}/prog/{progressDetail}";
            byte[] body = System.Text.Encoding.UTF8.GetBytes("{}");
            //Debug.Log("PUT URL para actualizar progreso: " + updateUrl);

            UnityWebRequest updateRequest = UnityWebRequest.Put(updateUrl, body);
            updateRequest.SetRequestHeader("Content-Type", "application/json");

            //Debug.Log("Antes de enviar solicitud PUT...");

            yield return updateRequest.SendWebRequest();

            //Debug.Log("Después de enviar solicitud PUT...");
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