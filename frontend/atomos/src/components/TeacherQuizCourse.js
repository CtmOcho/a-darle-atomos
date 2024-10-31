// TeacherQuizCourse.js
import React, { useState, useEffect } from 'react';
import StudentQuizDetails from './StudentQuizDetails';
import './TeacherProgressCourse.css';
import config from '../config/config';
import back from '../media/back.svg';

const TeacherQuizCourse = ({ courseName, onNavigateBack }) => {
  const [students, setStudents] = useState([]);
  const [selectedStudent, setSelectedStudent] = useState('');
  const [currentView, setCurrentView] = useState('main');
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

  const handleSelectStudent = () => {
    setCurrentView('details');
  };

  const handleBack = () => {
    if (currentView === 'details') {
      setCurrentView('main');
      setSelectedStudent('');
    } else {
      onNavigateBack();
    }
  };

  return (
    <div className="page-container">
      {currentView === 'main' && (
        <>
          <div className="teacher-progress-course-container m-1 p-2 justify-content-center col-lg-8 col-xs-12 col-md-10 col-sm-10 col-xl-8 col-xxl-6">
            <div className="col-12 justify-content-start">
              <img className="img-fluid btn-back-svg" onClick={onNavigateBack} src={back} alt="Volver" />
            </div>
            <h1 className="display-2">Seleccionar estudiante para ver sus quizzes</h1>
            <form onSubmit={(e) => { e.preventDefault(); handleSelectStudent(); }}>
              <div className="form-group col-12 text-center">
                <label className="display-4 col-12">Nombre del Alumno:</label>
                <select
                  className="col-8 fs-2"
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
                <button type="submit" className="btn col-6 p-2 m-4">Seleccionar</button>
              </div>
            </form>
          </div>
        </>
      )}

      {currentView === 'details' && (
        <StudentQuizDetails username={selectedStudent} onNavigateBack={handleBack} />
      )}
    </div>
  );
};

export default TeacherQuizCourse;
