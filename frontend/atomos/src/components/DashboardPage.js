import React, { useState, useContext, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { UserContext } from '../context/UserContext';
import './DashboardPage.css';
import profileImage from '../media/perfil.png';
import Modal from './Modal';
import image1 from '../media/dest-1.png';
import image2 from '../media/sublimacion-1.png';
import image3 from '../media/Heisenberg.png';
import image4 from '../media/ley-gas1.png';
import image5 from '../media/ruther1.png';
import image6 from '../media/yodo-en-agua.png';
import image7 from '../media/cam1.png';
import image8 from '../media/abas1.png';
import image9 from '../media/dor1.png';
import image10 from '../media/cond1.png';
import image11 from '../media/pasta1.png';

const experiments = {
  '7mo': [
    { title: 'Experimento de Destilación', image: image1 },
    { title: 'Sublimación del Yodo Sólido', image: image2 },
  ],
  '8vo': [
    { title: 'Color a la Llama', image: image3 },
    { title: 'Ley de Gases', image: image4 },
    { title: 'Experimento de Rutherford', image: image5 },
  ],
  '1ro': [
    { title: 'Sodio Metálico y Agua', image: image6 },
    { title: 'Camaleón Químico', image: image7 },
    { title: 'Identificación Ácido-base', image: image8 },
  ],
  '2do': [
    { title: 'Lluvia Dorada', image: image9 },
    { title: 'Solución Conductora', image: image10 },
    { title: 'Pasta de Dientes para Elefantes', image: image11 },
  ],
};

const DashboardPage = () => {
  const navigate = useNavigate();
  const { user, setUser } = useContext(UserContext);
  const [selectedCourse, setSelectedCourse] = useState('7mo');
  const [activeModal, setActiveModal] = useState(null);
  const [dropdownOpen, setDropdownOpen] = useState(false);
  const [menuOpen, setMenuOpen] = useState(false);

  useEffect(() => {
    if (!user || !user.username) {
      navigate('/');
    }
  }, [user, navigate]);

  const handleCourseSelect = (course) => {
    setSelectedCourse(course);
    setActiveModal(null); // Cierra cualquier modal activo
  };

  const handleModalOpen = (experiment) => {
    setActiveModal(experiment);
  };

  const handleModalClose = () => {
    setActiveModal(null);
  };

  const handleItemClick = (experimentTitle) => {
    navigate(`/experiment/${experimentTitle}`);
  };

  // Lógica para alternar el collapse y el dropdown, asegurando que solo uno esté abierto a la vez
  const toggleDropdown = () => {
    if (menuOpen) setMenuOpen(false); // Cierra el menú si está abierto
    setDropdownOpen(!dropdownOpen);
  };

  const toggleMenu = () => {
    if (dropdownOpen) setDropdownOpen(false); // Cierra el dropdown si está abierto
    setMenuOpen(!menuOpen);
  };

  return (
    <div className="dashboard-container col-12">
      <nav className="navbar navbar-expand-lg col-12">
        {/* Botón "Salir" con distribución de columnas */}
        <button className="btn-back col-xxl-1 col-xl-1 col-lg-1 col-md-2 col-sm-2 col-3" onClick={() => navigate('/')}>
          Salir
        </button>

        {/* Navbar Toggler con distribución de columnas */}
        <button
          className="navbar-toggler col-xxl-7 col-xl-7 col-lg-8 col-md-4 col-sm-4 col-3"
          type="button"
          onClick={toggleMenu} // Abre y cierra el menú colapsable
          aria-controls="navbarNav"
          aria-expanded={menuOpen}
          aria-label="Toggle navigation"
        >
          <span className="navbar-toggler-icon"></span>
        </button>

        {/* Contenido colapsable de la navbar */}
        <div className={`collapse navbar-collapse col-xxl-7 col-xl-7 col-lg-8 col-md-4 col-sm-4 col-3 ${menuOpen ? 'show' : ''}`} id="navbarNav">
          <ul className="navbar-nav">
            {Object.keys(experiments).map((course) => (
              <li className="nav-item" key={course}>
                <a
                  className={`courseButtons nav-link fs-1 ${selectedCourse === course ? 'active' : ''}`}
                  onClick={() => handleCourseSelect(course)}
                >
                  {course === '7mo' && '7mo Básico'}
                  {course === '8vo' && '8vo Básico'}
                  {course === '1ro' && '1ro Medio'}
                  {course === '2do' && '2do Medio'}
                </a>
              </li>
            ))}
          </ul>
        </div>

        {/* Dropdown del perfil */}
        <div className="top-buttons d-flex justify-content-end col-xxl-2 col-xl-2 col-lg-2 col-md-4 col-sm-4 col-4">
          <div className={`dropdown col-12 ${dropdownOpen ? 'show' : ''}`}>
            <img
              src={profileImage}
              alt="Perfil"
              className="btn-profile img-fluid"
              onClick={toggleDropdown} // Abre y cierra el dropdown
            />
            <div className="dropdown-content">
              <button onClick={() => navigate('/profile')}>Mi Perfil</button>
              {user.type === 'P' && (
                <>
                  <button onClick={() => navigate('/edit-courses')}>Gestor de Cursos</button>
                  <button onClick={() => navigate('/check-progress')}>Ver Progresos</button>
                </>
              )}
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
      </nav>

      {/* Contenedor de experimentos */}
      <div className="experiments-container col-12">
        {experiments[selectedCourse].map((experiment, index) => (
          <div
            key={index}
            className="experiment-card col-5 mx-3"
            onClick={() => handleModalOpen(experiment)}
            onMouseEnter={(e) => e.currentTarget.classList.add('hovered')}
            onMouseLeave={(e) => e.currentTarget.classList.remove('hovered')}
          >
            <div className="display-5 mb-2">{experiment.title}</div>
            <img src={experiment.image} alt={experiment.title} />
          </div>
        ))}
      </div>

      {/* Modal para mostrar detalles del experimento */}
      {activeModal && (
        <Modal experiment={activeModal} onClose={handleModalClose} onNavigate={handleItemClick} />
      )}
    </div>
  );
};

export default DashboardPage;
