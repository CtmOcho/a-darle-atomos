using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;

public class Login : MonoBehaviour
{

    [SerializeField] private string authenticationEndpointLog = "http://localhost:13756/login";
    [SerializeField] private string authenticationEndpointStudent = "http://localhost:13756/student";
    [SerializeField] private string authenticationEndpointTeacher = "http://localhost:13756/teacher";
    [SerializeField] private TMP_InputField usernameInputField;
    [SerializeField] private TMP_InputField passwordInputField;

    
    public void OnLoginClick(){
        StartCoroutine(TryLogin());
    }
    /*
    public void OnCreateStudentClick(){
        StartCoroutine(TryCreateStudent());
    }
    public void OnCreateTeacherClick(){
        StartCoroutine(TryCreateTeacher());
    }

    public void OnDeleteStudentClick(){
        StartCoroutine(TryDeleteStudent());
    }
    */

    private IEnumerator TryLogin(){
        
        string username = usernameInputField.text;    
        string password = passwordInputField.text;

        UnityWebRequest request = UnityWebRequest.Get($"{authenticationEndpointLog}?user={username}&pass={password}");
        var handler = request.SendWebRequest();

        float startTime = 0.0f;
        while(!handler.isDone){
            startTime += Time.deltaTime;
            if(startTime > 10.0f){
                break;
            }
            yield return null;
        }

        if(request.result == UnityWebRequest.Result.Success){

            Debug.Log(request.downloadHandler.text);
        }else{
            Debug.Log("No se puedo conectar al servidor");
        }

        Debug.Log($"{username}:{password}");

        yield return null;
    }

    /*
    private IEnumerator TryCreateStudent(){
        string username = usernameInputField.text;    
        string password = passwordInputField.text;

        UnityWebRequest request = UnityWebRequest.Post($"{authenticationEndpointStudent}?user={username}&pass={password}");
        var handler = request.SendWebRequest();

        float startTime = 0.0f;
        while(!handler.isDone){
            startTime += Time.deltaTime;
            if(startTime > 10.0f){
                break;
            }
            yield return null;
        }

        if(request.result == UnityWebRequest.Result.Success){

            Debug.Log(request.downloadHandler.text);
        }else{
            Debug.Log("No se puedo conectar al servidor");
        }
    }

    private IEnumerator TryCreateTeacher(){
        string username = usernameInputField.text;    
        string password = passwordInputField.text;

        UnityWebRequest request = UnityWebRequest.Post($"{authenticationEndpointTeacher}?user={username}&pass={password}");
        var handler = request.SendWebRequest();

        float startTime = 0.0f;
        while(!handler.isDone){
            startTime += Time.deltaTime;
            if(startTime > 10.0f){
                break;
            }
            yield return null;
        }

        if(request.result == UnityWebRequest.Result.Success){

            Debug.Log(request.downloadHandler.text);
        }else{
            Debug.Log("No se puedo conectar al servidor");
        }
    }

    private IEnumerator TryDeleteTeacher(){
        string username = usernameInputField.text;    
        string password = passwordInputField.text;

        UnityWebRequest request = UnityWebRequest.Delete($"{authenticationEndpointTeacher}/{username}");
        var handler = request.SendWebRequest();

        float startTime = 0.0f;
        while(!handler.isDone){
            startTime += Time.deltaTime;
            if(startTime > 10.0f){
                break;
            }
            yield return null;
        }

        if(request.result == UnityWebRequest.Result.Success){

            Debug.Log(request.downloadHandler.text);
        }else{
            Debug.Log("No se puedo conectar al servidor");
        }
    }
    */
}

