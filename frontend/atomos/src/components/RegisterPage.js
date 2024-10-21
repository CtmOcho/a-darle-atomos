import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { CSSTransition } from 'react-transition-group';
import './RegisterPage.css';
import config from '../config/config';

const RegisterPage = () => {
  const [username, setUsername] = useState('');
  const [password, setPassword] = useState('');
  const [isStudent, setIsStudent] = useState(false);
  const [error, setError] = useState(null);
  const [successModal, setSuccessModal] = useState(false); // Controla el modal de éxito
  const [errorModal, setErrorModal] = useState(false); // Controla el modal de error
  const [inProp, setInProp] = useState(false); // Controla la transición de la página
  const navigate = useNavigate();

  useEffect(() => {
    const timer = setTimeout(() => {
      setInProp(true); // Iniciar fade-in
    }, 100); 

    return () => clearTimeout(timer); // Limpia el timeout si el componente se desmonta
  }, []);

  const handleRegister = async (e) => {
    e.preventDefault();
    if (isStudent) {
      await tryCreateStudent();
    } else {
      await tryCreateTeacher();
    }
  };

  const tryCreateStudent = async () => {
    const authenticationEndpointStudent = `${config.backendUrl}/student`;
    const upperCaseUsername = username.toUpperCase();
    const url = `${authenticationEndpointStudent}?user=${upperCaseUsername}&pass=${password}`;
    try {
      const response = await fetch(url, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/x-www-form-urlencoded',
          'ngrok-skip-browser-warning': 'true',
        },
        body: "",
      });

      if (response.status === 201) {
        setSuccessModal(true); // Mostrar modal de éxito
      } else if (response.status === 409) {
        const errorMessage = await response.text();
        setError(errorMessage);
        setErrorModal(true); // Mostrar modal de error
      } else {
        const errorMessage = await response.text();
        setError(errorMessage);
        setErrorModal(true); // Mostrar modal de error
      }
    } catch (err) {
      setError('No se pudo conectar al servidor');
      setErrorModal(true); // Mostrar modal de error
    }
  };

  const tryCreateTeacher = async () => {
    const authenticationEndpointTeacher = `${config.backendUrl}/teacher`;
    const upperCaseUsername = username.toUpperCase();
    const url = `${authenticationEndpointTeacher}/${upperCaseUsername}/${password}`;
    try {
      const response = await fetch(url, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/x-www-form-urlencoded',
          'ngrok-skip-browser-warning': 'true',
        },
        body: "",
      });

      if (response.status === 201) {
        setSuccessModal(true); // Mostrar modal de éxito
      } else if (response.status === 409) {
        const errorMessage = await response.text();
        setError(errorMessage);
        setErrorModal(true); // Mostrar modal de error
      } else {
        const errorMessage = await response.text();
        setError(errorMessage);
        setErrorModal(true); // Mostrar modal de error
      }
    } catch (err) {
      setError('No se pudo conectar al servidor');
      setErrorModal(true); // Mostrar modal de error
    }
  };

  const closeModals = () => {
    setSuccessModal(false);
    setErrorModal(false);
  };

  const handleNavigation = (path) => {
    setInProp(false); // Iniciar fade-out

    setTimeout(() => {
      navigate(path); // Navegar después del fade-out
    }, 500); 
  };

  return (
    <div className="page-container">
      <nav className="navbar col-12">
      <CSSTransition
        in={inProp}
        timeout={500}
        classNames="fade"
        unmountOnExit
      >
        <button className="btn-back" onClick={() => handleNavigation("/")}>Volver</button>
      </CSSTransition>

      </nav>
      
      {/* Contenido principal con fade-in y fade-out de 0.5s */}
      <CSSTransition
        in={inProp}
        timeout={500}
        classNames="fade"
        unmountOnExit
      >
        <div className="register-container col-lg-12 col-xs-12 col-md-10 col-sm-10 col-xl-12 col-xxl-12">
          <h2 className='display-2 justify-content-center'>Registro</h2>
          <form className="form-register justify-content-start" onSubmit={handleRegister}>
            <div className="form-group">
              <label>Nombre de usuario:</label>
              <input
                type="text"
                className="form-control"
                autoCapitalize="none"
                autoCorrect="off"
                value={username}
                onChange={(e) => setUsername(e.target.value.toUpperCase())}
                required
              />
            </div>
            <div className="form-group justify-content-center">
              <label>Contraseña:</label>
              <input
                type="password"
                className="form-control"
                autoCapitalize="none"
                autoCorrect="off"
                value={password}
                onChange={(e) => setPassword(e.target.value)}
                required
              />
            </div>
            <div className="form-group form-check">
              <input
                type="checkbox"
                className="form-check-input"
                id="studentCheck"
                checked={isStudent}
                onChange={(e) => setIsStudent(e.target.checked)}
              />
              <label className="form-check-label" htmlFor="studentCheck">
                Soy estudiante
              </label>
            </div>
            <button type="submit" className="btn btn-success w-100">Registrarse</button>
          </form>
        </div>
      </CSSTransition>



      {/* Modal de éxito con fade-in/fade-out */}
      <CSSTransition
        in={successModal}
        timeout={500}
        classNames="fade"
        unmountOnExit
      >
        <div className="custom-modal-overlay d-flex justify-content-center align-items-center col-12 col-sm-6 col-md-6 col-lg-5 col-xl-5 col-xxl-5">
          <div className="custom-modal-content justify-content-center p-4">
            <h2>¡Usuario creado exitosamente!</h2>
            <div className = "row col-12 justify-content-center">
            <button className="btn btn-primary justify-content-center " onClick={() => handleNavigation('/login')}>Iniciar Sesión</button>
            </div>
            <div className = "row col-12 justify-content-center">

            <button className="btn btn-secondary justify-content-center" onClick={closeModals}>Volver</button>
            </div>
          </div>
        </div>
      </CSSTransition>

      {/* Modal de error con fade-in/fade-out */}
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

export default RegisterPage;
