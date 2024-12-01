import React from 'react';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import Navbar from './components/Navbar';
import MoviesPage from './pages/Movies/MoviesPage';
import SeancesPage from './pages/Seances/SeancesPage';
import TestPage from './pages/TestPage';
import RegisterPage from './pages/Account/RegisterPage';
import { ToastContainer } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import './styles/globals.css';
import LoginPage from './pages/Account/LoginPage';
import { AuthProvider } from './contexts/AuthContext';


const App: React.FC = () => {
    return (
        <AuthProvider>
            <Router>
                <Navbar />
                <div>
                    <Routes>
                        <Route path='/' element={<MoviesPage />} />
                        <Route path="/movies" element={<MoviesPage />} />
                        <Route path="/seances" element={<SeancesPage />} />
                        <Route path="/register" element={<RegisterPage />} />
                        <Route path="/login" element={<LoginPage />} />
                        <Route path="/test" element={<TestPage />} />
                    </Routes>
                </div>
            </Router>
            <ToastContainer
                position='bottom-right'
                theme='dark'
            />
        </AuthProvider>
    );
};

export default App;
