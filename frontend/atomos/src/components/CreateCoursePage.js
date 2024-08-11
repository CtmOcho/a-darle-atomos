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
      <button className="btn-back" onClick={() => navigate('/edit-courses')}>Volver</button>
      <div className="create-course-container">
        <h1>Crear Curso</h1>
        <form onSubmit={handleCreateCourse}>
          <div className="form-group">
            <label>Nombre del Curso</label>
            <input
              type="text"
              value={courseName}
              onChange={(e) => setCourseName(e.target.value)}
              required
            />
          </div>
          {error && <p className="error-message">{error}</p>}
          <div className="form-buttons">
            <button type="submit" className="btn">Crear</button>
            <button type="button" className="btn" onClick={() => navigate('/edit-courses')}>Cancelar</button>
          </div>
        </form>
      </div>
    </div>
  );
};

export default CreateCoursePage;
