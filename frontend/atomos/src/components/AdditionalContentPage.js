import React from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import './AdditionalContentPage.css';
import image1 from '../media/bohr.png';
import image2 from '../media/fuegos-artificiales-san-juan-y-san-pedro-leon-2019.png';
import image3 from '../media/Heisenberg.png';

const AdditionalContentPage = () => {
    const navigate = useNavigate();
    const { experimentName } = useParams();
  
    // Diccionario con el contenido adicional por experimento
    const additionalContent = {
      'Color a la Llama': [
        {
          imgSrc: image1,
          description: 'Los principales problemas del modelo atómico de Rutherford eran la incompatibilidad con las leyes electromagnéticas, el movimiento de los electrones en torno al núcleo y las inexplicables líneas espectrales observadas en los átomos.'
        },
        {
          imgSrc: image2,
          description: 'uso en la vida real: Identificación de elementos metálicos en muestras desconocidas. Análisis cualitativo de compuestos metálicos. Control de calidad en la industria, especialmente en la fabricación de productos químicos. Estudios arqueológicos y geológicos. En los fuegos artificiales'
        },
        {
          imgSrc: image3,
          description: 'En el episodio piloto de la serie televisiva Breaking Bad, el profesor Walt White enseña a los alumnos cómo cambia el color de la llama de un mecharo Bunsen cuando la rocía con distintas disoluciones de productos químicos.'
        },
      ],
      'Sublimación del Yodo Sólido': [
        {
          imgSrc: 'ruta_de_imagen4.png',
          description: 'Descripción del contenido 1 para Sublimación del Yodo Sólido...'
        },
        {
          imgSrc: 'ruta_de_imagen5.png',
          description: 'Descripción del contenido 2 para Sublimación del Yodo Sólido...'
        },
        {
          imgSrc: 'ruta_de_imagen6.png',
          description: 'Descripción del contenido 3 para Sublimación del Yodo Sólido...'
        },
      ],
      // Agrega más experimentos aquí...
    };
  
    // Obtener el contenido basado en el nombre del experimento
    const content = additionalContent[experimentName] || [];
  
    return (
      <div className="additional-content-page-container">
        <button className="btn-back" onClick={() => navigate(-1)}>Volver</button>
        <h1>Contenido Adicional: {experimentName}</h1>
        <div className="content-grid">
          {content.map((item, index) => (
            <div key={index} className="content-item">
              <img src={item.imgSrc} alt={`Contenido ${index + 1}`} />
              <p>{item.description}</p>
            </div>
          ))}
        </div>
      </div>
    );
  };
  
  export default AdditionalContentPage;
