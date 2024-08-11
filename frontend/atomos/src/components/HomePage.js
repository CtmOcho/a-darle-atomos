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
      <img src={logo} alt="Logo" className="logo" />
      <h1 className="title">A darle átomos</h1>
      <div className="buttons-container">
        <button className="btn" onClick={() => navigate('/register')}>Registrarse</button>
        <button className="btn" onClick={() => navigate('/login')}>Iniciar Sesión</button>
      </div>
    </div>
  );
};

export default HomePage;
