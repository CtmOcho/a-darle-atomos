import React from 'react';
import ReactDOM from 'react-dom/client';
import './index.css';
import App from './App';
import reportWebVitals from './reportWebVitals';
import 'bootstrap/dist/css/bootstrap.min.css';
import config from './config/config';

// Función para leer el archivo de texto y actualizar el config
async function fetchConfigAndRenderApp() {
  try {
    const response = await fetch(`${process.env.PUBLIC_URL}/config.txt`);
    const text = await response.text();

    // Actualiza el valor de backendUrl en config
    config.backendUrl = text.trim();

    // Renderiza la aplicación después de actualizar el config
    const root = ReactDOM.createRoot(document.getElementById('root'));
    root.render(
      <React.StrictMode>
        <App />
      </React.StrictMode>
    );
  } catch (error) {
    console.error('Error leyendo el archivo de configuración:', error);
  }
}

// Llama a la función para leer el archivo y renderizar la app
fetchConfigAndRenderApp();

// Si quieres medir el rendimiento en tu aplicación, pasa una función
// para registrar los resultados (por ejemplo: reportWebVitals(console.log))
// o envíalos a un endpoint de análisis. Aprende más: https://bit.ly/CRA-vitals
reportWebVitals();
