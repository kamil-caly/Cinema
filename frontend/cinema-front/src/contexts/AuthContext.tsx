import React, { createContext, useContext, useState, ReactNode, Dispatch, SetStateAction, useEffect } from 'react';
import { useLocalStorage } from '../hooks/useLocalStorage';
import config from '../app_config.json';
import { Get } from '../services/BaseApi';
import { UserDataDto } from '../types/UserDataDto';

interface AuthContextType {
    isLogged: boolean;
    setIsLogged: (value: boolean) => void;
}

const AuthContext = createContext<AuthContextType | null>(null);

export const AuthProvider = ({ children }: { children: ReactNode }) => {
    const API_URL = config.API_URL;
    const [isLogged, setIsLogged] = useState<boolean>(false);
    const [getLSValue] = useLocalStorage();

    useEffect(() => {
        const token: string = getLSValue('token') ?? '';
        if (getLSValue('token') !== null) {
            const fetchUserDataDto = async () => {
                if (API_URL) {
                    try {
                        const data = await Get<UserDataDto>(API_URL, '/account/getUserData', { params: { token: token } });
                        //debugger;
                        // tymczasowe rozwiązanie
                        setIsLogged(data !== null);
                    } catch (error) {
                        console.error('Error fetching UserDataDto:', error);
                    }
                }
            };
            fetchUserDataDto();
        }
    }, [])

    return (
        <AuthContext.Provider value={{ isLogged, setIsLogged }}>
            {children}
        </AuthContext.Provider>
    );
};

export function useAuthContext() {
    return useContext(AuthContext);
}