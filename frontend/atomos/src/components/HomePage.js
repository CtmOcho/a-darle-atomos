import React from 'react';
import { useNavigate } from 'react-router-dom';
import logo from '../media/logo.png'; // Asegúrate de que la ruta sea correcta
import './HomePage.css';
import config from '../config/config';
import { CSSTransition } from 'react-transition-group';

const HomePage = () => {
  const navigate = useNavigate();
  const [inProp, setInProp] = React.useState(true);

  const handleNavigation = (path) => {
    setInProp(false);
    setTimeout(() => {
      navigate(path);
    }, 300); // La duración debe coincidir con la animación en CSS
  };

  return (
    <CSSTransition in={inProp} timeout={300} classNames="fade" unmountOnExit>
      <div className="home-container">
        <nav className="navbar col-12">
          <div>
            <img src={logo} alt="Logo" className="logo" />
          </div>
        </nav>
        <div className="row align-items-center text-center col-lg-10 col-xs-12 col-md-10 col-sm-10 col-xl-10 col-xxl-10 justify-content-center">
          <div className="col-12">
            <h1 className="title display-1">A darle átomos</h1>
            <div className="buttons-container justify-content-center align-items-center text-center">
              <button className="btn btn-block" onClick={() => handleNavigation('/register')}>Registrarse</button>
              <button className="btn btn-block" onClick={() => handleNavigation('/login')}>Iniciar Sesión</button>
            </div>
          </div>
        </div>
      </div>
    </CSSTransition>
  );
};

export default HomePage;
