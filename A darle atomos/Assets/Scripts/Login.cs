using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using Navigation1;



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
    public class Curso
    {
        public string course;
        public string teacher;
        public string[] students;
    }

    public Navigation1.Navigation navigation;
    [SerializeField] private string authenticationEndpointLog = "http://localhost:13756/login";
    [SerializeField] private string authenticationEndpointStudent = "http://localhost:13756/student";
    [SerializeField] private string authenticationEndpointTeacher = "http://localhost:13756/teacher";
    [SerializeField] private string authenticationEndpointCourses = "http://localhost:13756/curso";
    [SerializeField] private string authenticationEndpointUpdateCourses = "http://localhost:13756/updateCurso";
    [SerializeField] private string authenticationEndpointUpdateStudent = "http://localhost:13756/updateStudent";
    [SerializeField] private TMP_InputField usernameInputField;
    [SerializeField] private TMP_InputField passwordInputField;
    [SerializeField] private TMP_InputField cursoInputField;
    [SerializeField] private TMP_InputField studentsInputField;
    [SerializeField] private string Flag;

    
    public void OnLoginClick(){
        StartCoroutine(TryLogin());
    }
    
    public void OnCreateStudentClick(){
        StartCoroutine(TryCreateStudent());
    }
    
    public void OnCreateTeacherClick(){
        StartCoroutine(TryCreateTeacher());
    }

    public void OnCreateCourseClick(){
        StartCoroutine(TryCreateCourse());
    }

    public void OnDeleteStudentClick(){
        StartCoroutine(TryDeleteStudent());
    }
    
    public void OnRemoveFromCursoClick(){
        string inputText = studentsInputField.text;
        string[] studentsToRemove = inputText.Split(',');
        StartCoroutine(TryRemoveFromCurso(studentsToRemove));
    }
    public void OnInsertInCursoClick(){
        string inputText = studentsInputField.text;
        string[] studentsToInsert = inputText.Split(',');

        StartCoroutine(TryInsertInCurso(studentsToInsert));
    }
    public void OnUpdateStudentClick(){
        StartCoroutine(TryUpdateStudent());
    }

    public void OnGetStudentsFromCursoClick(){
        StartCoroutine(TryGetStudentsFromCurso());
    }

    public void OnPutStudentProgress(){
        StartCoroutine(TryPutStudentProgress());
    }

    private IEnumerator TryLogin(){
        
        string flag = Flag;
        string username = usernameInputField.text;    
        string password = passwordInputField.text;
        string url = $"{authenticationEndpointLog}/{username}/{password}";

        UnityWebRequest request = UnityWebRequest.Get(url);
        var handler = request.SendWebRequest();

        float startTime = 0.0f;
        while(!handler.isDone){
            startTime += Time.deltaTime;
            if(startTime > 20.0f){
                break;
            }
            yield return null;
        }

        long responseCode = request.responseCode;
        if(request.result == UnityWebRequest.Result.Success){
            Debug.Log(responseCode);
            if(responseCode == 200){
                string responseText = request.downloadHandler.text;
                LoginResponse loginResponse = JsonUtility.FromJson<LoginResponse>(responseText);

                SessionData.username = loginResponse.username;
                SessionData.progreso = loginResponse.progreso;
                SessionData.curso = new List<string>(loginResponse.curso); // Convertir array a lista
                SessionData.type = loginResponse.type;
                if(loginResponse.type == "E"){

                navigation = gameObject.AddComponent<Navigation>();

                navigation.LoadScene("Experiencias_alumnos");
                
                }
                else{

                navigation = gameObject.AddComponent<Navigation>();

                navigation.LoadScene("Post_login_profesor");

                }
            

            }



        }else{
            Debug.Log("No existe el usuario");
        }

        Debug.Log($"{username}:{password}");

        yield return null;
    }

    
    private IEnumerator TryCreateStudent(){
        string username = usernameInputField.text;    
        string password = passwordInputField.text;
        string url = $"{authenticationEndpointStudent}?user={username}&pass={password}";
        

        UnityWebRequest request = UnityWebRequest.PostWwwForm(url, "");
        var handler = request.SendWebRequest();

        float startTime = 0.0f;
        while(!handler.isDone){
            startTime += Time.deltaTime;
            if(startTime > 10.0f){
                break;
            }
            yield return null;
        }

        long responseCode = request.responseCode;
        if(request.result == UnityWebRequest.Result.Success){
            Debug.Log(responseCode);
            if(responseCode == 201){
   
                navigation = gameObject.AddComponent<Navigation>();

                navigation.LoadScene("Login_alumno");
            
            }
            else{
                Debug.Log("Creacion invalida");
            }
            

        }
    }
    private IEnumerator TryCreateCourse(){
        string teacher = "teacher"; //cambiar esto por el username en sesion
        string curso = "curso";//cambiar por el curso que se desea crear

        string url = $"{authenticationEndpointCourses}//{teacher}/{curso}";
        

        UnityWebRequest request = UnityWebRequest.PostWwwForm(url, "");
        var handler = request.SendWebRequest();

        float startTime = 0.0f;
        while(!handler.isDone){
            startTime += Time.deltaTime;
            if(startTime > 10.0f){
                break;
            }
            yield return null;
        }

        long responseCode = request.responseCode; //Devuelve 201 on Success
        if(request.result == UnityWebRequest.Result.Success){
            // Ingresar aca lo que se quiera hacer
        }
    }
    
    private IEnumerator TryCreateTeacher(){
        string username = usernameInputField.text;    
        string password = passwordInputField.text;

        UnityWebRequest request = UnityWebRequest.PostWwwForm($"{authenticationEndpointTeacher}/{username}/{password}", "");
        var handler = request.SendWebRequest();

        float startTime = 0.0f;
        while(!handler.isDone){
            startTime += Time.deltaTime;
            if(startTime > 10.0f){
                break;
            }
            yield return null;
        }

        long responseCode = request.responseCode;
        if(request.result == UnityWebRequest.Result.Success){
            Debug.Log(responseCode);
            if(responseCode == 201){
   
                navigation = gameObject.AddComponent<Navigation>();

                navigation.LoadScene("Login_profesor");
            
            }
            else{
                Debug.Log("Creacion invalida");
            }
            

        }


        else{
            Debug.Log("No se puedo conectar al servidor");
        }



        yield return null;
    }
    
        private IEnumerator TryDeleteStudent(){
            string username = usernameInputField.text;    

            if(SessionData.username == username){
                UnityWebRequest request = UnityWebRequest.Delete($"{authenticationEndpointStudent}/{username}");
                var handler = request.SendWebRequest();

                float startTime = 0.0f;
                while(!handler.isDone){
                    startTime += Time.deltaTime;
                    if(startTime > 10.0f){
                        break;
                    }
                    yield return null;
                }

                long responseCode = request.responseCode;
                if(request.result == UnityWebRequest.Result.Success){
                    Debug.Log(responseCode);
                    if(responseCode == 200){
        
                        navigation = gameObject.AddComponent<Navigation>();

                        navigation.LoadScene("Home1");

                        Debug.Log("Eliminado");
                    
                    }
                    else{
                        Debug.Log("Fallo en la conexion");
                    }     

            }


            else{
                Debug.Log("No se puedo conectar al servidor");
            }



            yield return null;
        }
        
    }
    
    private IEnumerator TryInsertInCurso(string[] students){

        string curso = cursoInputField.text; //curso donde se quieran insertar los alumnos
        var jsonData = new
        {
            students = students
        };
        string json = JsonUtility.ToJson(jsonData);

        UnityWebRequest request = UnityWebRequest.Put($"{authenticationEndpointUpdateCourses}/{curso}/insertStudents",json);
        var handler = request.SendWebRequest();

        float startTime = 0.0f;
        while(!handler.isDone){
            startTime += Time.deltaTime;
            if(startTime > 10.0f){
                break;
            }
            yield return null;
        }

        long responseCode = request.responseCode; // devuelve 200 caso OK
        if(request.result == UnityWebRequest.Result.Success){
           Debug.Log(request.responseCode);
        }else{
            Debug.Log("No se puedo conectar al servidor");
        }

    }

     private IEnumerator TryRemoveFromCurso(string[] students){

        string curso = cursoInputField.text; //curso donde se quieran eliminar los alumnos
        var jsonData = new
        {
            students = students
        };
        string json = JsonUtility.ToJson(jsonData);

        UnityWebRequest request = UnityWebRequest.Put($"{authenticationEndpointUpdateCourses}/{curso}/removeStudents",json);
        var handler = request.SendWebRequest();

        float startTime = 0.0f;
        while(!handler.isDone){
            startTime += Time.deltaTime;
            if(startTime > 10.0f){
                break;
            }
            yield return null;
        }

        long responseCode = request.responseCode; // devuelve 200 caso OK
        if(request.result == UnityWebRequest.Result.Success){
           Debug.Log(request.responseCode);
        }else{
            Debug.Log("No se puedo conectar al servidor");
        }

    }

    private IEnumerator TryUpdateStudent(){
        string username = usernameInputField.text;    
        string password = passwordInputField.text;
        string sesion = "sesion";

        var jsonData = new
        {
            user = username,
            pass = password
        };

        string json = JsonUtility.ToJson(jsonData);

        //Reemplazar {sesion} por el usuario de la sesion
        UnityWebRequest request = UnityWebRequest.Put($"{authenticationEndpointUpdateStudent}/{sesion}",json);
        var handler = request.SendWebRequest();

        float startTime = 0.0f;
        while(!handler.isDone){
            startTime += Time.deltaTime;
            if(startTime > 10.0f){
                break;
            }
            yield return null;
        }

        long responseCode = request.responseCode; // Devuelve 200 caso OK
        if(request.result == UnityWebRequest.Result.Success){
           Debug.Log(request.responseCode);
        }else{
            Debug.Log("No se puedo conectar al servidor");
        }

    }
    
    private IEnumerator TryGetStudentsFromCurso(){

        string curso = cursoInputField.text;
        string url = $"{authenticationEndpointCourses}/students/{curso}";

        UnityWebRequest request = UnityWebRequest.Get(url);
        var handler = request.SendWebRequest();

        float startTime = 0.0f;
        while(!handler.isDone){
            startTime += Time.deltaTime;
            if(startTime > 20.0f){
                break;
            }
            yield return null;
        }

        long responseCode = request.responseCode;
        if(request.result == UnityWebRequest.Result.Success){
            Debug.Log(responseCode);
            if(responseCode == 200){
                string responseText = request.downloadHandler.text;
                Curso Response = JsonUtility.FromJson<Curso>(responseText);
                // 
                //SessionData.curso = new List<string>(Response.curso); // Convertir array a lista
            
            }



        }else{
            Debug.Log("No se puedo conectar al servidor");
        }
        yield return null;
    }


    private IEnumerator TryPutStudentProgress(){
        string username = SessionData.username;    

        var jsonData = new
        {
            user = username,
        };

        string json = JsonUtility.ToJson(jsonData);

        //Reemplazar {sesion} por el usuario de la sesion
        UnityWebRequest request = UnityWebRequest.Put($"{authenticationEndpointUpdateStudent}/{username}/prog",json);
        var handler = request.SendWebRequest();

        float startTime = 0.0f;
        while(!handler.isDone){
            startTime += Time.deltaTime;
            if(startTime > 10.0f){
                break;
            }
            yield return null;
        }

        long responseCode = request.responseCode; // Devuelve 200 caso OK
        Debug.Log(request.responseCode);

        if(responseCode == 200){
           Debug.Log(request.responseCode);
        }else{
            Debug.Log("No se puedo conectar al servidor");
        }
    }
}
