import React, { useState } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import './ExperimentPage.css';

const ExperimentPage = () => {
  const { experimentName } = useParams();
  const navigate = useNavigate();
  const [preQuizCompleted, setPreQuizCompleted] = useState(false);

  const handlePreQuizComplete = () => {
    setPreQuizCompleted(true);
  };

  const handlePreQuizClick = () => {
    // Lógica para manejar el pre cuestionario
    handlePreQuizComplete();
  };

  const handleNewQuizClick = () => {
    if (preQuizCompleted) {
      // Lógica para manejar el nuevo cuestionario
    }
  };

  return (
    <div className="page-container">
      <button className="btn-back" onClick={() => navigate(-1)}>Volver</button>
      <div className="experiment-page-container">
        <h1>{experimentName}</h1>
        <p>Aquí va la información adicional del experimento {experimentName}.</p>
        <div className="quiz-buttons">
          <button className="btn-pre-quiz" onClick={handlePreQuizClick}>Pre Cuestionario</button>
          <button className="btn-new-quiz" onClick={handleNewQuizClick} disabled={!preQuizCompleted}>Nuevo Cuestionario</button>
        </div>
      </div>
    </div>
  );
};

export default ExperimentPage;
