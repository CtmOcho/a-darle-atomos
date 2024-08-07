import React, { useState, useContext, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { UserContext } from '../context/UserContext';
import './DashboardPage.css';
import profileImage from '../media/perfil.png';

const DashboardPage = () => {
  const navigate = useNavigate();
  const { user, setUser } = useContext(UserContext);
  const [currentIndex, setCurrentIndex] = useState(0);
  const [dropdownVisible, setDropdownVisible] = useState(false);
  const [showPopup, setShowPopup] = useState(false);

  useEffect(() => {
    // Mostrar popup y redirigir a home si no hay datos en la sesión
    if (!user || !user.username) {
      setShowPopup(true);
      navigate('/');
    }
  }, [user, navigate]);

  const carouselItems = [
    'Color a la Llama', 'Sublimación del Yodo Sólido', 'Experimento de Destilación', 'Ley de Gases', 'Experimento de Rutherford',
    'Sodio Metálico y Agua', 'Camaleón Químico', 'Lluvia Dorada', 'Identificación Ácido-base', 'Pasta de Dientes para Elefantes'
  ];

  const handleLogout = () => {
    // Reiniciar datos de la sesión
    setUser(null);
    localStorage.removeItem('user'); // Si guardaste los datos de sesión en localStorage
    navigate('/');
  };

  const handleProfile = () => {
    navigate('/profile');
  };

  const handleCourseEdit = () => {
    navigate('/edit-courses');
  };

  const handlePrev = () => {
    setCurrentIndex((prevIndex) => (prevIndex === 0 ? carouselItems.length - 1 : prevIndex - 1));
  };

  const handleNext = () => {
    setCurrentIndex((prevIndex) => (prevIndex === carouselItems.length - 1 ? 0 : prevIndex + 1));
  };

  const handleItemClick = () => {
    navigate(`/experiment/${carouselItems[currentIndex]}`);
  };

  const handleClosePopup = () => {
    setShowPopup(false);
  };

  return (
    <div className="dashboard-container">
      <div className="top-buttons">
        <div className="dropdown">
          <img
            src={profileImage}
            alt="Perfil"
            className="btn-profile"
            onClick={() => setDropdownVisible(!dropdownVisible)}
          />
          {dropdownVisible && (
            <div className="dropdown-content">
              <button onClick={handleProfile}>Mi Perfil</button>
              <button onClick={handleLogout}>Cerrar Sesión</button>
              {user && user.type === 'P' && (
                <button onClick={handleCourseEdit}>Editar Cursos</button>
              )}
            </div>
          )}
        </div>
      </div>
      <div className="carousel-container">
        <button className="carousel-btn left-btn" onClick={handlePrev}>◀</button>
        <div className="carousel">
          <button className="carousel-item" onClick={handleItemClick}>{carouselItems[currentIndex]}</button>
        </div>
        <button className="carousel-btn right-btn" onClick={handleNext}>▶</button>
      </div>
      {showPopup && (
        <div className="popup">
          <div className="popup-content">
            <p>No se puede volver al dashboard de sesión finalizada</p>
            <button onClick={handleClosePopup}>Cerrar</button>
          </div>
        </div>
      )}
    </div>
  );
};

export default DashboardPage;
