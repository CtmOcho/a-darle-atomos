import React, { useState, useEffect, useContext } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import './ExperimentPage.css';
import config from '../config/config'; // Asegúrate de tener la configuración correcta
import { UserContext } from '../context/UserContext'; // Asumiendo que tienes un contexto de usuario

const ExperimentPage = () => {
  const { experimentName } = useParams();
  const navigate = useNavigate();
  const { user } = useContext(UserContext);
  const [preQuizCompleted, setPreQuizCompleted] = useState(false);

  // Lista de experimentos en el orden específico
  const carouselItems = [
    'Color a la Llama', 'Sublimación del Yodo Sólido', 'Experimento de Destilación', 'Ley de Gases', 'Experimento de Rutherford',
    'Sodio Metálico y Agua', 'Camaleón Químico', 'Lluvia Dorada', 'Identificación Ácido-base', 'Pasta de Dientes para Elefantes',
    'Solución conductora'
  ];

  // Obtener el índice del experimento actual
  const experimentIndex = carouselItems.indexOf(experimentName);
  const progressIndex = experimentIndex * 5; // El primer elemento de cada grupo de 5

  useEffect(() => {
    // Llamar al backend para verificar el progreso del estudiante
    const checkPreQuizCompletion = async () => {
      try {
        const response = await fetch(`${config.backendUrl}/getStudent/${user.username}/prog/${progressIndex + 1}`); // progressIndex + 1 porque los índices de la API parecen ser 1-based
        const data = await response.json();
        if (data.progressValue === 1) {
          setPreQuizCompleted(true);
        }
      } catch (err) {
        console.error('Error al verificar el progreso:', err);
      }
    };

    checkPreQuizCompletion();
  }, [experimentName, user.username, progressIndex]);

  const handlePreQuizClick = () => {
    navigate(`/quiz/${experimentName}/pre`);
    setPreQuizCompleted(true);
  };

  const handlePostQuizClick = () => {
    if (preQuizCompleted) {
      navigate(`/quiz/${experimentName}/post`);
    }
  };

  const handleAdditionalContentClick = () => {
    navigate(`/experiment/${experimentName}/additional-content`);
  };

  return (
    <div className="page-container">
      <button className="btn-back" onClick={() => navigate('/dashboard')}>Volver</button>
      <div className="experiment-page-container">
        <h1>{experimentName}</h1>
        <div className="quiz-buttons">
          <button className="btn-additional-content" onClick={handleAdditionalContentClick}>Contenido Adicional</button>
          <button className="btn-pre-quiz" onClick={handlePreQuizClick}>Pre Cuestionario</button>
          <button className="btn-post-quiz" onClick={handlePostQuizClick} disabled={!preQuizCompleted}>Post Cuestionario</button>
        </div>
      </div>
    </div>
  );
};

export default ExperimentPage;
