import React from 'react';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import Navbar from './components/Navbar';

import MoviesPage from './pages/Movies/MoviesPage';
import SeancesPage from './pages/Seances/SeancesPage';

const App: React.FC = () => {
  return (
    <Router>
      <Navbar />
      <div>
        <Routes>
          <Route path='/' element={<MoviesPage />} />
          <Route path="/movies" element={<MoviesPage />} />
          <Route path="/seances" element={<SeancesPage />} />
        </Routes>
      </div>
    </Router>
  );
};

export default App;
