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
          const progressSum = await response.text(); // Cambiado de response.json() a response.text()
          setProgress(Number(progressSum)); // Convertir el texto recibido a número
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
      <nav className='navbar col-12'>
        <button className="btn-back" onClick={() => navigate(-1)}>Volver</button>
      </nav>
      <div className="profile-container col-lg-8 col-xs-12 col-md-10 col-sm-10 col-xl-8 col-xxl-6 justify-content-center">
        <h1 className='display-1'>Perfil del Usuario</h1>
        <div className="profile-info  col-12">
          <div className={`profile-item username ${user.type === 'P' ? 'professor' : ''}`}>
            <h1 className='display-4 col-6' >{user.username}</h1>
          </div>
          {user.type === 'E' && (
            <>
              <div className="profile-item course">
                <h1 className='diplay-4 col-6'>{user.course}</h1>
              </div>
              <div className="profile-item progress-container">
                <h1 className='display-4'>Progreso:</h1>
                <div className="progress-bar">
                  <div className="progress" style={{ width: `${progress / 55 * 100}%` }}></div>
                </div>
                <h1 className='display-4'>{(progress / 55 * 100).toFixed(2)}%</h1> {/* Ajusta esto según sea necesario */}
              </div>
            </>
          )}
        </div>
        <div className="profile-buttons col-lg-8 col-xs-12 col-md-10 col-sm-10 col-xl-8 col-xxl-6">
          <button className="btn" onClick={() => navigate('/edit-profile')}>Editar</button>
          <button className="btn" onClick={() => navigate('/delete-profile')}>Borrar</button>
          {user.type === 'E' && (
            <>
        
          <button className="btn" onClick={() => navigate(`/progress-detail/${user.username}`)}>Progreso</button>
          </>

          )}
        </div>
      </div>
    </div>
  );
};  

export default ProfilePage;
