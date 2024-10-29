// DeleteCourse.js
import React, { useState, useEffect, useContext } from 'react';
import { UserContext } from '../context/UserContext';
import './DeleteCoursePage.css';
import config from '../config/config';
import back from '../media/back.svg';


const DeleteCourse = ({ onNavigateBack }) => {
  const { user } = useContext(UserContext);
  const [courses, setCourses] = useState([]);
  const [selectedCourse, setSelectedCourse] = useState('');
  const [error, setError] = useState(null);

  useEffect(() => {
    const fetchCourses = async () => {
      const url = `${config.backendUrl}/curso/${user.username}`;
      try {
        const response = await fetch(url, {
          headers: {
            'ngrok-skip-browser-warning': 'true',
          },
        });
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
    const url = `${config.backendUrl}/curso/${selectedCourse}`;
    try {
      const response = await fetch(url, {
        method: 'DELETE',
        headers: {
          'ngrok-skip-browser-warning': 'true',
        },
      });

      if (response.ok) {
        console.log('Curso eliminado con éxito');
        onNavigateBack(); // Regresa a EditCourses después de eliminar
      } else {
        const errorMessage = await response.text();
        setError(errorMessage);
      }
    } catch (error) {
      setError('Error al eliminar el curso');
    }
  };

  return (
    <div className="page-container ontainer-fluid col-12">
      <div className="delete-course-container col-lg-8 col-xs-12 col-md-10 col-sm-10 col-xl-8 col-xxl-6">
      <div className="col-12">
            <img className="img-fluid btn-back-svg" onClick={onNavigateBack} src={back} alt="Volver" />
          </div>
        <h1 className='display-2'>Eliminar Curso</h1>
        {error && <p className="error-message">{error}</p>}
        <div className="form-group">
          <label className='display-4'>Seleccionar Curso:</label>
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
            <button className="btn btn-remove" onClick={handleDeleteCourse}>Eliminar Curso</button>
          </div>
        )}
      </div>
    </div>
  );
};

export default DeleteCourse;
