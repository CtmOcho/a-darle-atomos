// ProfessorToolsPage.js
import React, { useContext, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { UserContext } from '../context/UserContext';
import './DashboardPage.css';
import { CSSTransition } from 'react-transition-group';
import profileImage from '../media/perfil.png';
import EditCourses from './EditCourses';
import TeacherProgress from './TeacherProgress';
import TeacherQuiz from './TeacherQuiz'; // Importa el componente TeacherQuiz

const ProfessorToolsPage = () => {
  const navigate = useNavigate();
  const { user, setUser } = useContext(UserContext);
  const [menuOpen, setMenuOpen] = useState(false);
  const [dropdownOpen, setDropdownOpen] = useState(false);
  const [inProp, setInProp] = useState(true);
  const [selectedOption, setSelectedOption] = useState("Gestor de Cursos");

  const toggleDropdown = () => {
    if (menuOpen) setMenuOpen(false);
    setDropdownOpen(!dropdownOpen);
  };

  const toggleMenu = () => {
    if (dropdownOpen) setDropdownOpen(false);
    setMenuOpen(!menuOpen);
  };

  const handleSelectedOption = (option) => {
    setSelectedOption(option);
  };

  const handleNavigateBack = () => {
    setSelectedOption(null); // Regresa a la vista principal
  };

  const renderContent = () => {
    switch (selectedOption) {
      case 'Gestor de Cursos':
        return <EditCourses onNavigate={handleNavigateBack} />;
      case 'Ver Progresos':
        return <TeacherProgress onNavigateBack={handleNavigateBack} />;
      case 'Ver Cuestionarios':
        return <TeacherQuiz onNavigateBack={handleNavigateBack} />; // Cambiado a TeacherQuiz
      default:
        return null;
    }
  };

  return (
    <div className="dashboard-container col-12">
      <nav className="navbar navbar-expand-lg col-12">
        <CSSTransition in={inProp} timeout={500} classNames="fade" unmountOnExit>
          <button className="btn-back col-xxl-1 col-xl-1 col-lg-1 col-md-2 col-sm-3 col-4" onClick={() => navigate('/dashboard')}>
            Volver
          </button>
        </CSSTransition>

        <CSSTransition in={inProp} timeout={500} classNames="fade" unmountOnExit>
          <button
            className="navbar-toggler col-xxl-7 col-xl-7 col-lg-8 col-md-4 col-sm-4 col-3"
            type="button"
            onClick={toggleMenu}
            aria-controls="navbarNav"
            aria-expanded={menuOpen}
            aria-label="Toggle navigation"
          >
            <span className="navbar-toggler-icon"></span>
          </button>
        </CSSTransition>

        <CSSTransition in={inProp} timeout={500} classNames="fade" unmountOnExit>
          <div className={`collapse navbar-collapse col-xxl-7 col-xl-7 col-lg-8 col-md-4 col-sm-4 col-4 ${menuOpen ? 'show' : ''}`} id="navbarNav">
            <ul className="navbar-nav">
              <li className="nav-item">
                <a className="courseButtons nav-link fs-2" onClick={() => handleSelectedOption('Gestor de Cursos')}>
                  Gestor de Cursos
                </a>
              </li>
              <li className="nav-item">
                <a className="courseButtons nav-link fs-2" onClick={() => handleSelectedOption('Ver Progresos')}>
                  Ver Progresos
                </a>
              </li>
              <li className="nav-item">
                <a className="courseButtons nav-link fs-2" onClick={() => handleSelectedOption('Ver Cuestionarios')}>
                  Ver Cuestionarios
                </a>
              </li>
            </ul>
          </div>
        </CSSTransition>

        <CSSTransition in={inProp} timeout={500} classNames="fade" unmountOnExit>
          <div className="top-buttons d-flex justify-content-end col-xxl-2 col-xl-2 col-lg-2 col-md-4 col-sm-4 col-6">
            <div className={`dropdown col-12 ${dropdownOpen ? 'show' : ''}`}>
              <img
                src={profileImage}
                alt="Perfil"
                className="btn-profile img-fluid"
                onClick={toggleDropdown}
              />
              <div className="dropdown-content">
                <button onClick={() => navigate('/profile')}>Mi Perfil</button>
                <button
                  onClick={() => {
                    setUser(null);
                    navigate('/');
                  }}
                >
                  Cerrar Sesión
                </button>
              </div>
            </div>
          </div>
        </CSSTransition>
      </nav>

      <div className="professor-tools-content col-12 row">
        {renderContent()}
      </div>
    </div>
  );
};

export default ProfessorToolsPage;
