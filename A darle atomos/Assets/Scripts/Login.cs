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

    [System.Serializable]
    public class UpdateResponse
    {
        public string user;
        public string pass;
    }

     [System.Serializable]
    public class Curso
    {
        public string course;
        public string teacher;
        public string[] students;
    }

    [System.Serializable]
    public class UpdateResponseRemove
    {
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
    [SerializeField] private TMP_InputField nivelInputField;
    [SerializeField] private TMP_InputField letraInputField;
    [SerializeField] public string Flag;
    public GameObject progressBar;

    
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

    public void OnDeleteCursoClick(){
        StartCoroutine(TryDeleteCurso());
        
    }

    public void OnGetStudentsFromCursoClick(){
        StartCoroutine(TryGetStudentsFromCurso());
    }

    public void OnPutStudentProgress(){
        StartCoroutine(TryPutStudentProgress());
    }

    public void OnGetStudentProgress(string user){
        StartCoroutine(TryGetStudentProgress(user));
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
                SessionData.curso = new List<string>(loginResponse.curso); 
                Debug.Log(SessionData.curso);// Convertir array a lista
                SessionData.type = loginResponse.type;
                if(loginResponse.type == "E"){

                Debug.Log($"{username}:{password}");

                navigation = gameObject.AddComponent<Navigation>();

                navigation.LoadScene("Experiencias_alumnos");
                
                }
                else{

                Debug.Log($"{username}:{password}");

                Debug.Log("Profesor ingreso al sistema");

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

                //Debug.Log($"Usuario={username} con clave: pass={password} fue creado exitosamente");
                Debug.Log($"Usuario creado exitosamente , codigo: {responseCode}");
                navigation = gameObject.AddComponent<Navigation>();

                navigation.LoadScene("Login_alumno");
            
            }
            else{
                Debug.Log("Creacion invalida");
            }
            
        }
        else{
 
            Debug.Log("Conexion fallida");           
        }
    }
    private IEnumerator TryCreateCourse(){
        string teacher = SessionData.username; //cambiar esto por el username en sesion
        string curso = nivelInputField.text + letraInputField.text;//cambiar por el curso que se desea crear
        Debug.Log(curso);

        string url = $"{authenticationEndpointCourses}/{teacher}/{curso}";

        Debug.Log(url);        

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
            Debug.Log(responseCode);
           if(responseCode == 201){
            SessionData.curso.Add(curso); 
            Debug.Log("Curso creado exitosamente");
            navigation = gameObject.AddComponent<Navigation>();

            navigation.LoadScene("Editor_cursos");
           }
           else{
            Debug.Log("Curso  ya existe");
           }
        }
        else{
            Debug.Log("Fallo la conexion");
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

                Debug.Log("Profesor fue creado exitosamente");
   
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
    
    private IEnumerator TryDeleteStudent()
    {
        string username = usernameInputField.text;
        Debug.Log(SessionData.username);
        Debug.Log(username);


        if (SessionData.username == username)
        {
            Debug.Log(SessionData.type);
            if (SessionData.type == "E")
            {
                UnityWebRequest request = UnityWebRequest.Delete($"{authenticationEndpointStudent}/{username}");
                UnityWebRequestAsyncOperation handler = request.SendWebRequest();

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
                    Debug.Log(responseCode);
                    if (responseCode == 200)
                    {
                        Debug.Log("El usuario fue eliminado");

                        Navigation navigation = gameObject.AddComponent<Navigation>();
                        navigation.LoadScene("Home1");
                    }
                    else
                    {
                        Debug.Log("No se pudo eliminar");
                    }
                }
            }
            else
            {

                UnityWebRequest request = UnityWebRequest.Delete($"{authenticationEndpointStudent}/{username}");
                UnityWebRequestAsyncOperation handler = request.SendWebRequest();

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
                Debug.Log("Entre");
                long responseCode = request.responseCode;
                Debug.Log(responseCode);
                if (request.result == UnityWebRequest.Result.Success)
                {
                    Debug.Log(responseCode);
                    if (responseCode == 200)
                    {
                        Debug.Log("El usuario fue eliminado");

                        Navigation navigation = gameObject.AddComponent<Navigation>();
                        navigation.LoadScene("Home2");
                    }
                    else
                    {
                        Debug.Log("No se pudo eliminar");
                    }
                }
            }
        }
        else
        {
            Debug.Log("No se pudo conectar al servidor");
        }

        yield return null;
    }

    
    private IEnumerator TryInsertInCurso(string[] students){

        string curso = SessionData.CursoSeleccionado; //curso donde se quieran insertar los alumnos
        UpdateResponseRemove updateResponse = new UpdateResponseRemove
        {
            students = students
        };

        string json = JsonUtility.ToJson(updateResponse);

        Debug.Log(json);

        Debug.Log(curso);


        // Crear la solicitud PUT con los datos JSON
        UnityWebRequest request = new UnityWebRequest($"{authenticationEndpointUpdateCourses}/{curso}/insertStudents", UnityWebRequest.kHttpVerbPUT);
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("Accept", "application/json");

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
           if(responseCode == 200){

                Debug.Log($"Estudiantes: {students} agregados al curso");

                navigation = gameObject.AddComponent<Navigation>();

                navigation.LoadScene("Editor_cursos");

           }
           else{
            Debug.Log("No se pudo agregar al estudiante");
           }
        }else{
            Debug.Log("No se puedo conectar al servidor");
        }

    }

     private IEnumerator TryRemoveFromCurso(string[] students){

        string curso = SessionData.CursoSeleccionado; //curso donde se quieran insertar los alumnos
        UpdateResponseRemove updateResponse = new UpdateResponseRemove
        {
            students = students
        };

        string json = JsonUtility.ToJson(updateResponse);

        Debug.Log(json);

        Debug.Log(curso);

        // Crear la solicitud PUT con los datos JSON
        UnityWebRequest request = new UnityWebRequest($"{authenticationEndpointUpdateCourses}/{curso}/removeStudents", UnityWebRequest.kHttpVerbPUT);
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("Accept", "application/json");

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
           if(responseCode == 200){

                Debug.Log($"Estudiantes: {students} eliminados del curso");

                navigation = gameObject.AddComponent<Navigation>();

                navigation.LoadScene("Editor_cursos");

           }
           else{
            Debug.Log("No se pudo eliminar al estudiante");
           }
        }else{
            Debug.Log("No se puedo conectar al servidor");
        }

    }

    private IEnumerator TryUpdateStudent()
    {
        string username = usernameInputField.text;
        string password = passwordInputField.text;
        string sesion = SessionData.username;

        UpdateResponse updateResponse = new UpdateResponse
        {
            user = username,
            pass = password
        };

        string json = JsonUtility.ToJson(updateResponse);

        Debug.Log(json);

        // Crear la solicitud PUT con los datos JSON
        UnityWebRequest request = new UnityWebRequest($"{authenticationEndpointUpdateStudent}/{sesion}", UnityWebRequest.kHttpVerbPUT);
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("Accept", "application/json");

        // Enviar la solicitud y esperar la respuesta
        var handler = request.SendWebRequest();

        float startTime = 0.0f;
        while (!handler.isDone)
        {
            startTime += Time.deltaTime;
            if (startTime > 10.0f)
            {
                Debug.Log("Tiempo de espera excedido");
                yield break;
            }
            yield return null;
        }

        long responseCode = request.responseCode;
        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log(request.result);
            if (responseCode == 200)
            {
                if (SessionData.type == "E")
                {
                    Debug.Log("Usuario editado correctamente");

                    SessionData.username = username;

                    navigation = gameObject.AddComponent<Navigation>();

                    navigation.LoadScene("Perfil_estudiante");
                }
                else
                {
                    Debug.Log("Usuario editado correctamente");

                    SessionData.username = username;

                    navigation = gameObject.AddComponent<Navigation>();

                    navigation.LoadScene("Perfil_profesor");
                }
            }
            else
            {
                Debug.Log("No se pudo editar");
            }
        }
        else
        {
            Debug.Log("No se pudo conectar al servidor: " + request.error);
        }
    }
    
    public IEnumerator TryGetStudentsFromCurso()
    {
        string curso = SessionData.CursoSeleccionado;
        string url = $"{authenticationEndpointCourses}/students/{curso}";



        UnityWebRequest request = UnityWebRequest.Get(url);
        var handler = request.SendWebRequest();

        float startTime = 0.0f;
        while (!handler.isDone)
        {
            startTime += Time.deltaTime;
            if (startTime > 20.0f)
            {
                Debug.Log("Tiempo de espera excedido");
                break;
            }
            yield return null;
        }


        long responseCode = request.responseCode;
        Debug.Log(responseCode);
        if (request.result == UnityWebRequest.Result.Success)
        {
            if (responseCode == 200)
            {
                Debug.Log("Datos obtenidos exitosamente");

                // Parse the JSON response
                string responseText = request.downloadHandler.text;
                string stringLimpio = responseText.Replace("[", "").Replace("]", "").Replace("'", "").Replace(", ", ",");
                List<string> students = new List<string>(stringLimpio.Split(','));


                // Save the students in SessionData
                if (!SessionData.alumnosPorCurso.ContainsKey(curso))
                {
                    SessionData.alumnosPorCurso[curso] = new List<string>();
                }

                SessionData.alumnosPorCurso[curso].Clear();
                SessionData.alumnosPorCurso[curso].AddRange(students);
                navigation = gameObject.AddComponent<Navigation>();
                if(Flag == "K"){
                    navigation.LoadScene("Mis_cursos2");               

                }
                else{
                    navigation.LoadScene("Editar_curso2");

                }


            }
        }
        else
        {
            Debug.Log("No se pudo conectar al servidor");
        }
        yield return null;
    }


    private IEnumerator TryDeleteCurso(){

        string curso= cursoInputField.text;    

        if(SessionData.CursoSeleccionado == curso){
            Debug.Log(SessionData.CursoSeleccionado);
            UnityWebRequest request = UnityWebRequest.Delete($"{authenticationEndpointCourses}/{curso}");
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
    
                    Debug.Log("El curso fue eliminado");
                    
                    SessionData.curso.Remove(curso);

                    navigation = gameObject.AddComponent<Navigation>();

                    navigation.LoadScene("Editor_cursos");


                    
                }
                else{
                        Debug.Log("No se pudo eliminar");
                
                }
            }else{
                Debug.Log("No se pudo conectar al servidor");
            }     

        }


        else{
            Debug.Log("Escriba el curso seleccionado");
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
           SessionData.progreso +=1;
        }else{
            Debug.Log("No se puedo conectar al servidor");
        }
    }

    public IEnumerator TryGetStudentProgress(string user){
        string username = user.Replace("\"", "");    

        string url = $"{authenticationEndpointStudent}/{username}/prog";

        UnityWebRequest request = UnityWebRequest.Get(url);
        var handler = request.SendWebRequest();
        //Debug.Log("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
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

                string data = request.downloadHandler.text;
                float data1 = float.Parse(data);
                Debug.Log(data1);
                progressBar.GetComponent<interfaz.Image>().fillAmount = data1/4;

            }



        }else{
            Debug.Log("No existe el usuario");
        }
        yield return null;
    }

}