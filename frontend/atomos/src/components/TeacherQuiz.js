// TeacherQuiz.js
import React, { useState, useEffect, useContext } from 'react';
import { UserContext } from '../context/UserContext';
import TeacherQuizCourse from './TeacherQuizCourse';
import config from '../config/config';
import './TeacherProgressPage.css';

const TeacherQuiz = ({ onNavigateBack }) => {
  const { user } = useContext(UserContext);
  const [courses, setCourses] = useState([]);
  const [selectedCourse, setSelectedCourse] = useState('');
  const [currentView, setCurrentView] = useState('main'); // "main" o "course"
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

  const handleSelectCourse = (courseName) => {
    setSelectedCourse(courseName);
    setCurrentView('course');
  };

  const handleBack = () => {
    if (currentView === 'course') {
      setCurrentView('main');
      setSelectedCourse('');
    } else {
      onNavigateBack();
    }
  };

  return (
    <div className="page-container container-fluid col-12">
      {currentView === 'main' && (
        <div className="row modify-course-container justify-content-center col-lg-8 col-xs-12 col-md-10 col-sm-10 col-xl-8 col-xxl-6 ">
          <h1 className="display-2">Revisar Quizzes</h1>
          {error && <p className="error-message">{error}</p>}
          <div className="form-group col-10 justify-content-center">
            <label className="col-12 display-4">Seleccionar Curso:</label>
            <select
              value={selectedCourse}
              onChange={(e) => handleSelectCourse(e.target.value)}
              required
              className="col-8 p-1 m-1"
            >
              <option value="" disabled className="col-8 p-1 m-1">Seleccione un curso</option>
              {courses.map(course => (
                <option key={course._id} value={course.course}>{course.course}</option>
              ))}
            </select>
          </div>
        </div>
      )}
      
      {currentView === 'course' && (
        <TeacherQuizCourse courseName={selectedCourse} onNavigateBack={handleBack} />
      )}
    </div>
  );
};

export default TeacherQuiz;
