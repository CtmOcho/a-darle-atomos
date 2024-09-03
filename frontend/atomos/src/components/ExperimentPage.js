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
const labInstructions = {
  'Color a la Llama': [
    'Este experimento muestra cómo diferentes metales emiten colores al ser calentados, ayudando a entender el modelo atómico de Bohr.',
    'Objetivos de aprendizaje:',
    '1. Comprender cómo los átomos emiten luz cuando se calientan.',
    '2. Identificar los colores emitidos por calcio, potasio, sodio, cobre y plomo.',
    '3. Usar el color para identificar diferentes elementos.',
    'Procedimiento en el laboratorio virtual:',
    '1. Enciende el mechero virtual en Unity.',
    '2. Humedece la varilla en ácido clorhídrico virtual.',
    '3. Toca un elemento (calcio, potasio, sodio, cobre o plomo) con la varilla.',
    '4. Observa y anota el color que aparece en la llama.'
  ],
  
  'Sublimación del Yodo Sólido': [
    'Este experimento muestra cómo el yodo sólido se convierte en gas sin pasar por el estado líquido, ayudándonos a entender los cambios de estado de la materia.',
    'Objetivos de aprendizaje:',
    '1. Comprender qué es la sublimación y cómo cambia el estado de la materia.',
    '2. Observar cuándo el yodo comienza a sublimar al calentarse.',
    '3. Aprender cómo la sublimación se aplica en la vida diaria y en la industria.',
    'Procedimiento en el laboratorio virtual:',
    '1. Enciende el mechero virtual y observa cómo sube la temperatura.',
    '2. Observa el yodo sólido y nota cuándo comienza a sublimar, es decir, a convertirse en gas.',
    '3. Toma notas sobre la temperatura en la que ocurre la sublimación y cualquier cambio que observes en el yodo.'
  ],
  
  'Experimento de Destilación': [
    'Este experimento enseña cómo separar componentes de una mezcla homogénea, como el agua y el etanol, utilizando sus diferentes puntos de ebullición.',
    'Objetivos de aprendizaje:',
    '1. Entender cómo la destilación separa líquidos en una mezcla homogénea.',
    '2. Aprender a usar el equipo de destilación, incluyendo el calentador y el condensador.',
    'Procedimiento en el laboratorio virtual:',
    '1. Enciende el calentador virtual para calentar la mezcla de agua y etanol.',
    '2. Observa cómo la temperatura sube hasta que el etanol empieza a destilarse.',
    '3. Mira cómo el etanol se vaporiza, se enfría en el condensador, y se separa del agua.',
    '4. Toma notas sobre la temperatura en la que ocurre la destilación del etanol.'
  ],
  
  'Ley de Gases': [
    'Este experimento explora cómo la presión y el volumen de un gas cambian cuando varía la temperatura, siguiendo la Ley de los Gases Ideales.',
    'Objetivos de aprendizaje:',
    '1. Comprender cómo la temperatura afecta la presión y el volumen de un gas.',
    '2. Aplicar la Ley de los Gases Ideales para predecir estos cambios.',
    'Procedimiento en el laboratorio virtual:',
    '1. Observa cómo la temperatura y la presión cambian a medida que se calienta el líquido en el laboratorio virtual.',
    '2. Toma notas sobre cómo estos cambios se relacionan con la Ley de los Gases Ideales.'
  ],
  
  'Experimento de Rutherford': [
    'Propósito: Este experimento tiene como objetivo reproducir, de manera simplificada, el famoso experimento de dispersión de Rutherford, que llevó al descubrimiento del núcleo atómico.',
    'Objetivos de aprendizaje:',
    '1. Entender el modelo atómico de Rutherford y su importancia en la historia de la ciencia.',
    '2. Analizar los resultados del experimento original y sus implicaciones.',
    '3. Comprender la estructura interna del átomo y la distribución de carga.'
  ],
  'Sodio Metálico y Agua': [
    'Propósito: Este experimento ilustra la reacción altamente exotérmica entre el sodio metálico y el agua, demostrando la reactividad de los metales alcalinos.',
    'Objetivos de aprendizaje:',
    '1. Observar la reactividad de los metales alcalinos con el agua.',
    '2. Comprender los conceptos de reacciones exotérmicas y liberación de gas hidrógeno.',
    '3. Analizar las precauciones de seguridad necesarias al manejar sustancias altamente reactivas.'
  ],
  'Camaleón Químico': [
    'Propósito: El experimento del camaleón químico demuestra la oscilación de colores en una solución debido a reacciones redox sucesivas, ilustrando principios de cinética química y equilibrio.',
    'Objetivos de aprendizaje:',
    '1. Comprender las reacciones de óxido-reducción (redox).',
    '2. Observar cómo las condiciones de reacción afectan la velocidad de cambio de color.',
    '3. Analizar el concepto de reacción oscilante en química.'
  ],
  'Lluvia Dorada': [
    'Propósito: Este experimento muestra la precipitación de yoduro de plomo en una solución, que produce un precipitado dorado, simulando la "lluvia dorada".',
    'Objetivos de aprendizaje:',
    '1. Comprender el concepto de reacciones de precipitación.',
    '2. Observar la formación de cristales y la influencia de la concentración en la precipitación.',
    '3. Analizar las propiedades del yoduro de plomo y su comportamiento en solución.'
  ],
  'Identificación Ácido-base': [
    'Propósito: Este experimento busca enseñar cómo identificar ácidos y bases utilizando indicadores de pH y observar cómo cambian de color en diferentes medios.',
    'Objetivos de aprendizaje:',
    '1. Aprender a usar indicadores de pH para identificar ácidos y bases.',
    '2. Comprender la teoría de ácidos y bases de Brønsted-Lowry.',
    '3. Observar los cambios de color de los indicadores y relacionarlos con el pH de la solución.'
  ],
  'Pasta de Dientes para Elefantes': [
    'Propósito: Este experimento demuestra una reacción de descomposición rápida del peróxido de hidrógeno que resulta en la producción de grandes cantidades de espuma.',
    'Objetivos de aprendizaje:',
    '1. Comprender la reacción de descomposición del peróxido de hidrógeno.',
    '2. Observar el papel de un catalizador en acelerar una reacción química.',
    '3. Analizar las aplicaciones de reacciones similares en la industria química.'
  ],
  'Solución conductora': [
    'Propósito: Este experimento examina la conductividad eléctrica de diferentes soluciones, mostrando cómo la presencia de iones influye en la capacidad de una solución para conducir electricidad.',
    'Objetivos de aprendizaje:',
    '1. Comprender el concepto de conductividad eléctrica en soluciones.',
    '2. Identificar qué tipos de compuestos (electrolitos) conducen electricidad en solución.',
    '3. Analizar la relación entre la concentración de iones en una solución y su conductividad.'
  ],
};

