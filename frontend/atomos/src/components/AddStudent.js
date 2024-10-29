// AddStudent.js
import React, { useState, useEffect } from 'react';
import './AddStudentPage.css';
import config from '../config/config';
import back from '../media/back.svg';

const AddStudent = ({ courseName, onNavigateBack }) => {
  const [students, setStudents] = useState([]);
  const [selectedStudent, setSelectedStudent] = useState('');
  const [error, setError] = useState(null);

  useEffect(() => {
    const fetchStudents = async () => {
      const url = `${config.backendUrl}/students/not-in-course/${courseName}`;
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

  const handleAddStudent = async (e) => {
    e.preventDefault();
    const url = `${config.backendUrl}/updateCurso/${courseName}/insertStudents`;
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
        console.log('Estudiante agregado con éxito');
        onNavigateBack(); // Regresa a ModifyCourse después de agregar al estudiante
      } else {
        const errorMessage = await response.text();
        setError(errorMessage);
      }
    } catch (error) {
      setError('Error al agregar el estudiante');
    }
  };

  return (
    <div className="page-container">
      <div className="add-student-container justify-content-center col-lg-8 col-xs-12 col-md-10 col-sm-10 col-xl-8 col-xxl-6">
      <div className="col-12">
            <img className="img-fluid btn-back-svg" onClick={onNavigateBack} src={back} alt="Volver" />
          </div>
        <h1 className='display-2'>Agregar Alumno a {courseName}</h1>
        <form onSubmit={handleAddStudent}>
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
          <div className="form-buttons col-12 justify-content-center">
            <button type="submit" className="btn col-6 p-2 m-4">Agregar</button>
          </div>
        </form>
      </div>
    </div>
  );
};

export default AddStudent;
