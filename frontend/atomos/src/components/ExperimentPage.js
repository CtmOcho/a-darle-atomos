import React, { useState } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import './ExperimentPage.css';
import config from '../config/config'; // Asegúrate de tener la configuración correcta

const ExperimentPage = () => {
  const { experimentName } = useParams();
  const navigate = useNavigate();
  const [preQuizCompleted, setPreQuizCompleted] = useState(false);

  const handlePreQuizComplete = () => {
    setPreQuizCompleted(true);
  };

  const handlePreQuizClick = () => {
    handlePreQuizComplete();
  };

  const handleNewQuizClick = () => {
    if (preQuizCompleted) {
      // Lógica para manejar el nuevo cuestionario
    }
  };

  const handleAdditionalContentClick = () => {
    navigate(`/experiment/${experimentName}/additional-content`);
  };

  return (
    <div className="page-container">
      <button className="btn-back" onClick={() => navigate(-1)}>Volver</button>
      <div className="experiment-page-container">
        <h1>{experimentName}</h1>
        <div className="quiz-buttons">
          <button className="btn-additional-content" onClick={handleAdditionalContentClick}>Contenido Adicional</button>
          <button className="btn-pre-quiz" onClick={handlePreQuizClick}>Pre Cuestionario</button>
          <button className="btn-new-quiz" onClick={handleNewQuizClick} disabled={!preQuizCompleted}>Post Cuestionario</button>
        </div>
      </div>
    </div>
  );
};

export default ExperimentPage;
