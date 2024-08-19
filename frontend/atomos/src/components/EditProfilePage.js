import React, { useState, useContext } from 'react';
import { useNavigate } from 'react-router-dom';
import { UserContext } from '../context/UserContext';
import './EditProfilePage.css';
import config from "../config/config";


const EditProfilePage = () => {
  const { user, setUser } = useContext(UserContext);
  const [newUsername, setNewUsername] = useState(user.username);
  const [newPassword, setNewPassword] = useState('');
  const [error, setError] = useState(null);
  const navigate = useNavigate();

  const handleUpdateProfile = async (e) => {
    e.preventDefault();

    const updateData = {
      user: newUsername || null,
      pass: newPassword || null,
    };

    try {
      const response = await fetch(`${config.backendUrl}/updateStudent/${user.username}`, {
        method: 'PUT',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(updateData),
      });

      if (!response.ok) {
        const errorText = response.status === 404 ? 'Usuario no encontrado' : 'Error al actualizar el perfil';
        setError(errorText);
        return;
      }

      const updatedUser = await response.json();
      setUser({ ...user, username: updateData.user });
      alert('Perfil actualizado exitosamente');
      navigate('/profile');
    } catch (err) {
      setError('Error al conectarse con el servidor');
      console.error(err);
    }
  };

  return (
    <div className="edit-profile-container">
    <button className="btn-back" onClick={() => navigate(-1)}>Volver</button>

      <h2>Editar Perfil</h2>
      <form onSubmit={handleUpdateProfile} className="edit-form">
        <div className="form-group">
          <label>Nuevo Nombre de Usuario:</label>
          <input
            type="text"
            value={newUsername}
            onChange={(e) => setNewUsername(e.target.value.toUpperCase())}
          />
        </div>
        <div className="form-group">
          <label>Nueva Contraseña:</label>
          <input
            type="password"
            value={newPassword}
            onChange={(e) => setNewPassword(e.target.value)}
          />
        </div>
        {error && <p className="error">{error}</p>}
        <button type="submit" className="btn-update">Actualizar Perfil</button>
      </form>
    </div>
  );
};

export default EditProfilePage;
