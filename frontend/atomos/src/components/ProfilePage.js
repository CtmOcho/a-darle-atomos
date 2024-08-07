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
      <div className="profile-container">
        <h1>Perfil del Usuario</h1>
        <div className="profile-info">
          <div className={`profile-item username ${user.type === 'P' ? 'professor' : ''}`}>
            <p>{user.username}</p>
          </div>
          {user.type === 'E' && (
            <div className="profile-item course">
              <p>{user.course}</p>
            </div>
          )}
        </div>
        <div className="profile-buttons">
          <button className="btn-edit" onClick={() => navigate('/edit-profile')}>Editar</button>
          <button className="btn-delete" onClick={() => navigate('/delete-profile')}>Borrar</button>
        </div>
      </div>
    </div>
  );
};

export default ProfilePage;
