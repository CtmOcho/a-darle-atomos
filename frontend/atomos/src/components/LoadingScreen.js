import React, { useEffect, useRef } from 'react';
import loading from '../media/atomo.png'; // Asegúrate de que la ruta sea correcta
import autoAnimate from '@formkit/auto-animate';
import './LoadingScreen.css'; // Opcional: estilos para la pantalla de carga

const LoadingScreen = ({ onLoaded }) => {
  const parentRef = useRef(null);

  // Inicializa autoAnimate sobre el ref del contenedor
  useEffect(() => {
    if (parentRef.current) {
      autoAnimate(parentRef.current);
    }
  }, [parentRef]);

  // Simula el fin de la carga cuando la imagen de carga esté lista
  const handleLoaded = () => {
    if (onLoaded) {
      onLoaded();
    }
  };

  return (
    <div ref={parentRef} className="loading-screen">
      <div className='loading-content'>
        <img src={loading} alt="Loading logo" className="loading-logo" onLoad={handleLoaded} />
        <p className='loading-text'>Cargando...</p>
      </div>
    </div>
  );
};

export default LoadingScreen;
