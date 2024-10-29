// ModifyCourse.js
import React, { useState, useEffect, useContext } from 'react';
import { UserContext } from '../context/UserContext';
import './ModifyCoursePage.css';
import config from '../config/config';
import RemoveStudent from './RemoveStudent';
import AddStudent from './AddStudent';
import back from '../media/back.svg';

const ModifyCourse = ({ onNavigateBack }) => {
  const { user } = useContext(UserContext);
  const [courses, setCourses] = useState([]);
  const [selectedCourse, setSelectedCourse] = useState('');
  const [currentView, setCurrentView] = useState('main'); // "main", "remove", "add"
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

  const handleNavigateToRemove = (course) => {
    setSelectedCourse(course);
    setCurrentView('remove');
  };

  const handleNavigateToAdd = (course) => {
    setSelectedCourse(course);
    setCurrentView('add');
  };

  return (
    <div className="page-container container-fluid col-12">
      {currentView === 'main' && (
        <div className="row modify-course-container justify-content-center col-lg-8 col-xs-12 col-md-10 col-sm-10 col-xl-8 col-xxl-6">
          <div className="col-12">
            <img className="img-fluid btn-back-svg" onClick={onNavigateBack} src={back} alt="Volver" />
          </div>
          <h1 className='display-2'>Modificar Curso</h1>
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
                <button className="btn btn-remove col-4 p-2 m-4" onClick={() => handleNavigateToRemove(selectedCourse)}>Eliminar Alumno</button>
                <button className="btn col-4 p-2 m-4" onClick={() => handleNavigateToAdd(selectedCourse)}>Agregar Alumno</button>
              </div>
            )}
          </div>
        </div>
      )}

      {currentView === 'remove' && (
        <RemoveStudent
          courseName={selectedCourse}
          onNavigateBack={() => setCurrentView('main')}
        />
      )}

      {currentView === 'add' && (
        <AddStudent
          courseName={selectedCourse}
          onNavigateBack={() => setCurrentView('main')}
        />
      )}
    </div>
  );
};

export default ModifyCourse;
