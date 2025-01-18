// src/pages/Movies.tsx

import React, { useEffect, useState } from 'react';
import { FetchError, Get } from '../../services/BaseApi';
import MovieCard from '../../components/MovieCard';
import { MovieDto } from './MoviesPageTypes';
import { toast } from 'react-toastify';


const MoviesPage: React.FC = () => {
    const API_URL = process.env.REACT_APP_API_URL ?? '';
    const [movies, setMovies] = useState<MovieDto[]>([]);

    useEffect(() => {
        const fetchMovies = async () => {
            if (API_URL) {
                try {
                    const data = await Get<MovieDto[]>(API_URL, '/movie/getAll');
                    setMovies(data);
                } catch (error) {
                    const fetchError = error as FetchError;
                    toast.error('Fetch error occurred: ' + fetchError.body);
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
