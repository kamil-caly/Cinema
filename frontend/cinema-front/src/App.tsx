import React from 'react';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import Navbar from './components/Navbar';

import MoviesPage from './pages/Movies/MoviesPage';
import SeancesPage from './pages/Seances/SeancesPage';
import TestPage from './pages/TestPage';

const App: React.FC = () => {
  return (
    <Router>
      <Navbar />
      <div>
        <Routes>
          <Route path='/' element={<MoviesPage />} />
          <Route path="/movies" element={<MoviesPage />} />
          <Route path="/seances" element={<SeancesPage />} />
          <Route path="/test" element={<TestPage />} />
        </Routes>
      </div>
    </Router>
  );
};

export default App;
