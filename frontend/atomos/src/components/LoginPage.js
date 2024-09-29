import React, { useState, useContext, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { CSSTransition } from 'react-transition-group';
import { UserContext } from '../context/UserContext';
import './LoginPage.css';
import config from '../config/config';

const LoginPage = () => {
  const [username, setUsername] = useState('');
  const [password, setPassword] = useState('');
  const [error, setError] = useState(null); // Controla el mensaje de error
  const [errorModal, setErrorModal] = useState(false); // Controla el modal de error
  const [inProp, setInProp] = useState(false); // Controla la transición de la página
  const navigate = useNavigate();
  const { setUser } = useContext(UserContext);

  // Controla el fade-in del contenido principal
  useEffect(() => {
    const timer = setTimeout(() => {
      setInProp(true); // Comienza la transición
    }, 100);

    return () => clearTimeout(timer); // Limpia el timeout si el componente se desmonta
  }, []);

  const handleLogin = async (e) => {
    e.preventDefault();
    const authenticationEndpointLog = `${config.backendUrl}/login`;
    const upperCaseUsername = username.toUpperCase();

    try {
      const response = await fetch(authenticationEndpointLog, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
          'ngrok-skip-browser-warning': 'true',  // Añadir este encabezado si es necesario
        },
        body: JSON.stringify({
          user: upperCaseUsername,
          pass: password,
        }),
      });

      if (!response.ok) {
        const errorMessage = await response.text();
        setError(errorMessage);
        setErrorModal(true); // Mostrar modal de error
        return;
      }

      const data = await response.json();
      setUser({ username: data.username, course: data.curso, progress: data.progress, type: data.type }); // Guardar la información del usuario en el contexto

      // Redirigir según el tipo de usuario
      if (data.type === 'E') {
        console.log(`Alumno ${upperCaseUsername} al sistema`);
      } else {
        console.log(`Profesor ${upperCaseUsername} al sistema`);
      }
      navigate('/dashboard');
    } catch (err) {
      setError('No se pudo conectar al servidor');
      setErrorModal(true); // Mostrar modal de error en caso de fallo de conexión
    }
  };

  const closeModals = () => {
    setErrorModal(false); // Cerrar el modal de error
  };

  const handleNavigation = (path) => {
    setInProp(false); // Inicia el fade-out del contenido

    setTimeout(() => {
      navigate(path); // Navega después del fade-out
    }, 500); // Duración del fade-out
  };

  return (
    <div className="page-container">
      <nav className="navbar col-12">
        {/* Navbar con transición suave */}
        <CSSTransition
          in={inProp}
          timeout={500}
          classNames="fade"
          unmountOnExit
        >
          <button className="btn-back" onClick={() => handleNavigation('/')}>Volver</button>
        </CSSTransition>
      </nav>

      {/* Contenido principal con fade-in y fade-out de 0.5s */}
      <CSSTransition
        in={inProp}
        timeout={500}
        classNames="fade"
        unmountOnExit
      >
        <div className="login-container col-lg-12 col-xs-12 col-md-10 col-sm-10 col-xl-12 col-xxl-12 justify-content-center">
          <h2 className='display-2'>Inicio de Sesión</h2>
          <form className="form-login" onSubmit={handleLogin}>
            <div className="form-group">
              <label>Nombre de usuario:</label>
              <input
                type="text"
                value={username}
                autoCapitalize="none"
                autoCorrect="off"
                onChange={(e) => setUsername(e.target.value.toUpperCase())} 
                required
              />
            </div>
            <div className="form-group">
              <label>Contraseña:</label>
              <input
                type="password"
                value={password}
                autoCapitalize="none"
                autoCorrect="off"
                onChange={(e) => setPassword(e.target.value)}
                required
              />
            </div>
            <button type="submit" className="btn">Iniciar Sesión</button>
          </form>
        </div>
      </CSSTransition>

      {/* Modal de error */}
      <CSSTransition
        in={errorModal}
        timeout={500}
        classNames="fade"
        unmountOnExit
      >
        <div className="custom-modal-overlay d-flex justify-content-center align-items-center">
          <div className="custom-modal-content p-4">
            <h2 className="display-5">Ha ocurrido un error</h2>
            <p className="error-message">{error}</p>
            <button className="btn btn-secondary" onClick={closeModals}>Volver</button>
          </div>
        </div>
      </CSSTransition>

    </div>
  );
};

export default LoginPage;
