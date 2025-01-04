import React, { createContext, useContext, useState, ReactNode, Dispatch, SetStateAction, useEffect, useReducer } from 'react';
import { useLocalStorage } from '../hooks/useLocalStorage';
import config from '../app_config.json';
import { FetchError, Get } from '../services/BaseApi';
import { UserDataDto } from '../types/Other';
import { toast } from 'react-toastify';

type AuthState = {
    isLogged: boolean;
    userDto?: UserDataDto;
}

type AuthAction =
    | { type: 'LOGIN' }
    | { type: 'LOGOUT' }
    | { type: 'SET_USER_DATA', content: UserDataDto };

type AuthContextType = {
    state: AuthState;
    dispatch: React.Dispatch<AuthAction>;
};

const initialState: AuthState = {
    isLogged: false
};

function authReducer(state: AuthState, action: AuthAction): AuthState {
    switch (action.type) {
        case 'LOGIN':
            return { ...state, isLogged: true, userDto: undefined };
        case 'LOGOUT':
            return { ...state, isLogged: false, userDto: undefined };
        case 'SET_USER_DATA':
            return { ...state, isLogged: true, userDto: action.content };
        default:
            return state;
    }
}

const AuthContext = createContext<AuthContextType | null>(null);

export const AuthProvider = ({ children }: { children: ReactNode }) => {
    const API_URL = config.API_URL;
    const [getLSValue, setLsValue] = useLocalStorage();
    const [state, dispatch] = useReducer(authReducer, initialState);

    const handleLogout = () => {
        dispatch({ type: 'LOGOUT' });
        setLsValue('token', '');
    };

    useEffect(() => {
        const token: string = getLSValue('token') ?? '';
        if (token) {
            const fetchUserDataDto = async () => {
                if (API_URL) {
                    try {
                        const data = await Get<UserDataDto>(API_URL, '/account/getUserData', { params: { token: token } });
                        if (data !== null) {
                            dispatch({ type: 'SET_USER_DATA', content: data });
                        }
                    } catch (error) {
                        const fetchError = error as FetchError;
                        toast.error('Fetch error occurred: ' + fetchError.body);
                        handleLogout();
                    }
                }
            };
            fetchUserDataDto();
        } else if (state.isLogged) {
            dispatch({ type: 'LOGOUT' });
            handleLogout();
        }
    }, [state.isLogged])

    return (
        <AuthContext.Provider value={{ state, dispatch }}>
            {children}
        </AuthContext.Provider>
    );
};

export function useAuthContext() {
    const context = useContext(AuthContext);
    if (!context) {
        throw new Error('useAuthContext must be used within an AuthProvider');
    }
    return context;
}