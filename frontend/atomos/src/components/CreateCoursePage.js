import React, { useState, useContext } from 'react';
import { useNavigate } from 'react-router-dom';
import { UserContext } from '../context/UserContext';
import './CreateCoursePage.css';
import config from '../config/config';

const CreateCoursePage = () => {
  const navigate = useNavigate();
  const { user } = useContext(UserContext);  // Obtener el nombre del profesor del contexto de usuario
  const [courseName, setCourseName] = useState('');
  const [error, setError] = useState(null);

  const handleCreateCourse = async (e) => {
    e.preventDefault();
    const url = `${config.backendUrl}/curso/${user.username}/${courseName}`;

    try {
      const response = await fetch(url, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
          'ngrok-skip-browser-warning': 'true',  // Añadir este encabezado
        },
      });

      if (response.status === 201) {
        console.log('Curso creado con éxito');
        navigate('/edit-courses'); // Redirigir a la página de edición de cursos después de crear el curso
      } else if (response.status === 409) {
        console.log('El curso ya existe');
        setError('El curso ya existe');
      } else if (response.status === 404) {
        console.log('El profesor no existe');
        setError('El profesor no existe');
      } else {
        console.log('Error al crear el curso');
        setError('Error al crear el curso');
      }
    } catch (error) {
      console.error('Error al crear el curso:', error);
      setError('Error al crear el curso');
    }
  };

  return (
    <div className="page-container">
    <nav className="navbar col-12">
      <button className="btn-back" onClick={() => navigate(-1)}>Volver</button>
    </nav>
      <div className="create-course-container justify-content-center col-lg-8 col-xs-12 col-md-10 col-sm-10 col-xl-8 col-xxl-6">
        <h1 className='display-2'>Crear Curso</h1>
        <form className="col-10 justify-content-center p-1 m-1 align-items-center" onSubmit={handleCreateCourse}>
          <div className="form-group">
            <label className='col-6 display-4'>Nombre del Curso</label>
            <input
            className='col-6'
              type="text"
              value={courseName}
              onChange={(e) => setCourseName(e.target.value)}
              required
            />
          </div>
          {error && <p className="error-message">{error}</p>}
          <div className="form-buttons justify-content-center col-12 p-1 m-1">
            <button type="submit" className="btn col-6 p-2 m-4">Crear</button>
          </div>
        </form>
      </div>
    </div>
  );
};

export default CreateCoursePage;
