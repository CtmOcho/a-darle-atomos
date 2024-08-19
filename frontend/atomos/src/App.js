import React from 'react';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import HomePage from './components/HomePage';
import RegisterPage from './components/RegisterPage';
import LoginPage from './components/LoginPage';
import DashboardPage from './components/DashboardPage';
import ProfilePage from './components/ProfilePage';
import ExperimentPage from './components/ExperimentPage';
import EditCoursesPage from './components/EditCoursesPage';
import CreateCoursePage from './components/CreateCoursePage';
import ModifyCoursePage from './components/ModifyCoursePage';
import AddStudentPage from './components/AddStudentPage';
import RemoveStudentPage from './components/RemoveStudentPage';
import DeleteCoursePage from './components/DeleteCoursePage';
import { UserProvider } from './context/UserContext';
import AdditionalContentPage from './components/AdditionalContentPage';
import QuizPage from './components/QuizPage';
import DeleteProfilePage from './components/DeleteProfilePage';
import EditProfilePage from './components/EditProfilePage';
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
            <Route path="/edit-courses" element={<EditCoursesPage />} />
            <Route path="/create-course" element={<CreateCoursePage />} />
            <Route path="/modify-course" element={<ModifyCoursePage />} />
            <Route path="/add-student/:courseName" element={<AddStudentPage />} />
            <Route path="/remove-student/:courseName" element={<RemoveStudentPage />} />
            <Route path="/delete-course" element={<DeleteCoursePage />} />
            <Route path="/experiment/:experimentName/additional-content" element={<AdditionalContentPage />} />
            <Route path="/quiz" element={<QuizPage />} />
            <Route path="/delete-profile" element={<DeleteProfilePage />} />
            <Route path="/edit-profile" element={<EditProfilePage />} />
          </Routes>
        </div>
      </Router>
    </UserProvider>
  );
}

export default App;
