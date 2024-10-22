import React from 'react';
import './Modal.css';

const Modal = ({ experiment, onClose, onNavigate }) => {
  const handleNavigate = () => {
    onNavigate(experiment.title);
  };

  return (
    <div className="modal-overlay ">
      <div className="modal-content justify-content-center text-center">
        <h1 className="display-1 pt-5">{experiment.title}</h1>
        <img 
          src={experiment.image} 
          alt={experiment.title} 
          className="img-fluid"
        />
        <div className="modal-buttons col-12">
          <button className="btn-back-modal col-lg-5 col-xxl-5 col-xl-5 col-12  " onClick={onClose}>Cancelar</button>
          <div className='row col-lg-2 col-xl-2 col-xxl-2 col-12 separation-col'>
          </div>
          <button className="col-lg-5 col-xxl-5 col-xl-5 col-12 btn-modal-confirm" onClick={handleNavigate}>Ir a experimento</button>
        </div>
      </div>
    </div>
  );
};

export default Modal;
