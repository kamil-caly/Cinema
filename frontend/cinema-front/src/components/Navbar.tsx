// src/components/Navbar.tsx
import React from 'react';
import { NavLink } from 'react-router-dom';

const Navbar: React.FC = () => {
    return (
        <nav className="fixed top-0 z-50 w-full bg-cinemaBgTertiary text-cinemaTextPrimary h-navbar flex items-center border-b-[1px] border-cinemaBorderSecondary">
            <div className='flex justify-between w-full'>
                <div className="flex">
                    {/* Ikonka kina */}
                    <NavLink to="/movies" className="text-cinemaTextSecondary text-2xl font-bold ms-5 p-2">
                        🎬 Cinema
                    </NavLink>

                    {/* Zakładki nawigacyjne */}
                    <NavLink
                        to="/movies"
                        className={({ isActive }) =>
                            `text-xl mt-1 p-2 ms-5 rounded-full hover:bg-cinemaHoverPrimary ${isActive ? 'text-cinemaTextSecondary bg-cinemaHoverSecondary' : ''
                            }`
                        }
                    >
                        Movies
                    </NavLink>

                    <NavLink
                        to="/seances"
                        className={({ isActive }) =>
                            `text-xl mt-1 p-2 ms-5 rounded-full hover:bg-cinemaHoverPrimary ${isActive ? 'text-cinemaTextSecondary bg-cinemaHoverSecondary' : ''
                            }`
                        }
                    >
                        Seances
                    </NavLink>
                </div>
                <div className='flex'>
                    <NavLink
                        to="/login"
                        className={({ isActive }) =>
                            `text-xl mt-1 p-2 me-5 rounded-full hover:bg-cinemaHoverPrimary ${isActive ? 'text-cinemaTextSecondary bg-cinemaHoverSecondary' : ''
                            }`
                        }
                    >
                        Login
                    </NavLink>
                    <NavLink
                        to="/register"
                        className={({ isActive }) =>
                            `text-xl mt-1 p-2 me-5 rounded-full hover:bg-cinemaHoverPrimary ${isActive ? 'text-cinemaTextSecondary bg-cinemaHoverSecondary' : ''
                            }`
                        }
                    >
                        Register
                    </NavLink>
                </div>
            </div>
        </nav>
    );
};

export default Navbar;
