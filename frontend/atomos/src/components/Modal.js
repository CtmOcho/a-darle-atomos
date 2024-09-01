import React from 'react';
import './Modal.css';

const Modal = ({ experiment, onClose, onNavigate }) => {
  const handleNavigate = () => {
    onNavigate(experiment.title);
  };

  return (
    <div className="modal-overlay">
      <div className="modal-content">
        <h1 className="display-1">{experiment.title}</h1>
        <img 
          src={experiment.image} 
          alt={experiment.title} 
          className="img-fluid"
        />
        <p>Aquí va el contenido detallado del experimento.</p>
        <div className="modal-buttons">
          <button className="btn-sec " onClick={onClose}>Cancelar</button>
          <button className="btn-prim " onClick={handleNavigate}>Ir a experimento</button>
        </div>
      </div>
    </div>
  );
};

export default Modal;
