using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SessionData
{
    public static string username;
    public static List<string> curso = new List<string>(); // Cambiado a lista de cadenas
    public static int progreso;
    public static string type;
    public static Dictionary<string, List<string>> alumnosPorCurso = new Dictionary<string, List<string>>();
    public static string CursoSeleccionado;
    public static string AlumnoSeleccionado;
    
    
}