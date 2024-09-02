import React from 'react';
import { useNavigate } from 'react-router-dom';
import './EditCoursesPage.css';

import config from '../config/config';
//${config.backendUrl} -> reemplazar http://localhost:13756 por esto!!!


const EditCoursesPage = () => {
  const navigate = useNavigate();

  return (
    <div className="page-container col-12">
    <nav className="navbar col-12">
      <button className="btn-back" onClick={() => navigate(-1)}>Volver</button>
    </nav>
      <div className="edit-courses-page-container col-lg-6 col-xs-12 col-md-10 col-sm-10 col-xl-6 col-xxl-6 justify-content-center">
        <h1 className='display-2'>Editar Cursos</h1>
        <div className="course-buttons p-3">
          <button className="btn" onClick={() => navigate('/create-course')}>Crear Curso</button>
          <button className="btn" onClick={() => navigate('/modify-course')}>Modificar Curso</button>
          <button className="btn" onClick={() => navigate('/delete-course')}>Eliminar Curso</button>
        </div>
      </div>
    </div>
  );
};

export default EditCoursesPage;
