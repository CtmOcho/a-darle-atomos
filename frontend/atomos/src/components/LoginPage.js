import React, { useState, useContext } from 'react';
import { useNavigate } from 'react-router-dom';
import { UserContext } from '../context/UserContext';
import './LoginPage.css';

import config from '../config/config';
//${config.backendUrl} -> reemplazar ht tp://localhost:13756/ por esto!!!

const LoginPage = () => {
  const [username, setUsername] = useState('');
  const [password, setPassword] = useState('');
  const [error, setError] = useState(null);
  const navigate = useNavigate();
  const { setUser } = useContext(UserContext);

  const handleLogin = async (e) => {
    e.preventDefault();
    const authenticationEndpointLog = `${config.backendUrl}/login`;
    const upperCaseUsername = username.toUpperCase();
    const url = `${authenticationEndpointLog}/${upperCaseUsername}/${password}`;

    try {
      const response = await fetch(url, {
        method: 'GET',
        headers: {
          'Content-Type': 'application/json',
        },
      });

      if (!response.ok) {
        throw new Error('Credenciales inválidas');
      }

      const data = await response.json();
      setUser({ username: data.username, course: data.curso, progress: data.progress, type: data.type }); // Guardar la información del usuario en el contexto

      // Aquí puedes manejar la respuesta del login y navegar a la página adecuada
      if (data.type === 'E') {
        console.log(`${upperCaseUsername}:${password}`);
        navigate('/dashboard'); // Redirigir a la página de dashboard para alumnos
      } else {
        console.log(`${upperCaseUsername}:${password}`);
        console.log('Profesor ingresó al sistema');
        navigate('/dashboard'); // Redirigir a la página post login para profesores
      }
    } catch (err) {
      setError('No existe el usuario');
      console.error(err);
    }
  };

  return (
    <div className="page-container">
      <button className="btn-back" onClick={() => navigate(-1)}>Volver</button>
      <div className="login-container">
        <h2>Inicio de Sesión</h2>
        <form className= "form-login" onSubmit={handleLogin}>
          <div className="form-group">
            <label>Nombre de usuario:</label>
            <input
              type="text"
              value={username}
              autoCapitalize="none"
              autoCorrect="off"
              onChange={(e) => setUsername(e.target.value.toUpperCase())} // Convertir a mayúsculas al escribir
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
          {error && <p className="error">{error}</p>}
          <button type="submit" className="btn">Iniciar Sesión</button>
        </form>
      </div>
    </div>
  );
};

export default LoginPage;
