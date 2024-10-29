// RemoveStudent.js
import React, { useState, useEffect } from 'react';
import './RemoveStudentPage.css';
import config from '../config/config';
import back from '../media/back.svg';

const RemoveStudent = ({ courseName, onNavigateBack }) => {
  const [students, setStudents] = useState([]);
  const [selectedStudent, setSelectedStudent] = useState('');
  const [error, setError] = useState(null);

  useEffect(() => {
    const fetchStudents = async () => {
      const url = `${config.backendUrl}/students/in-course/${courseName}`;
      try {
        const response = await fetch(url, {
          headers: {
            'ngrok-skip-browser-warning': 'true',
          },
        });
        if (response.ok) {
          const data = await response.json();
          setStudents(data);
        } else {
          setError('Error al obtener los estudiantes');
        }
      } catch (error) {
        setError('Error al conectar con el servidor');
      }
    };
    fetchStudents();
  }, [courseName]);

  const handleRemoveStudent = async (e) => {
    e.preventDefault();
    const url = `${config.backendUrl}/updateCurso/${courseName}/removeStudents`;
    try {
      const response = await fetch(url, {
        method: 'PUT',
        headers: {
          'Content-Type': 'application/json',
          'ngrok-skip-browser-warning': 'true',
        },
        body: JSON.stringify({ students: [selectedStudent] }),
      });

      if (response.ok) {
        console.log('Estudiante eliminado con éxito');
        onNavigateBack(); // Regresa a ModifyCourse después de eliminar al estudiante
      } else {
        const errorMessage = await response.text();
        setError(errorMessage);
      }
    } catch (error) {
      setError('Error al eliminar el estudiante');
    }
  };

  return (
    <div className="page-container">

      <div className="remove-student-container col-12 justify-content-center col-lg-8 col-xs-12 col-md-10 col-sm-10 col-xl-8 col-xxl-6">
      <div className="col-12">
            <img className="img-fluid btn-back-svg" onClick={onNavigateBack} src={back} alt="Volver" />
          </div>
        <h1 className='display-2'>Eliminar Alumno de {courseName}</h1>
        <form onSubmit={handleRemoveStudent}>
          <div className="form-group">
            <label className='display-4'>Nombre del Alumno:</label>
            <select
              value={selectedStudent}
              onChange={(e) => setSelectedStudent(e.target.value)}
              required
            >
              <option value="" disabled>Seleccione un alumno</option>
              {students.map(student => (
                <option key={student._id} value={student.username}>{student.username}</option>
              ))}
            </select>
          </div>
          {error && <p className="error-message">{error}</p>}
          <div className="form-buttons col-12 justify-content-center m-1 p-1">
            <button type="submit" className="btn btn-remove col-6 p-2 m-4">Eliminar</button>
          </div>
        </form>
      </div>
    </div>
  );
};

export default RemoveStudent;
