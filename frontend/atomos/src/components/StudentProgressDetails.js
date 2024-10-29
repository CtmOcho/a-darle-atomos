// StudentProgressDetails.js
import React, { useEffect, useState, useContext } from 'react';
import { UserContext } from '../context/UserContext';
import './ProgressPage.css';
import config from '../config/config';
import checkIcon from '../media/check-circle.svg';
import crossIcon from '../media/close-circle.svg';
import back from '../media/back.svg';



const StudentProgressDetails = ({ username, onNavigateBack }) => {
  const { user } = useContext(UserContext);
  const [progressData, setProgressData] = useState(Array(50).fill(0));
  const [generalProgress, setGeneralProgress] = useState(0);

  const carouselItems = [
    'Color a la Llama', 'Sublimación del Yodo Sólido', 'Experimento de Destilación', 'Ley de Gases', 'Experimento de Rutherford',
    'Sodio Metálico y Agua', 'Camaleón Químico', 'Lluvia Dorada', 'Identificación Ácido-base', 'Pasta de Dientes para Elefantes'
  ];

  useEffect(() => {
    const fetchProgressDetails = async () => {
      try {
        const response = await fetch(`${config.backendUrl}/getProgressData/${username}`, {
          headers: {
            'ngrok-skip-browser-warning': 'true',
          },
        });

        if (response.ok) {
          const data = await response.json();
          setProgressData(data.progressdata);

          const completedActivities = data.progressdata.filter(value => value === 1).length;
          const totalActivities = data.progressdata.length;
          const progressPercentage = (completedActivities / totalActivities) * 100;

          setGeneralProgress(progressPercentage);
        } else {
          console.error('Error al obtener los detalles del progreso');
        }
      } catch (error) {
        console.error('Error al conectarse con el servidor', error);
      }
    };

    fetchProgressDetails();
  }, [username]);

  const renderProgressRow = (experimentIndex) => {
    const startIndex = experimentIndex * 5;
    return (
      <tr key={experimentIndex}>
        <td className='first-column col-2'>{carouselItems[experimentIndex]}</td>
        {progressData.slice(startIndex, startIndex + 5).map((value, index) => (
          <td key={index} className={value === 1 ? 'completed' : 'not-completed'}>
            <img src={value === 1 ? checkIcon : crossIcon} alt={value === 1 ? 'Completado' : 'No Completado'} className="img-fluid status-image-progress" />
          </td>
        ))}
      </tr>
    );
  };

  return (
    <div className="progress-page-container">
      
      <div className="progress-details-container col-lg-10 col-12 col-md-12 col-sm-12 col-xl-10 col-xxl-10 justify-content-center">
      <div className="col-1">
<img className="img-fluid btn-back-svg" onClick={onNavigateBack} src={back} alt="Volver" />
</div>
        <h1 className="display-4">Detalles progreso del estudiante: {username}</h1>
        <div className="progress-bar">
          <div className="progress" style={{ width: `${generalProgress}%` }}></div>
        </div>
        <h1 className="display-4">{generalProgress.toFixed(2)}%</h1>
        <div className='table-responsive'>
          <table className="table table-striped mt-4 col-12">
            <thead>
              <tr>
                <th className='col-2'>Experimento</th>
                <th className='col-2'>Laboratorio</th>
                <th className='col-2'>Molecular</th>
                <th className='col-2'>Contenido Adicional</th>
                <th className='col-2'>Prequiz</th>
                <th className='col-2'>Postquiz</th>
              </tr>
            </thead>
            <tbody>
              {Array.from({ length: 10 }, (_, index) => renderProgressRow(index))}
            </tbody>
          </table>
        </div>
      </div>
    </div>
  );
};

export default StudentProgressDetails;
