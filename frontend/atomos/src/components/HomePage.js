import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { CSSTransition } from 'react-transition-group';
import logo from '../media/logo.png'; // Asegúrate de que la ruta sea correcta
import './HomePage.css';

const HomePage = () => {
  const navigate = useNavigate();
  const [inProp, setInProp] = useState(false); // Controla la transición del contenido

  // Simula la carga de la página sin pantalla de carga
  useEffect(() => {
    const timer = setTimeout(() => {
      setInProp(true); // Comienza la transición del contenido principal
    }, 100);

    return () => clearTimeout(timer); // Limpia el timeout si el componente se desmonta
  }, []);

  const handleNavigation = (path) => {
    // Iniciar fade-out del contenido
    setInProp(false);

    // Espera a que el fade-out termine antes de navegar
    setTimeout(() => {
      navigate(path);
    }, 500); // La duración del timeout coincide con la duración del fade-out (500ms)
  };

  return (
    <div className="home-container col-12">
      {/* Navbar siempre visible, sin animación */}
      <nav className="row navbar col-12 ">
      <CSSTransition
        in={inProp}
        timeout={500}
        classNames="fade"
        unmountOnExit
      >
        <div>
          <img src={logo} alt="Logo" className="logo img-fluid" />
        </div>
      </CSSTransition>

      </nav>

      {/* Contenido principal con fade-in y fade-out de 0.5s */}
      <CSSTransition
        in={inProp}
        timeout={500}
        classNames="fade"
        unmountOnExit
      >
        <div className="row align-items-center text-center col-12 justify-content-center">
          <div className="col-12">
            <h1 className="title display-1">A darle átomos</h1>
            <div className="buttons-container justify-content-center align-items-center text-center">
              <button className="btn col-12 col-lg-6 col-xl-6 col-xxl-6" onClick={() => handleNavigation('/register')}>Registrarse</button>
              <button className="btn col-12 col-lg-6 col-xl-6 col-xxl-6" onClick={() => handleNavigation('/login')}>Iniciar Sesión</button>
            </div>
          </div>
        </div>
      </CSSTransition>
    </div>
  );
};

export default HomePage;
