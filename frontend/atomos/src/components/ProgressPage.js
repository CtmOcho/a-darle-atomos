import React, { useContext, useEffect, useState } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import { UserContext } from '../context/UserContext';
import './ProgressPage.css';
import config from '../config/config';

const ProgressPage = () => {
  const { user } = useContext(UserContext);
  const { username } = useParams();
  const navigate = useNavigate();
  const [progressData, setProgressData] = useState(Array(55).fill(0)); // Inicializar con un array de 55 ceros
  const [generalProgress, setGeneralProgress] = useState(0);



  const carouselItems = [
    'Color a la Llama', 'Sublimación del Yodo Sólido', 'Experimento de Destilación', 'Ley de Gases', 'Experimento de Rutherford',
    'Sodio Metálico y Agua', 'Camaleón Químico', 'Lluvia Dorada', 'Identificación Ácido-base', 'Pasta de Dientes para Elefantes',
    'Solución conductora'
  ];

  useEffect(() => {
    const fetchProgressDetails = async () => {
      try {
        // Realiza la solicitud GET al nuevo endpoint
        const response = await fetch(`${config.backendUrl}/getProgressData/${username}`);
        
        if (response.ok) {
          const data = await response.json();
          setProgressData(data.progressdata);
  
          // Calcular el porcentaje de progreso general
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
            {value === 1 ? 'Completado' : 'No completado'}
          </td>
        ))}
      </tr>
    );
  };
  

  return (
    <div className="progress-page-container">
      <nav className="navbar col-12">
        <button className="btn-back" onClick={() => navigate(-1)}>Volver</button>
      </nav>
      <div className="progress-details-container col-lg-8 col-xs-12 col-md-10 col-sm-10 col-xl-8 col-xxl-6 justify-content-center">
        <h1 className="display-4">Detalles progreso del estudiante: {username}</h1>
        <div className="progress-bar">
          <div className="progress" style={{ width: `${generalProgress}%` }}></div>
        </div>
        <h1 className="display-4">{generalProgress.toFixed(2)}%</h1>
        <table className="table table-striped mt-4 col-12">
          <thead>
            <tr>
              <th className='col-2' >Experimento</th>
              <th className='col-2'>Laboratorio</th>
              <th className='col-2'>Molecular</th>
              <th className='col-2'>Contenido Adicional</th>
              <th className='col-2'>Prequiz</th>
              <th className='col-2'>Postquiz</th>
            </tr>
          </thead>
          <tbody>
            {Array.from({ length: 11 }, (_, index) => renderProgressRow(index))}
          </tbody>
        </table>
      </div>
    </div>
  );
};

export default ProgressPage;
