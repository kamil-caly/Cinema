import React from 'react';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import Navbar from './components/Navbar';

import MoviesPage from './pages/Movies/MoviesPage';
import SeancesPage from './pages/Seances/Seances';

const App: React.FC = () => {
  return (
    <Router>
      <Navbar />
      <div className='pt-16'> {/* Dodajemy odstęp od góry dla treści */}
        <Routes>
          <Route path="/movies" element={<MoviesPage />} />
          <Route path="/seances" element={<SeancesPage />} />
        </Routes>
      </div>
    </Router>
  );
};

export default App;
