// EditCourses.js
import React, { useState } from 'react';
import './EditCoursesPage.css';
import CreateCourse from './CreateCourse';
import ModifyCourse from './ModifyCourse';
import DeleteCourse from './DeleteCourse';

const EditCourses = ({ onNavigate }) => {
  const [currentView, setCurrentView] = useState('main'); // "main", "create", "modify", "delete"
  const [selectedCourse, setSelectedCourse] = useState('');

  const handleViewChange = (view) => {
    setCurrentView(view);
  };

  return (
    <div className="page-container container-fluid col-12">

      {currentView === 'main' && (
        <div className="edit-courses-page-container col-lg-6 col-xs-12 col-md-10 col-sm-10 col-xl-6 col-xxl-6 justify-content-center">
          <h1 className='display-2'>Editar Cursos</h1>
          <div className="course-buttons p-3">
            <button className="btn" onClick={() => handleViewChange('create')}>Crear Curso</button>
            <button className="btn" onClick={() => handleViewChange('modify')}>Modificar Curso</button>
            <button className="btn" onClick={() => handleViewChange('delete')}>Eliminar Curso</button>
          </div>
        </div>
      )}

      {currentView === 'create' && (
        <CreateCourse onNavigateBack={() => handleViewChange('main')} />
      )}

      {currentView === 'modify' && (
        <ModifyCourse
          onNavigateBack={() => handleViewChange('main')}
        />
      )}

      {currentView === 'delete' && (
        <DeleteCourse onNavigateBack={() => handleViewChange('main')} />
      )}
    </div>
  );
};

export default EditCourses;
