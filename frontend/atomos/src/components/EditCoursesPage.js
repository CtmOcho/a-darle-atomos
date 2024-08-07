import React from 'react';
import { useNavigate } from 'react-router-dom';
import './EditCoursesPage.css';

const EditCoursesPage = () => {
  const navigate = useNavigate();

  return (
    <div className="page-container">
      <button className="btn-back" onClick={() => navigate('/dashboard')}>Volver</button>
      <div className="edit-courses-page-container">
        <h1>Editar Cursos</h1>
        <p>Aquí puedes editar los cursos.</p>
        <div className="course-buttons">
          <button className="btn-create-course" onClick={() => navigate('/create-course')}>Crear Curso</button>
          <button className="btn-modify-course" onClick={() => navigate('/modify-course')}>Modificar Curso</button>
          <button className="btn-delete-course" onClick={() => navigate('/delete-course')}>Eliminar Curso</button>
        </div>
      </div>
    </div>
  );
};

export default EditCoursesPage;