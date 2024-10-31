import React from 'react';
import './Question.css';

const Question = ({ question, questionIndex, selectedOption, onSelectOption }) => {
  return (
    <div className="container mb-5 question-container">
      <div className="row">
        <div className="col-12">
          <p className="fw-bold text-center">{questionIndex + 1}. {question.question}</p>
          <div className="options-container">
            {question.options.map((option, index) => (
              <div key={index} className="col-md-6">
                <input
                  type="radio"
                  id={`question-${questionIndex}-option-${index}`}
                  name={`question-${questionIndex}`}
                  value={index}
                  checked={selectedOption === index}
                  onChange={() => onSelectOption(index)}
                  className="radio-input"
                />
                <label
                  htmlFor={`question-${questionIndex}-option-${index}`}
                  className={`box ${index === 0 ? 'first' : ''} ${index === 1 ? 'second' : ''} ${index === 2 ? 'third' : ''} ${index === 3 ? 'fourth' : ''}`}
                >
                  <div className="course">
                    <span className="circle"></span>
                    <span className="subject">{option}</span>
                  </div>
                </label>
              </div>
            ))}
          </div>
        </div>
      </div>
    </div>
  );
};

export default Question;
