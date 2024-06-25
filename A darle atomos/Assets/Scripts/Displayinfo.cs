using UnityEngine;
using TMPro;
using System.Collections.Generic;

public enum SessionDataField
{
    Username,
    Progreso,
    Curso,
    Tipo,
    CursoSeleccionado,
    alumnosPorCurso
}

public class DisplaySessionData : MonoBehaviour
{
    public TMP_Text displayText; // Campo para el componente TMP_Text
    public TMP_Dropdown studentDropdown; 
    public TMP_Dropdown courseDropdown; // Campo para el Dropdown de cursos
    public SessionDataField fieldToDisplay; // Campo para seleccionar qué propiedad mostrar
    public Transform studentScrollViewContent; // Contenedor del Scroll View para los alumnos
    public GameObject studentTextPrefab; // Prefab del Text para los nombres de los alumnos
    public Login loginfunc;

    void Start()
    {
        if (displayText != null)
        {
            string textToDisplay = GetSessionDataField(fieldToDisplay);
            displayText.text = textToDisplay;
        }
        else
        {
            Debug.Log("Text reference is not set.");
        }

        if (courseDropdown != null)
        {
            UpdateDropdown(courseDropdown, SessionData.curso);
            courseDropdown.onValueChanged.AddListener(delegate { OnCourseSelected(); });

            // Seleccionar la primera opción por defecto
            if (SessionData.curso.Count > 0)
            {
                courseDropdown.value = 0;
                OnCourseSelected(); // Llamar a OnCourseSelected para actualizar el curso seleccionado por defecto
            }
        }
        else
        {
            Debug.Log("Course dropdown is not set.");
        }

        if (studentDropdown != null)
        {
            // Inicialmente actualiza el dropdown de estudiantes con el curso seleccionado por defecto
            UpdateStudentDropdown(studentDropdown, SessionData.CursoSeleccionado);
        }
        else
        {
            Debug.Log("Student dropdown is not set.");
        }
    }

    private string GetSessionDataField(SessionDataField field)
    {
        switch (field)
        {
            case SessionDataField.Username:
                return SessionData.username;
            case SessionDataField.Progreso:
                return SessionData.progreso.ToString();
            case SessionDataField.Curso:
                return SessionData.curso.Count > 0 ? SessionData.curso[0] : "Sin Curso";
            case SessionDataField.Tipo:
                return SessionData.type;
            case SessionDataField.CursoSeleccionado:
                return SessionData.CursoSeleccionado;
            case SessionDataField.alumnosPorCurso:
                return string.Join(", ", SessionData.alumnosPorCurso.ContainsKey(SessionData.CursoSeleccionado) ? SessionData.alumnosPorCurso[SessionData.CursoSeleccionado] : new List<string>());
            default:
                return "Unknown Field";
        }
    }

    private void UpdateDropdown(TMP_Dropdown dropdown, List<string> options)
    {
        dropdown.ClearOptions();
        dropdown.AddOptions(options);
    }

    private void UpdateStudentDropdown(TMP_Dropdown dropdown,string course)
    {
        ICollection<string> llaves = SessionData.alumnosPorCurso.Keys;

            // Recorrer y mostrar las llaves
            foreach (string llave in llaves)
                {
                    Debug.Log("ACA");
                    Debug.Log(llave);
                }
        if (SessionData.alumnosPorCurso.ContainsKey(course))
        {
            List<string> students = SessionData.alumnosPorCurso[course];
            Debug.Log($"Found {students.Count} students for course {course}: {string.Join(", ", students)}");
            studentDropdown.ClearOptions();
            studentDropdown.AddOptions(students);
        }
        else
        {
            Debug.Log($"No students found for the selected course: {course}");
            studentDropdown.ClearOptions();
        }
    }

    private void OnCourseSelected()
    {
        string selectedCourse = courseDropdown.options[courseDropdown.value].text;
        SessionData.CursoSeleccionado = selectedCourse;
        UpdateStudentDropdown(studentDropdown, selectedCourse);
    }
}
