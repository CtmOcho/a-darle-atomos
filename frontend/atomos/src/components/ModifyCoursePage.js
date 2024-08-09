import React, { useState, useEffect, useContext } from 'react';
import { useNavigate } from 'react-router-dom';
import { UserContext } from '../context/UserContext';
import './ModifyCoursePage.css';
import config from '../config/config';
//${config.backendUrl} -> reemplazar http://localhost:13756 por esto!!!



const ModifyCoursePage = () => {
  const navigate = useNavigate();
  const { user } = useContext(UserContext);
  const [courses, setCourses] = useState([]);
  const [selectedCourse, setSelectedCourse] = useState('');
  const [error, setError] = useState(null);

  useEffect(() => {
    const fetchCourses = async () => {
      const url = `${config.backendUrl}/curso/${user.username}`;
      try {
        const response = await fetch(url);
        if (response.ok) {
          const data = await response.json();
          setCourses(data);
        } else {
          setError('Error al obtener los cursos');
        }
      } catch (error) {
        setError('Error al conectar con el servidor');
      }
    };

    fetchCourses();
  }, [user.username]);

  const handleAddStudent = () => {
    navigate(`/add-student/${selectedCourse}`);
  };

  const handleRemoveStudent = () => {
    navigate(`/remove-student/${selectedCourse}`);
  };

  return (
    <div className="page-container">
      <button className="btn-back" onClick={() => navigate('/edit-courses')}>Volver</button>
      <div className="modify-course-container">
        <h1>Modificar Curso</h1>
        {error && <p className="error-message">{error}</p>}
        <div className="form-group">
          <label>Seleccionar Curso:</label>
          <select
            value={selectedCourse}
            onChange={(e) => setSelectedCourse(e.target.value)}
            required
          >
            <option value="" disabled>Seleccione un curso</option>
            {courses.map(course => (
              <option key={course._id} value={course.course}>{course.course}</option>
            ))}
          </select>
        </div>
        {selectedCourse && (
          <div className="course-buttons">
            <button className="btn" onClick={handleAddStudent}>Agregar Alumno</button>
            <button className="btn" onClick={handleRemoveStudent}>Eliminar Alumno</button>
          </div>
        )}
      </div>
    </div>
  );
};

export default ModifyCoursePage;
