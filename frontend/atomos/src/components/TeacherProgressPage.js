import React, { useState, useEffect, useContext } from 'react';
import { useNavigate } from 'react-router-dom';
import { UserContext } from '../context/UserContext';
import './TeacherProgressPage.css';
import config from '../config/config';
//${config.backendUrl} -> reemplazar http://localhost:13756 por esto!!!



const TeacherProgressPage = () => {
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

  const handleCheckStudent = () => {
    navigate(`/check-student-course/${selectedCourse}`);
  };

  return (
    <div className="page-container container-fluid col-12">
          <nav className="navbar col-12">
      <button className="btn-back" onClick={() => navigate(-1)}>Volver</button>
    </nav>
      <div className="row modify-course-container justify-content-center col-lg-8 col-xs-12 col-md-10 col-sm-10 col-xl-8 col-xxl-6 ">
        <h1 className='display-2'>Revisar Progreso</h1>
        {error && <p className="error-message">{error}</p>}
        <div className="form-group col-10 justify-content-center">
          <label className='col-12 display-4'>Seleccionar Curso:</label>
          <select
            value={selectedCourse}
            onChange={(e) => setSelectedCourse(e.target.value)}
            required
            className='col-8 p-1 m-1'
          >
            <option value="" disabled className='col-8 p-1 m-1'>Seleccione un curso</option>
            {courses.map(course => (
              <option key={course._id} value={course.course}>{course.course}</option>
            ))}
          </select>
        {selectedCourse && (
          <div className="row course-buttons justify-content-center p-1 m-1 col-12">
            <button className="btn col-6 p-2 m-4 " onClick={handleCheckStudent}>Seleccionar Curso</button>
          </div>
        )}
        </div>
      </div>
    </div>
  );
};

export default TeacherProgressPage;
