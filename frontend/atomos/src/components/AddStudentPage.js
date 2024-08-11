import React, { useState, useEffect } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import './AddStudentPage.css';
import config from '../config/config';

const AddStudentPage = () => {
  const navigate = useNavigate();
  const { courseName } = useParams();
  const [students, setStudents] = useState([]);
  const [selectedStudent, setSelectedStudent] = useState('');
  const [error, setError] = useState(null);

  useEffect(() => {
    const fetchStudents = async () => {
      const url = `${config.backendUrl}/students/not-in-course/${courseName}`;
      try {
        const response = await fetch(url);
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
        },
        body: JSON.stringify({ students: [selectedStudent] }),
      });

      if (response.ok) {
        console.log('Estudiante agregado con éxito');
        navigate('/modify-course');
      } else {
        const errorMessage = await response.text();
        console.log('Error al agregar el estudiante:', errorMessage);
        setError(errorMessage);
      }
    } catch (error) {
      console.error('Error al agregar el estudiante:', error);
      setError('Error al agregar el estudiante');
    }
  };

  return (
    <div className="page-container">
      <button className="btn-back" onClick={() => navigate('/modify-course')}>Volver</button>
      <div className="add-student-container">
        <h1>Agregar Alumno a {courseName}</h1>
        <form onSubmit={handleAddStudent}>
          <div className="form-group">
            <label>Nombre del Alumno:</label>
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
          <div className="form-buttons">
            <button type="submit" className="btn">Agregar</button>
            <button type="button" className="btn" onClick={() => navigate('/modify-course')}>Cancelar</button>
          </div>
        </form>
      </div>
    </div>
  );
};

export default AddStudentPage;
