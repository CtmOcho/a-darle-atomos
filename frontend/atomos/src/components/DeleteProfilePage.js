import React, { useState, useContext } from 'react';
import { useNavigate } from 'react-router-dom';
import { UserContext } from '../context/UserContext';
import './DeleteProfilePage.css';
import config from "../config/config";

const DeleteProfilePage = () => {
  const { user, setUser } = useContext(UserContext);
  const [inputUsername, setInputUsername] = useState('');
  const [error, setError] = useState(null);
  const navigate = useNavigate();

  const handleDeleteProfile = async (e) => {
    e.preventDefault();

    if (inputUsername.toUpperCase() !== user.username.toUpperCase()) {
      setError('El nombre de usuario ingresado no coincide con su perfil actual.');
      return;
    }

    try {
      const response = await fetch(`${config.backendUrl}/student/${inputUsername.toUpperCase()}`, {
        method: 'DELETE',
        headers: {
          'Content-Type': 'application/json',
          'ngrok-skip-browser-warning': 'true',  // Añadir este encabezado
        },
      });

      if (response.ok) {
        alert('Usuario eliminado exitosamente');
        setUser(null);
        navigate('/');
      } else {
        const errorText = response.status === 404 ? 'Usuario no existe' : 'Error al borrar perfil';
        setError(errorText);
      }
    } catch (err) {
      setError('Error al conectarse con el servidor');
      console.error(err);
    }
  };


  return (
    <div className="delete-profile-container col-12">
        <nav className="navbar col-12">
      <button className="btn-back" onClick={() => navigate(-1)}>Volver</button>
    </nav>
      <h2 className='display-2'>Borrar perfil</h2>
      <form onSubmit={handleDeleteProfile} className="delete-form col-lg-12 col-xs-12 col-md-10 col-sm-10 col-xl-12 col-xxl-12 justify-content-center">
        <div className="form-group display-6">
          <label>Ingrese su nombre de usuario para borrarlo:</label>
          <input
            type="text"
            value={inputUsername}
            onChange={(e) => setInputUsername(e.target.value)}
            required
          />
        </div>
        {error && <p className="error">{error}</p>}
        <button type="submit" className="btn">Borrar Perfil</button>
      </form>
    </div>
  );
};

export default DeleteProfilePage;
