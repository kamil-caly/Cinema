// src/components/MovieCard.tsx

import React from 'react';
import { MovieDto } from '../types/Movie';
import { useNavigate } from 'react-router-dom';

interface MovieCardProps {
    movie: MovieDto;
}

const MovieCard: React.FC<MovieCardProps> = ({ movie }) => {
    const navigate = useNavigate();

    return (
        <div className="bg-cMainBg p-6 rounded-lg shadow-md transition-all duration-300 transform hover:scale-105 hover:bg-cMainBgHover cursor-pointer"
            onClick={() => navigate(`/seances?title=${movie.title}`)}>
            <img
                src={movie.imageUrl}
                alt={movie.title}
                className="w-full h-movieImage object-fill rounded-lg mb-4"
            />
            <h2 className="text-2xl font-semibold mb-2">{movie.title}</h2>
            <p className="text-gray-400 text-sm mb-2">{movie.durationInMin} min</p>
            <p className="text-gray-300">{movie.description}</p>
        </div>
    );
};

export default MovieCard;
