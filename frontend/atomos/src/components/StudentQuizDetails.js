// StudentQuizDetails.js
import React, { useEffect, useState, useContext } from 'react';
import { UserContext } from '../context/UserContext';
import './ProgressPage.css';
import config from '../config/config';
import checkIcon from '../media/check-circle.svg';
import crossIcon from '../media/close-circle.svg';
import back from '../media/back.svg';

const StudentQuizDetails = ({ username, onNavigateBack }) => {
  const { user } = useContext(UserContext);
  const [quizData, setQuizData] = useState(Array(10).fill(Array(6).fill(0))); // 10 experimentos, 6 valores cada uno

  const quizHeaders = [
    'Pre-Quiz 1', 'Pre-Quiz 2', 'Pre-Quiz 3',
    'Post-Quiz 1', 'Post-Quiz 2', 'Post-Quiz 3'
  ];

  const carouselItems = [
    'Color a la Llama', 'Sublimación del Yodo Sólido', 'Experimento de Destilación', 'Ley de Gases', 'Experimento de Rutherford',
    'Sodio Metálico y Agua', 'Camaleón Químico', 'Lluvia Dorada', 'Identificación Ácido-base', 'Pasta de Dientes para Elefantes'
  ];

  // Obtener datos de cada quiz para el usuario especificado
  useEffect(() => {
    const fetchQuizData = async () => {
      try {
        const response = await fetch(`${config.backendUrl}/quiz/${username}`, {
          headers: {
            'ngrok-skip-browser-warning': 'true',
          },
        });
  
        if (response.ok) {
          const quizzes = await response.json();
          setQuizData(quizzes); // Ahora solo establece los datos de los quizzes directamente
        } else {
          console.error('Error al obtener los datos del usuario');
        }
      } catch (error) {
        console.error('Error al conectarse con el servidor', error);
      }
    };
  
    fetchQuizData();
  }, [username]);
  
  const renderQuizRow = (experimentIndex) => (
    <tr key={experimentIndex}>
      <td className="first-column col-2">{carouselItems[experimentIndex]}</td>
      {quizData[experimentIndex].map((value, index) => (
        <td key={index} className={value === 1 ? 'completed' : 'not-completed'}>
          <img
            src={value === 1 ? checkIcon : crossIcon}
            alt={value === 1 ? 'Completado' : 'No Completado'}
            className="img-fluid status-image-progress"
          />
        </td>
      ))}
    </tr>
  );

  return (
    <div className="progress-page-container">
      <div className="progress-details-container col-lg-10 col-12 col-md-12 col-sm-12 col-xl-10 col-xxl-10 justify-content-center">
        <div className="col-1">
          <img className="img-fluid btn-back-svg" onClick={onNavigateBack} src={back} alt="Volver" />
        </div>
        <h1 className="display-4">Detalles de quizzes del estudiante: {username}</h1>
        <div className="table-responsive">
          <table className="table table-striped mt-4 col-12">
            <thead>
              <tr>
                <th className="col-2">Experimento</th>
                {quizHeaders.map((header, index) => (
                  <th key={index} className="col-1">{header}</th>
                ))}
              </tr>
            </thead>
            <tbody>
              {Array.from({ length: 10 }, (_, index) => renderQuizRow(index))}
            </tbody>
          </table>
        </div>
      </div>
    </div>
  );
};

export default StudentQuizDetails;
