import React, { useContext, useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { UserContext } from '../context/UserContext';
import './ProfilePage.css';
import config from "../config/config";

const ProfilePage = () => {
  const { user } = useContext(UserContext);
  const navigate = useNavigate();
  const [progress, setProgress] = useState(0);

  useEffect(() => {
    const getProgress = async () => {
      try {
        const response = await fetch(`${config.backendUrl}/student/${user.username}/prog`);
        if (response.ok) {
          const data = await response.json();
          setProgress(data.progress);
        } else {
          console.error('Error al obtener el progreso');
        }
      } catch (error) {
        console.error('Error al conectarse con el servidor', error);
      }
    };

    getProgress();
  }, [user.username]);

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
            <>
              <div className="profile-item course">
                <p>{user.course}</p>
              </div>
              <div className="profile-item progress-container">
                <p>Progreso:</p>
                <div className="progress-bar">
                  <div className="progress" style={{ width: `${progress}%` }}></div>
                </div>
                <p>{progress}/100</p> {/* Ajusta esto según sea necesario */}
              </div>
            </>
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
