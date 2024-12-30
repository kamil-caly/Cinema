// src/pages/Movies.tsx

import React, { useEffect, useState } from 'react';
import { Get } from '../../services/BaseApi';
import config from '../../app_config.json';
import MovieCard from '../../components/MovieCard';
import { MovieDto } from './MoviesPageTypes';


const MoviesPage: React.FC = () => {
    const API_URL = config.API_URL;
    const [movies, setMovies] = useState<MovieDto[]>([]);

    useEffect(() => {
        const fetchMovies = async () => {
            if (API_URL) {
                try {
                    const data = await Get<MovieDto[]>(API_URL, '/movie/getAll');
                    setMovies(data);
                } catch (error) {
                    console.error('Error fetching movies:', error);
                }
            }
        };

        fetchMovies();
    }, []);

    return (
        <div className="bg-cinemaBgPrimary text-cinemaTextPrimary min-h-screen p-8 pt-pageTopPadding">
            <div className="grid gap-12 sm:grid-cols-1 md:grid-cols-2 lg:grid-cols-3  xl:grid-cols-4">
                {movies.map((movie) => (
                    <MovieCard key={movie.title} movie={movie} />
                ))}
            </div>
        </div>
    );
};

export default MoviesPage;
