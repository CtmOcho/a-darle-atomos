import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import './QuizPage.css';
import config from "../config/config";


const QuizPage = () => {
  const navigate = useNavigate();

  const questions = [
    {
      question: '¿Cuál es el color de la llama cuando se quema sodio?',
      options: ['Amarillo', 'Azul', 'Verde', 'Rojo'],
    },
    {
      question: '¿Qué elemento se sublima al calentar?',
      options: ['Yodo', 'Azufre', 'Mercurio', 'Plomo'],
    },
    {
      question: '¿Quién desarrolló el modelo atómico que reemplazó al de Rutherford?',
      options: ['Niels Bohr', 'Albert Einstein', 'Ernest Rutherford', 'Marie Curie'],
    },
  ];

  const [answers, setAnswers] = useState(Array(questions.length).fill(null));

  const handleOptionChange = (questionIndex, optionIndex) => {
    const newAnswers = [...answers];
    newAnswers[questionIndex] = optionIndex;
    setAnswers(newAnswers);
  };

  const handleSubmit = () => {
    console.log('Respuestas enviadas:', answers);
    navigate('/dashboard');
  };

  return (
    <div className="quiz-page-container page-container">
      <button className="btn-back" onClick={() => navigate(-1)}>Volver</button>
      <div className="quiz">
        <h1>Cuestionario</h1>
        {questions.map((q, qi) => (
          <div key={qi} className="question-container">
            <p>{q.question}</p>
            <div className="options-container">
              {q.options.map((option, oi) => (
                <label key={oi} className="quiz-label">
                  <input
                    type="radio"
                    name={`question-${qi}`}
                    value={oi}
                    checked={answers[qi] === oi}
                    onChange={() => handleOptionChange(qi, oi)}
                  />
                  {option}
                </label>
              ))}
            </div>
          </div>
        ))}
        <button className="btn-submit quiz-submit">Enviar</button>
      </div>
    </div>
  );
};

export default QuizPage;
