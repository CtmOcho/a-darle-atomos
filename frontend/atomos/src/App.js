import React from 'react';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import HomePage from './components/HomePage';
import RegisterPage from './components/RegisterPage';
import LoginPage from './components/LoginPage';
import DashboardPage from './components/DashboardPage';
import ProfilePage from './components/ProfilePage';
import ExperimentPage from './components/ExperimentPage';


import { UserProvider } from './context/UserContext';
import AdditionalContentPage from './components/AdditionalContentPage';
import QuizPage from './components/QuizPage';
import DeleteProfilePage from './components/DeleteProfilePage';
import EditProfilePage from './components/EditProfilePage';
import ProgressPage from './components/ProgressPage';
import TeacherProgressCourse from './components/TeacherProgressCourse';
import ProfessorToolsPage from './components/ProfessorToolsPage';
import './App.css';

function App() {
  return (
    <UserProvider>
      <Router>
        <div className="App">
          <Routes>
            <Route exact path="/" element={<HomePage />} />
            <Route path="/register" element={<RegisterPage />} />
            <Route path="/login" element={<LoginPage />} />
            <Route path="/dashboard" element={<DashboardPage />} />
            <Route path="/profile" element={<ProfilePage />} />
            <Route path="/experiment/:experimentName" element={<ExperimentPage />} />

            
            <Route path="/experiment/:experimentName/additional-content" element={<AdditionalContentPage />} />
            <Route path="/quiz/:experimentName/:quizType" element={<QuizPage />} />
            <Route path="/delete-profile" element={<DeleteProfilePage />} />
            <Route path="/edit-profile" element={<EditProfilePage />} />
            <Route path="/progress-detail/:username" element={<ProgressPage />} />
            <Route path="/check-student-course/:courseName" element={<TeacherProgressCourse />} />
            <Route path="/professorTools" element={<ProfessorToolsPage />} />
          </Routes>
        </div>
      </Router>
    </UserProvider>
  );
}

export default App;
