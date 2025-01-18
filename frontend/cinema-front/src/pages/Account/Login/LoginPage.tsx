import FormField from "../../../components/FormField";
import { useEffect, useState } from "react";
import { FetchError, Post } from "../../../services/BaseApi";
import { toast } from "react-toastify";
import { useLocalStorage } from "../../../hooks/useLocalStorage";
import { useAuthContext } from "../../../contexts/AuthContext";
import { LoginUserDto } from "./LoginPageTypes";
import { Link, useNavigate } from "react-router-dom";

interface SignInAccountErrors {
    emailError?: string;
    passwordError?: string;
}

const RegisterPage: React.FC = () => {
    const API_URL = process.env.REACT_APP_API_URL ?? '';
    const [email, setEmail] = useState<string>('');
    const [password, setPassword] = useState<string>('');
    const [errors, setErrors] = useState<SignInAccountErrors>();
    const [_, setLSValue] = useLocalStorage();
    const { state, dispatch } = useAuthContext();
    const navigate = useNavigate();

    const logInClick = async () => {
        let errors: SignInAccountErrors = {};
        if (!email) {
            errors.emailError = 'Email is required';
        }
        if (!password) {
            errors.passwordError = 'Password is required';
        }
        if (password && password.length < 4) {
            errors.passwordError = 'Password must be at least 4 characters long';
        }

        setErrors(errors)

        if (Object.keys(errors).length > 0) {
            return;
        }

        const postBody: LoginUserDto = {
            email: email,
            password: password
        }

        try {
            const result = await Post(API_URL, '/account/login', { body: postBody });
            toast.success('Login success');
            setLSValue('token', result.toString());
            dispatch({ type: 'LOGIN' });
            navigate('/movies')
        } catch (error) {
            debugger;
            const fetchError = error as FetchError;
            toast.error('Fetch error occurred: ' + fetchError.body);
        }
    }

    return (
        <div className="flex justify-center items-center min-h-screen bg-cinemaBgPrimary p-8 pt-pageTopPadding">
            <div className='w-accountForm bg-cinemaBgViolet border-[1px] border-cinemaBorderSecondary rounded-lg p-8'>
                <label className="block text-3xl text-cinemaTextPrimary font-medium mb-8">Sign in to your account</label>
                <FormField
                    label='Your email'
                    type='text'
                    name='email'
                    placeholder='name@gmail.com'
                    value={email}
                    error={errors?.emailError}
                    onChange={e => setEmail(e.target.value)}
                />
                <FormField
                    className='mb-12'
                    label='Password'
                    type='password'
                    name='password'
                    placeholder='•••••••'
                    value={password}
                    error={errors?.passwordError}
                    onChange={e => setPassword(e.target.value)}
                />
                <button onClick={logInClick} className="w-full rounded-lg h-12 mb-4 bg-cinemaBtnViolet text-cinemaTextPrimary font-semibold hover:bg-cinemaBtnVioletHover">Log in to your account</button>
                <div>
                    <span className='text-cinemaTextGrayStrong'>Don’t have an account yet?</span>
                    <Link to='/register' className='text-cinemaTextViolet ms-2 font-semibold cursor-pointer hover:underline'>Sign up</Link>
                </div>
            </div>
        </div>
    );
};

export default RegisterPage;