import React, { useState, useContext, useEffect } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import './QuizPage.css';
import config from "../config/config";
import { UserContext } from '../context/UserContext';

const QuizPage = () => {
  const { experimentName, quizType } = useParams();
  const navigate = useNavigate();
  const { user } = useContext(UserContext); // Asumiendo que tienes un contexto de usuario para obtener el nombre de usuario actual

  // Definir las preguntas para cada experimento y cada tipo de cuestionario
  const quizData = {
    'Color a la Llama': {
      pre: [
        { question: '¿Cuál es el color de la llama cuando se quema sodio?', options: ['Amarillo', 'Azul', 'Verde', 'Rojo'], answer: 0 }, // Amarillo
        { question: '¿Qué se necesita para observar el color de la llama en un experimento?', options: ['Mechero Bunsen', 'Ácido', 'Agua destilada', 'Sodio'], answer: 0 }, // Bunsen
        { question: '¿Qué elemento químico produce un color violeta en la llama?', options: ['Litio', 'Sodio', 'Potasio', 'Cobre'], answer: 2 }, // Potasio
      ],
      post: [
        { question: '¿Qué causa el color amarillo en la llama al quemar sodio?', options: ['Electrones excitados', 'Oxidación', 'Emisión de luz'], answer: 0 }, // Electrones excitados
        { question: '¿Qué color produce el cobre en la llama?', options: ['Verde', 'Azul', 'Rojo', 'Amarillo'], answer: 0 }, // Verde
        { question: '¿Cómo se explica la emisión de luz en este experimento?', options: ['Excitación de electrones', 'Reacción química', 'Oxidación del metal'], answer: 0 }, // Excitación de electrones
      ],
    },
    'Sublimación del Yodo Sólido': {
      pre: [
        { question: '¿Qué es la sublimación?', options: ['Cambio de sólido a gas', 'Cambio de sólido a líquido', 'Cambio de gas a sólido', 'Cambio de líquido a gas'], answer: 0 }, // Cambio de sólido a gas
        { question: '¿Qué sucede cuando el yodo se calienta?', options: ['Se sublima', 'Se derrite', 'Se disuelve', 'Se oxida'], answer: 0 }, // Se sublima
        { question: '¿Cuál es la apariencia del yodo sublimado?', options: ['Gas violeta', 'Líquido incoloro', 'Sólido blanco', 'Gas incoloro'], answer: 0 }, // Gas violeta
      ],
      post: [
        { question: '¿Por qué el yodo puede sublimarse?', options: ['Por sus bajas fuerzas intermoleculares', 'Por su alta reactividad', 'Por su punto de ebullición bajo'], answer: 0 }, // Por sus bajas fuerzas intermoleculares
        { question: '¿Qué indica el color violeta del yodo sublimado?', options: ['La presencia de yodo en estado gaseoso', 'La pureza del yodo', 'La reactividad del yodo'], answer: 0 }, // La presencia de yodo en estado gaseoso
        { question: '¿Cómo se revierte la sublimación del yodo?', options: ['Enfriando el gas para que se solidifique', 'Calentando aún más el gas', 'Añadiendo agua al gas'], answer: 0 }, // Enfriando el gas para que se solidifique
      ],
    },
    'Experimento de Destilación': {
      pre: [
        { question: '¿Qué es la destilación?', options: ['Separación de líquidos por diferencias de punto de ebullición', 'Separación de sólidos por filtración', 'Proceso de congelación de líquidos', 'Evaporación de agua'], answer: 0 }, // Separación de líquidos por diferencias de punto de ebullición
        { question: '¿Cuál es el propósito de la destilación simple?', options: ['Separar componentes de una mezcla', 'Crear nuevos compuestos', 'Aumentar la presión de un gas'], answer: 0 }, // Separar componentes de una mezcla
        { question: '¿Qué instrumento se usa para calentar la mezcla en una destilación?', options: ['Mechero Bunsen', 'Termómetro', 'Condensador', 'Vaso de precipitados'], answer: 0 }, // Mechero Bunsen
      ],
      post: [
        { question: '¿Qué sucede durante el proceso de destilación?', options: ['El componente con menor punto de ebullición se evapora primero', 'Los líquidos se mezclan aún más', 'La temperatura se mantiene constante'], answer: 0 }, // El componente con menor punto de ebullición se evapora primero
        { question: '¿Cómo se recupera el líquido evaporado en la destilación?', options: ['Mediante la condensación en el condensador', 'Dejándolo evaporar completamente', 'Filtrando la mezcla'], answer: 0 }, // Mediante la condensación en el condensador
        { question: '¿Cuál es una aplicación común de la destilación en la industria?', options: ['Purificación de agua', 'Cocción de alimentos', 'Refinación de petróleo', 'Fabricación de vidrios'], answer: 2 }, // Refinación de petróleo
      ],
    },
    'Ley de Gases': {
      pre: [
        { question: '¿Cuál de las siguientes es una ley de los gases?', options: ['Ley de Boyle', 'Ley de Dalton', 'Ley de Faraday', 'Ley de Coulomb'], answer: 0 }, // Ley de Boyle
        { question: '¿Qué relación describe la ley de Boyle?', options: ['Presión y volumen', 'Temperatura y volumen', 'Presión y temperatura'], answer: 0 }, // Presión y volumen
        { question: '¿Qué sucede cuando se aumenta la temperatura de un gas a volumen constante?', options: ['Aumenta la presión', 'Disminuye la presión', 'El gas se solidifica', 'El gas se licua'], answer: 0 }, // Aumenta la presión
      ],
      post: [
        { question: '¿Qué indica un comportamiento ideal en un gas?', options: ['Que sigue exactamente las leyes de los gases', 'Que no se puede medir su volumen', 'Que no responde a cambios de presión'], answer: 0 }, // Que sigue exactamente las leyes de los gases
        { question: '¿Qué sucede con el volumen de un gas si se disminuye la presión?', options: ['El volumen aumenta', 'El volumen disminuye', 'El volumen se mantiene'], answer: 0 }, // El volumen aumenta
        { question: '¿Cuál es un ejemplo real donde se aplica la Ley de Boyle?', options: ['En la jeringa al extraer un líquido', 'En el crecimiento de cristales', 'En la evaporación del agua'], answer: 0 }, // En la jeringa al extraer un líquido
      ],
    },
    'Experimento de Rutherford': {
      pre: [
        { question: '¿Qué demostró el experimento de Rutherford?', options: ['La existencia del núcleo atómico', 'La estructura en capas del átomo', 'El tamaño del electrón'], answer: 0 }, // La existencia del núcleo atómico
        { question: '¿Qué partículas usó Rutherford en su experimento?', options: ['Partículas alfa', 'Electrones', 'Neutrones', 'Protones'], answer: 0 }, // Partículas alfa
        { question: '¿Qué ocurrió con la mayoría de las partículas alfa en el experimento de Rutherford?', options: ['Pasaron a través de la lámina de oro sin desviarse', 'Fueron absorbidas por la lámina', 'Rebotaron hacia la fuente'], answer: 0 }, // Pasaron a través de la lámina de oro sin desviarse
      ],
      post: [
        { question: '¿Qué indica la deflexión de algunas partículas en el experimento de Rutherford?', options: ['La presencia de un núcleo denso y pequeño', 'Que los átomos son completamente sólidos', 'Que los electrones tienen masa'], answer: 0 }, // La presencia de un núcleo denso y pequeño
        { question: '¿Qué modelo atómico fue refutado por el experimento de Rutherford?', options: ['Modelo del pudín de Thomson', 'Modelo de Bohr', 'Modelo de Dalton'], answer: 0 }, // Modelo del pudín de Thomson
        { question: '¿Qué revela la trayectoria recta de la mayoría de las partículas alfa?', options: ['Que el átomo es mayormente espacio vacío', 'Que el átomo tiene una gran masa', 'Que los protones y neutrones están dispersos'], answer: 0 }, // Que el átomo es mayormente espacio vacío
      ],
    },
    'Sodio Metálico y Agua': {
      pre: [
        { question: '¿Qué sucede cuando el sodio metálico entra en contacto con agua?', options: ['Reacciona violentamente', 'Se disuelve lentamente', 'No reacciona', 'Se hunde y permanece inactivo'], answer: 0 }, // Reacciona violentamente
        { question: '¿Qué gas se libera cuando el sodio reacciona con agua?', options: ['Hidrógeno', 'Oxígeno', 'Nitrógeno', 'Helio'], answer: 0 }, // Hidrógeno
        { question: '¿Qué precaución es importante al manejar sodio metálico?', options: ['Evitar el contacto con agua', 'Almacenarlo al aire libre', 'Mantenerlo a alta temperatura', 'Sumergirlo en agua'], answer: 0 }, // Evitar el contacto con agua
      ],
      post: [
        { question: '¿Por qué el sodio reacciona violentamente con el agua?', options: ['Por su alta reactividad', 'Porque es un gas', 'Porque es un metal noble'], answer: 0 }, // Por su alta reactividad
        { question: '¿Cuál es el producto de la reacción entre sodio y agua?', options: ['Hidróxido de sodio e hidrógeno', 'Sulfato de sodio y oxígeno', 'Cloruro de sodio y agua'], answer: 0 }, // Hidróxido de sodio e hidrógeno
        { question: '¿Qué ocurre con la energía durante la reacción entre sodio y agua?', options: ['Se libera energía en forma de calor', 'Se absorbe energía', 'La energía permanece constante'], answer: 0 }, // Se libera energía en forma de calor
      ],
    },
    'Camaleón Químico': {
      pre: [
        { question: '¿Qué es un camaleón químico?', options: ['Una sustancia que cambia de color en diferentes condiciones', 'Un camaleón que reacciona a productos químicos', 'Un compuesto inerte'], answer: 0 }, // Una sustancia que cambia de color en diferentes condiciones
        { question: '¿Qué provoca el cambio de color en una reacción de camaleón químico?', options: ['Oxidación y reducción', 'Evaporación', 'Sublimación', 'Precipitación'], answer: 0 }, // Oxidación y reducción
        { question: '¿Qué se utiliza para observar el efecto camaleón químico?', options: ['Indicadores químicos', 'Sales metálicas', 'Ácidos fuertes', 'Bases fuertes'], answer: 0 }, // Indicadores químicos
      ],
      post: [
        { question: '¿Qué observación se puede hacer en una reacción de camaleón químico?', options: ['Cambio de color repetitivo', 'Producción de gas', 'Formación de un precipitado'], answer: 0 }, // Cambio de color repetitivo
        { question: '¿Qué papel juega el permanganato de potasio en el camaleón químico?', options: ['Actúa como agente oxidante', 'Actúa como agente reductor', 'Neutraliza el pH'], answer: 0 }, // Actúa como agente oxidante
        { question: '¿Cómo afecta la concentración de reactivos en el experimento?', options: ['Cambia la velocidad de la reacción', 'Aumenta la solubilidad', 'Evita la formación de productos'], answer: 0 }, // Cambia la velocidad de la reacción
      ],
    },
    'Lluvia Dorada': {
      pre: [
        { question: '¿Qué es la lluvia dorada en química?', options: ['La precipitación de yoduro de plomo', 'Un fenómeno atmosférico', 'Oxidación de metales'], answer: 0 }, // La precipitación de yoduro de plomo
        { question: '¿Qué solución se necesita para formar lluvia dorada?', options: ['Yoduro de potasio y nitrato de plomo', 'Cloruro de sodio y agua', 'Ácido clorhídrico y magnesio'], answer: 0 }, // Yoduro de potasio y nitrato de plomo
        { question: '¿Qué indica la formación de un precipitado dorado en esta reacción?', options: ['La formación de yoduro de plomo', 'Oxidación de hierro', 'Descomposición de agua oxigenada'], answer: 0 }, // La formación de yoduro de plomo
      ],
      post: [
        { question: '¿Qué compuesto es responsable de la "lluvia dorada"?', options: ['Yoduro de plomo', 'Sulfato de sodio', 'Cloruro de potasio'], answer: 0 }, // Yoduro de plomo
        { question: '¿Qué sucede con el precipitado al calentar la solución de lluvia dorada?', options: ['Se disuelve y se vuelve a formar al enfriarse', 'Se evapora completamente', 'Cambia de color'], answer: 0 }, // Se disuelve y se vuelve a formar al enfriarse
        { question: '¿Cómo se explica la coloración dorada del precipitado?', options: ['Por la formación de cristales de yoduro de plomo', 'Por la oxidación de metales', 'Por la reducción de iones metálicos'], answer: 0 }, // Por la formación de cristales de yoduro de plomo
      ],
    },
    'Identificación Ácido-base': {
      pre: [
        { question: '¿Qué indicador se utiliza comúnmente para identificar ácidos y bases?', options: ['Papel tornasol', 'Cloruro de sodio', 'Permanganato de potasio', 'Agua destilada'], answer: 0 }, // Papel tornasol
        { question: '¿Qué color indica un ácido en papel tornasol?', options: ['Rojo', 'Azul', 'Verde', 'Amarillo'], answer: 0 }, // Rojo
        { question: '¿Qué propiedad de las bases se puede identificar fácilmente?', options: ['Sabor amargo', 'Sabor dulce', 'Sabor ácido'], answer: 0 }, // Sabor amargo
      ],
      post: [
        { question: '¿Cómo cambia el papel tornasol en una solución básica?', options: ['De rojo a azul', 'De azul a rojo', 'Se desintegra'], answer: 0 }, // De rojo a azul
        { question: '¿Qué sucede al mezclar un ácido fuerte con una base fuerte?', options: ['Se neutralizan', 'Se vuelven más fuertes', 'No reaccionan'], answer: 0 }, // Se neutralizan
        { question: '¿Qué es una base según la teoría de Brønsted-Lowry?', options: ['Un donante de protones', 'Un aceptor de protones', 'Un catalizador'], answer: 1 }, // Un aceptor de protones
      ],
    },
    'Pasta de Dientes para Elefantes': {
      pre: [
        { question: '¿Qué reacción química se utiliza en la "pasta de dientes para elefantes"?', options: ['Decomposición de peróxido de hidrógeno', 'Oxidación de hierro', 'Neutralización de ácidos', 'Reducción de oxígeno'], answer: 0 }, // Decomposición de peróxido de hidrógeno
        { question: '¿Qué gas se libera en la reacción de la "pasta de dientes para elefantes"?', options: ['Oxígeno', 'Hidrógeno', 'Dióxido de carbono', 'Nitrógeno'], answer: 0 }, // Oxígeno
        { question: '¿Qué papel juega el jabón líquido en este experimento?', options: ['Atrapa el gas formando espuma', 'Cambia el color de la mezcla', 'Actúa como catalizador', 'Descompone el peróxido de hidrógeno'], answer: 0 }, // Atrapa el gas formando espuma
      ],
      post: [
        { question: '¿Qué se necesita para iniciar la reacción en la "pasta de dientes para elefantes"?', options: ['Un catalizador', 'Calor', 'Luz ultravioleta', 'Agitación'], answer: 0 }, // Un catalizador
        { question: '¿Qué observación principal se hace en este experimento?', options: ['La formación rápida de una gran cantidad de espuma', 'El cambio de color', 'La emisión de luz'], answer: 0 }, // La formación rápida de una gran cantidad de espuma
        { question: '¿Por qué se utiliza peróxido de hidrógeno en la "pasta de dientes para elefantes"?', options: ['Porque se descompone fácilmente liberando oxígeno', 'Porque reacciona lentamente con agua', 'Porque es un ácido fuerte'], answer: 0 }, // Porque se descompone fácilmente liberando oxígeno
      ],
    },
    'Solución conductora': {
      pre: [
        { question: '¿Qué sucede cuando un electrolito se disuelve en agua?', options: ['No ocurre nada.', 'Los iones se separan y conducen electricidad.', 'El agua se evapora.', 'Se forma una nueva sustancia.'], answer: 1 }, // Los iones se separan y conducen electricidad
        { question: '¿Cuál de las siguientes soluciones es la mejor conductora de electricidad?', options: ['Agua destilada.', 'Agua con sal.', 'Jugo de limón.', 'Agua de la llave.'], answer: 1 }, // Agua con sal
        { question: '¿Qué material necesitas para comprobar la conductividad de una solución?', options: ['Un termómetro.', 'Una bombilla y cables de cobre.', 'Un barómetro.', 'Un medidor de pH.'], answer: 1 }, // Una bombilla y cables de cobre
      ],
      post: [
        { question: '¿Qué relación existe entre la concentración de electrolitos y la conductividad eléctrica de una solución?', options: ['A mayor concentración, mayor conductividad.', 'A mayor concentración, menor conductividad.', 'No existe relación.', 'La concentración afecta solo la temperatura.'], answer: 0 }, // A mayor concentración, mayor conductividad
        { question: '¿Cuál de los siguientes compuestos no conduce electricidad cuando se disuelve en agua?', options: ['Agua destilada.', 'Agua con sal.', 'Jugo de limón.', 'Agua de la llave.'], answer: 0 }, // Agua destilada
        { question: '¿Por qué el cobre se utiliza en los cables para comprobar la conductividad?', options: ['Porque es un buen conductor de electricidad.', 'Porque es económico.', 'Porque es un material aislante.', 'Porque cambia de color al pasar corriente.'], answer: 0 }, // Porque es un buen conductor de electricidad
      ],
    },
  };
  

  const shuffleOptions = (question) => {
    const options = [...question.options];
    const correctAnswer = options[question.answer];
    for (let i = options.length - 1; i > 0; i--) {
      const j = Math.floor(Math.random() * (i + 1));
      [options[i], options[j]] = [options[j], options[i]];
    }
    const newAnswerIndex = options.indexOf(correctAnswer);
    return { ...question, options, answer: newAnswerIndex };
  };

  const [shuffledQuizData, setShuffledQuizData] = useState([]);
  const [answers, setAnswers] = useState([]);
  const [isSubmitDisabled, setIsSubmitDisabled] = useState(true);
  const [showResult, setShowResult] = useState(false);
  const [correctCount, setCorrectCount] = useState(0);
  const [incorrectCount, setIncorrectCount] = useState(0);

  useEffect(() => {
    const initialShuffledQuizData = quizData[experimentName][quizType].map(shuffleOptions);
    setShuffledQuizData(initialShuffledQuizData);
    setAnswers(Array(initialShuffledQuizData.length).fill(null)); // Inicializa el estado de respuestas con valores nulos
  }, [experimentName, quizType]);

  useEffect(() => {
    // Verifica si todas las preguntas han sido respondidas
    const allAnswered = answers.every(answer => answer !== null);
    setIsSubmitDisabled(!allAnswered);
  }, [answers]);

  const handleOptionChange = (questionIndex, optionIndex) => {
    const newAnswers = [...answers];
    newAnswers[questionIndex] = optionIndex;
    setAnswers(newAnswers);
  };

  const handleSubmit = async () => {
    let correct = 0;
    let incorrect = 0;
    const detail = [];

    answers.forEach((answer, index) => {
        if (answer === shuffledQuizData[index].answer) {
            correct++;
            detail.push(1);
        } else {
            incorrect++;
            detail.push(0);
        }
    });

    setCorrectCount(correct);
    setIncorrectCount(incorrect);
    setShowResult(true);

    const quizIndex = Object.keys(quizData).indexOf(experimentName) + 1;
    const type = quizType === 'pre' ? 'pre' : 'post';
    const values = detail.join('');

    try {
        // Llamada al backend para actualizar el quiz
        await fetch(`${config.backendUrl}/updateQuiz/${user.username}/quiz${quizIndex}/${type}/${values}`, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json',
            },
        });

        // Determinar el índice correcto para `progressdata`
        const progressIndex = quizIndex * 5 - (type === 'pre' ? 1 : 0);

        // Llamada al backend para actualizar el progreso del usuario
        await fetch(`${config.backendUrl}/updateStudent/${user.username}/prog/${progressIndex}`, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json',
            },
        });
    } catch (err) {
        console.error('Error al actualizar el quiz o progreso:', err);
    }
};

  return (
    <div className="quiz-page-container page-container">
      <button className="btn-back" onClick={() => navigate(-1)}>Volver</button>
      <div className="quiz">
        <h1>{quizType === 'pre' ? 'Pre Cuestionario' : 'Post Cuestionario'}: {experimentName}</h1>
        {shuffledQuizData.map((q, qi) => (
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
        <button className="btn-submit quiz-submit" onClick={handleSubmit} disabled={isSubmitDisabled}>
          Enviar
        </button>
      </div>
      {showResult && (
        <div className="result-popup">
          <h2>Resultado</h2>
          <p>Correctas: {correctCount}</p>
          <p>Incorrectas: {incorrectCount}</p>
          <button onClick={() => navigate(`/experiment/${experimentName}`)}>Aceptar</button>
        </div>
      )}
    </div>
  );
};

export default QuizPage;