// Obtener el índice del experimento actual
const experimentIndex = carouselItems.indexOf(experimentName);
const progressIndex = experimentIndex * 5; // El primer elemento de cada grupo de 5

useEffect(() => {
  // Llamar al backend para verificar el progreso del estudiante
  const checkPreQuizCompletion = async () => {
    try {
      const response = await fetch(`${config.backendUrl}/getStudent/${user.username}/prog/${progressIndex + 1}`, {
        headers: {
          'ngrok-skip-browser-warning': 'true',  // Añadir este encabezado
        },
      });
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
  <div className="page-container col-12 ">
            <nav className="navbar col-12">
    <button className="btn-back" onClick={() => navigate('/dashboard')}>Volver</button>
  </nav>
    <div className="experiment-page-container col-lg-8 col-xs-12 col-md-10 col-sm-10 col-xl-8 col-xxl-6 justify-content-center">
      <div className='row col-12'>

      <h1 className='display-1'>{experimentName}</h1>
      </div>
      {labInstructions[experimentName] && labInstructions[experimentName].length > 0 && (
  <div className="experiment-instructions col-12">
    {labInstructions[experimentName].map((instruction, index) => {
      const isHeading = instruction === 'Objetivos de aprendizaje:' || instruction === 'Procedimiento en el laboratorio virtual:';
      return (
        <p key={index} className={isHeading ? 'display-6 text-center' : 'fs-4 text-justify'}>
          {instruction}
        </p>
      );
    })}
  </div>
)}

      <div className="quiz-buttons row col-12">
        <button className="btn col-3 p-2 m-1.5" onClick={handleAdditionalContentClick}>Contenido Adicional</button>
        <button className="btn col-3 p-2 m-1.5" onClick={handlePreQuizClick}>Pre Cuestionario</button>
        <button className="btn col-3 p-2 m-1.5 " onClick={handlePostQuizClick} disabled={!preQuizCompleted}>Post Cuestionario</button>
      </div>
    </div>
  </div>
);
};

export default ExperimentPage;
