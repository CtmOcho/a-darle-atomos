import React, { useState, useEffect, useContext } from 'react';
import { useNavigate } from 'react-router-dom';
import { UserContext } from '../context/UserContext';
import './DeleteCoursePage.css';

const DeleteCoursePage = () => {
  const navigate = useNavigate();
  const { user } = useContext(UserContext);
  const [courses, setCourses] = useState([]);
  const [selectedCourse, setSelectedCourse] = useState('');
  const [error, setError] = useState(null);

  useEffect(() => {
    const fetchCourses = async () => {
      const url = `http://localhost:13756/curso/${user.username}`;
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

  const handleDeleteCourse = async (e) => {
    e.preventDefault();
    const url = `http://localhost:13756/curso/${selectedCourse}`;
    try {
      const response = await fetch(url, {
        method: 'DELETE',
      });

      if (response.ok) {
        console.log('Curso eliminado con éxito');
        navigate('/edit-courses');
      } else {
        const errorMessage = await response.text();
        console.log('Error al eliminar el curso:', errorMessage);
        setError(errorMessage);
      }
    } catch (error) {
      console.error('Error al eliminar el curso:', error);
      setError('Error al eliminar el curso');
    }
  };

  return (
    <div className="page-container">
      <button className="btn-back" onClick={() => navigate('/edit-courses')}>Volver</button>
      <div className="delete-course-container">
        <h1>Eliminar Curso</h1>
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
          <div className="form-buttons">
            <button className="btn" onClick={handleDeleteCourse}>Eliminar Curso</button>
          </div>
        )}
      </div>
    </div>
  );
};

export default DeleteCoursePage;
