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
        const response = await fetch(`${config.backendUrl}/student/${user.username}/prog`, {
          headers: {
            'ngrok-skip-browser-warning': 'true',  // Añadir este encabezado
          },
        });
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
        <button className="btn-back" onClick={() => navigate("/dashboard")}>Volver</button>
      </nav>
      <div className="profile-container row align-items-center text-center col-12 justify-content-center">
        <h1 className='display-1'>Perfil del Usuario</h1>
        <div className="profile-info row col-12 justify-content-center align-items-center">
          <div className={`profile-item username ${user.type === 'P' ? 'professor' : ''}`}>
            <h1 className='display-4 ' >{user.username}</h1>
          </div>
          {user.type === 'E' && (
            <>
              <div className="profile-item course">
                <h1 className='diplay-4 '>{user.course} </h1>
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
        <div className="row profile-buttons col-xs-12 col-sm-12 col-md-12 col-lg-12 col-xl-12 justify-content-center">
          <button className="btn col-xs-12 col-sm-12 col-md-3 col-lg-3 col-xl-3" onClick={() => navigate('/edit-profile')}>Editar</button>
          <button className="btn col-xs-12 col-sm-12 col-md-3 col-lg-3 col-xl-3" onClick={() => navigate('/delete-profile')}>Borrar</button>
          {user.type === 'E' && (
            <>
        
          <button className="btn col-xs-12 col-sm-12 col-md-3 col-lg-3 col-xl-3" onClick={() => navigate(`/progress-detail/${user.username}`)}>Progreso</button>
          </>

          )}
        </div>
      </div>
    </div>
  );
};  

export default ProfilePage;
