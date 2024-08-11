import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import './RegisterPage.css';
import config from '../config/config';
//${config.backendUrl} -> reemplazar ht tp://localhost:13756 por esto!!!



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
          'Content-Type': 'application/x-www-form-urlencoded', // Necesario para PostWwwForm
        },
        body: "", // `PostWwwForm` en Unity no envía ningún cuerpo adicional
      });

      if (!response.ok) {
        throw new Error('Creación inválida');
      }

      if (response.status === 201) {
        console.log(`Usuario creado exitosamente, código: ${response.status}`);
        navigate('/login'); // Redirigir a la página de login de alumnos
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
          'Content-Type': 'application/x-www-form-urlencoded', // Necesario para PostWwwForm
        },
        body: "", // `PostWwwForm` en Unity no envía ningún cuerpo adicional
      });

      if (!response.ok) {
        throw new Error('Creación inválida');
      }

      if (response.status === 201) {
        console.log('Profesor fue creado exitosamente');
        navigate('/login'); // Redirigir a la página de login de profesores
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
      <button className="btn-back" onClick={() => navigate(-1)}>Volver</button>
      <div className="register-container">
        <h2>Registro</h2>
        <form className="form-register"  onSubmit={handleRegister}>
          <div className="form-group">
            <label>Nombre de usuario:</label>
            <input
              type="text"
              autoCapitalize="none"
              autoCorrect="off"
              value={username}
              onChange={(e) => setUsername(e.target.value.toUpperCase())} // Convertir a mayúsculas al escribir
              required
            />
          </div>
          <div className="form-group">
            <label>Contraseña:</label>
            <input
              type="password"
              autoCapitalize="none"
              autoCorrect="off"
              value={password}
              onChange={(e) => setPassword(e.target.value)}
              required
            />
          </div>
          <div className="form-group">
            <label>
              <input
                type="checkbox"
                checked={isStudent}
                onChange={(e) => setIsStudent(e.target.checked)}
              />
              Soy estudiante
            </label>
          </div>
          {error && <p className="error">{error}</p>}
          <button type="submit" className="btn">Registrarse</button>
        </form>
      </div>
    </div>
  );
};

export default RegisterPage;
