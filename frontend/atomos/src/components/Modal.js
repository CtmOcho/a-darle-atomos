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
        <div className="modal-buttons col-12">
          <button className="btn-back col-5 " onClick={onClose}>Cancelar</button>
          <div className="col-2" >
          </div>
          <button className="btn fs-5 col-5 btn-modal-confirm" onClick={handleNavigate}>Ir a experimento</button>
        </div>
      </div>
    </div>
  );
};

export default Modal;
