// src/components/Navbar.tsx
import React from 'react';
import { NavLink } from 'react-router-dom';

const Navbar: React.FC = () => {
  return (
    <nav className="bg-cMainBg p-4 fixed top-0 left-0 w-full z-10 shadow-lg">
      <div className="mx-auto flex items-center px-5 space-x-8">
        {/* Ikonka kina */}
        <NavLink to="/movies" className="text-cMainText text-2xl font-bold">
          🎬 Cinema
        </NavLink>

        {/* Zakładki nawigacyjne */}
        <NavLink
          to="/movies"
          className={({ isActive }) =>
            `text-cMainText text-xl pt-1 hover:text-cMainTextHover transition-colors duration-200 ${
              isActive ? 'text-cMainTextHover underline underline-offset-4' : ''
            }`
          }
        >
          Movies
        </NavLink>
        
        <NavLink
          to="/seances"
          className={({ isActive }) =>
            `text-cMainText text-xl pt-1 hover:text-cMainTextHover transition-colors duration-200 ${
              isActive ? 'text-cMainTextHover underline underline-offset-4' : ''
            }`
          }
        >
          Seances
        </NavLink>
      </div>
    </nav>
  );
};

export default Navbar;
