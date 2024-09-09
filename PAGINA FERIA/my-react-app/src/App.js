import React from 'react';
import { BrowserRouter as Router, Route, Routes,} from 'react-router-dom'; // Usa Routes en lugar de Switch
import './App.css';
import HomePage from './pages/HomePage'; // Asegúrate de que HomePage esté en src/HomePage.js
import QuienesSomos from './pages/QuienesSomos';
import ADarleAtomos from './pages/ADarleAtomos';
import Contacto from './pages/Contacto';
import InstagramIcon from '@mui/icons-material/Instagram';
import FacebookIcon from '@mui/icons-material/Facebook';
import LinkedInIcon from '@mui/icons-material/LinkedIn';
import YouTubeIcon from '@mui/icons-material/YouTube';

function App() {
  return (
    <Router>
    <div className="App d-flex flex-column">
      <nav className="navbar navbar-expand-lg navbar-light  fixed-top">
        <div className="container">
          <a className="navbar-logo" href="#home">
            <img
              className="img-fluid"
              src="/assets/logo_bw.png" // Reemplaza con la URL de tu logo
              alt="Logo"
            />
          </a>
          <button
            className="navbar-toggler"
            type="button"
            data-bs-toggle="collapse"
            data-bs-target="#navbarNav"
            aria-controls="navbarNav"
            aria-expanded="false"
            aria-label="Toggle navigation"
          >
            <span className="navbar-toggler-icon"></span>
          </button>
          <div className="collapse navbar-collapse" id="navbarNav">
            <ul className="navbar-nav ms-auto navbar-links">
              <li className="nav-item">
                <a className="nav-link" href="/">Inicio</a>
              </li>
              <li className="nav-item">
                <a className="nav-link" href="/a-darle-atomos">¿Qué es A Darle Átomos?</a>
              </li>
              <li className="nav-item">
                <a className="nav-link" href="/quienes-somos">Quiénes Somos</a>
              </li>
              <li className="nav-item">
                <a className="nav-link" href="/contacto">Contacto</a>
              </li>
            </ul>
          </div>
        </div>
      </nav>

        {/* Configuración de rutas con Routes (v6) */}
        <Routes>
          <Route exact path="/" element={<HomePage />} /> {/* Renderiza HomePage en la ruta "/" */}
          <Route exact path="/quienes-somos" element={<QuienesSomos />} />
          <Route exact path="/a-darle-atomos" element={<ADarleAtomos />} />
          <Route exact path="/contacto" element={<Contacto />} />
        </Routes>

        {/* Footer */}
        <footer>
        <div className="container fs-5">
        <div className="row">

          <div className="col-lg-4">
            <div className="row">
              <a> LINK FSW </a>
            </div>
          </div>

          <div className="col-lg-4">
            <div className="row">
            <span >Síguenos en nuestras redes sociales</span>
              <div>
                <a href="https://www.facebook.com/people/A-Darle-%C3%81tomos/61564009658829/">
                <FacebookIcon></FacebookIcon>
                </a>
                <a href="https://www.instagram.com/adarleatomos/">
                <InstagramIcon></InstagramIcon>
                </a>
                <a href="https://www.linkedin.com/company/a-darle-átomos/?viewAsMember=true">
                <LinkedInIcon></LinkedInIcon>
                </a>
                <a href="https://www.youtube.com/@A_Darle_%C3%81tomos">  
                <YouTubeIcon></YouTubeIcon>
                </a>
              </div>
            </div>
          </div>

          <div className="col-lg-4">
            <div className="row">
              <a> LOGO EMPRESA REDIRIGIR A QUIENES SOMOS</a>
            </div>
          </div>
        </div>
            <div className="container">

          <div className="text-center mt-6 row">
            <div className= "col">
              <p>cosa del copy</p>
            </div>
          </div>
            </div>
        </div>


        </footer>

    </div>
    </Router>
  );
}

export default App;
