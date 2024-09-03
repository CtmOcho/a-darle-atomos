import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import './RegisterPage.css';
import config from '../config/config';

const RegisterPage = () => {
  const [username, setUsername] = useState('');
  const [password, setPassword] = useState('');
  const [isStudent, setIsStudent] = useState(false);
  const [error, setError] = useState(null);
  const navigate = useNavigate();

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
          'ngrok-skip-browser-warning': 'true',  // Añadir este encabezado
        },
        body: "",
      });
  
      if (!response.ok) {
        throw new Error('Creación inválida');
      }
  
      if (response.status === 201) {
        console.log(`Usuario creado exitosamente, código: ${response.status}`);
        navigate('/login');
      } else {
        console.error('Creación inválida');
      }
    } catch (err) {
      setError('Conexión fallida');
      console.error(err);
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
          'ngrok-skip-browser-warning': 'true',  // Añadir este encabezado
        },
        body: "",
      });
  
      if (!response.ok) {
        throw new Error('Creación inválida');
      }
  
      if (response.status === 201) {
        console.log('Profesor fue creado exitosamente');
        navigate('/login');
      } else {
        console.error('Creación inválida');
      }
    } catch (err) {
      setError('No se pudo conectar al servidor');
      console.error(err);
    }
  };
  

  return (
    <div className="page-container">
<nav className="navbar col-12">
    <button className="btn-back" onClick={() => navigate(-1)}>Volver</button>
</nav>
    <div className="register-container col-lg-12 col-xs-12 col-md-10 col-sm-10 col-xl-12 col-xxl-12 ">
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
        {error && <p className="text-danger">{error}</p>}
        <button type="submit" className="btn btn-success w-100">Registrarse</button>
      </form>
    </div>
  </div>
);
};


export default RegisterPage;
