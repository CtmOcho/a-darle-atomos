import React, { useContext } from 'react';
import { useNavigate } from 'react-router-dom';
import { UserContext } from '../context/UserContext';
import './ProfilePage.css';

const ProfilePage = () => {
  const { user } = useContext(UserContext);
  const navigate = useNavigate();

  return (
    <div className="profile-page-container">
      <button className="btn-back" onClick={() => navigate(-1)}>Volver</button>
      <h1>Perfil del Usuario</h1>
      <div className="profile-info">
        <div className="profile-item username">
          <p>{user.username}</p>
        </div>
        <div className="profile-item course">
          <p>{user.course}</p>
        </div>
        <div className="profile-item progress-container">
          <p>Progreso:</p>
          <div className="progress-bar">
            <div className="progress" style={{ width: `${user.progress}%` }}></div>
          </div>
        </div>
      </div>
      <div className="profile-buttons">
        <button className="btn-edit">Editar</button>
        <button className="btn-delete">Borrar</button>
      </div>
    </div>
  );
};

export default ProfilePage;
