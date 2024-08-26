import React, { useState, useEffect } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import './TeacherProgressCourse.css';
import config from '../config/config';

const TeacherProgressCourse = () => {
  const navigate = useNavigate();
  const { courseName } = useParams();
  const [students, setStudents] = useState([]);
  const [selectedStudent, setSelectedStudent] = useState('');
  const [error, setError] = useState(null);

  useEffect(() => {
    const fetchStudents = async () => {
        const url = `${config.backendUrl}/students/in-course/${courseName}`;
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

    const handleSelecStudent = () => {
        navigate(`/progress-detail/${selectedStudent}`);
      };

  return (
    <div className="page-container">
          <nav className="navbar col-12">
      <button className="btn-back" onClick={() => navigate(-1)}>Volver</button>
    </nav>
      <div className="teacher-progress-course-container m-1 p-2 justify-content-center col-lg-8 col-xs-12 col-md-10 col-sm-10 col-xl-8 col-xxl-6">
        <h1 className='display-2' >Seleccionar estudiante para ver su progreso</h1>
        <form onSubmit={handleSelecStudent}>
          <div className="form-group col-12 text-center">
            <label className='display-4 col-12'>Nombre del Alumno:</label>
            <select
            className='col-8 fs-2'
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
    </div>
  );
};

export default TeacherProgressCourse;
