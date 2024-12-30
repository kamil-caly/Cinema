// src/components/MovieCard.tsx

import React from 'react';
import { useNavigate } from 'react-router-dom';
import { MovieDto } from '../pages/Movies/MoviesPageTypes';

interface MovieCardProps {
    movie: MovieDto;
}

const MovieCard: React.FC<MovieCardProps> = ({ movie }) => {
    const navigate = useNavigate();

    return (
        <div className="bg-cinemaBgSecondary p-6 rounded-lg shadow-md transition-all duration-300 transform hover:scale-105 cursor-pointer"
            onClick={() => navigate(`/seances?title=${movie.title}`)}>
            <img
                src={movie.imageUrl}
                alt={movie.title}
                className="w-full h-movieImage object-fill rounded-lg mb-4"
            />
            <h2 className="text-2xl font-semibold mb-2">{movie.title}</h2>
            <p className="text-cinemaTextGrayStrong text-sm mb-2">{movie.durationInMin} min</p>
            <p className="text-cinemaTextGrayLight">{movie.description}</p>
        </div>
    );
};

export default MovieCard;
