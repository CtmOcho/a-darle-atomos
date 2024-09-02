import React from 'react';

const Question = ({ question, questionIndex, selectedOption, onSelectOption }) => {
  return (
    <div className="question-container col-12 display-7">
      <p className='display-6 text-center'>{question.question}</p>
      <div className="options-container col-12 justify-content-center align-items-start fs-4">
        <div className="row col-12">
          {question.options.map((option, index) => (
            <div key={index} className="col-6 p-2">
              <label className="quiz-label fs-4">
                <input
                  type="radio"
                  name={`question-${questionIndex}`}
                  value={index}
                  checked={selectedOption === index}
                  onChange={() => onSelectOption(index)}
                  className="me-2 text-start"
                />
                {option}
              </label>
            </div>
          ))}
        </div>
      </div>
    </div>
  );
};

export default Question;
