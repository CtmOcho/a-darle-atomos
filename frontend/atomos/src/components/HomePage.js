import React from 'react';
import { useNavigate } from 'react-router-dom';
import logo from '../media/logo.png'; // Asegúrate de que la ruta sea correcta
import './HomePage.css';


import config from '../config/config';
//${config.backendUrl} -> reemplazar http://localhost:13756 por esto!!!

const HomePage = () => {
  const navigate = useNavigate();

  return (
    <div className="home-container">
        <nav className="navbar col-12">
          <div>
          <img src={logo} alt="Logo" className="logo" />
          </div>
        </nav>
      <div className="row align-items-center text-center col-lg-8 col-xs-12 col-md-10 col-sm-10 col-xl-8 col-xxl-6 justify-content-center">
        <div className="col-12">
          <h1 className="title display-1">A darle átomos</h1>
          <div className="buttons-container justify-content-center align-items-center text-center">
            <button className="btn btn-block" onClick={() => navigate('/register')}>Registrarse</button>
            <button className="btn btn-block" onClick={() => navigate('/login')}>Iniciar Sesión</button>
          </div>
        </div>
      </div>
    </div>
  );
};

export default HomePage;
