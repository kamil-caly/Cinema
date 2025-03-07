import { useState } from 'react';

import FormField from '../../../components/FormField';
import { FetchError, Post } from '../../../services/BaseApi';
import { toast } from 'react-toastify';
import { useNavigate } from "react-router";
import { RegisterUserDto } from './RegisterPageTypes';
import { Link } from 'react-router-dom';

interface CreateAccountErrors {
    firstNameError?: string;
    lastNameError?: string;
    emailError?: string;
    passwordError?: string;
    confPasswordError?: string;
}

const RegisterPage: React.FC = () => {
    const API_URL = process.env.REACT_APP_API_URL ?? '';
    const [firstName, setFirstName] = useState<string>('');
    const [lastName, setLastName] = useState<string>('');
    const [email, setEmail] = useState<string>('');
    const [password, setPassword] = useState<string>('');
    const [confPassword, setConfPassword] = useState<string>('');
    const [errors, setErrors] = useState<CreateAccountErrors>();
    const navigate = useNavigate();

    const createAccoutClick = async () => {
        let errors: CreateAccountErrors = {};
        if (!firstName) {
            errors.firstNameError = 'FirstName is required';
        }
        if (!lastName) {
            errors.lastNameError = 'LastName is required';
        }
        if (!email) {
            errors.emailError = 'Email is required';
        }
        if (!password) {
            errors.passwordError = 'Password is required';
        }
        if (password && password.length < 4) {
            errors.passwordError = 'Password must be at least 4 characters long';
        }
        if (!confPassword) {
            errors.confPasswordError = 'Confirm Password is required'
        }
        if (confPassword && confPassword !== password) {
            errors.confPasswordError = 'Confirm Password must be the same as Password'
        }

        setErrors(errors)

        if (Object.keys(errors).length > 0) {
            return;
        }

        const postBody: RegisterUserDto = {
            firstName: firstName,
            lastName: lastName,
            email: email,
            password: password
        }

        try {
            const result = await Post(API_URL, '/account/register', { body: postBody });
            toast.success(result.toString());
            navigate('/login');
        } catch (error) {
            const fetchError = error as FetchError;
            toast.error('Fetch error occurred: ' + fetchError.body);
        }
    }

    return (
        <div className="flex justify-center items-center min-h-screen bg-cinemaBgPrimary p-8 pt-pageTopPadding">
            <div className='w-accountForm bg-cinemaBgViolet border-[1px] border-cinemaBorderSecondary rounded-lg p-8'>
                <label className="block text-3xl text-cinemaTextPrimary font-medium mb-8">Create an account</label>
                <FormField
                    label='First Name'
                    type='text'
                    name='firstName'
                    placeholder='John'
                    value={firstName}
                    error={errors?.firstNameError}
                    onChange={e => setFirstName(e.target.value)}
                />
                <FormField
                    label='Last Name'
                    type='text'
                    name='lastName'
                    placeholder='Deere'
                    value={lastName}
                    error={errors?.lastNameError}
                    onChange={e => setLastName(e.target.value)}
                />
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
                    label='Password'
                    type='password'
                    name='password'
                    placeholder='•••••••'
                    value={password}
                    error={errors?.passwordError}
                    onChange={e => setPassword(e.target.value)}
                />
                <FormField
                    className='mb-12'
                    label='Confirm Password'
                    type='password'
                    name='confirmPassword'
                    placeholder='•••••••'
                    value={confPassword}
                    error={errors?.confPasswordError}
                    onChange={e => setConfPassword(e.target.value)}
                />
                <button onClick={createAccoutClick} className="w-full rounded-lg h-12 mb-4 bg-cinemaBtnViolet text-cinemaTextPrimary font-semibold hover:bg-cinemaBtnVioletHover">Create an account</button>
                <div>
                    <span className='text-cinemaTextGrayStrong'>Already have an account?</span>
                    <Link to='/login' className='text-cinemaTextViolet ms-2 font-semibold cursor-pointer hover:underline'>Login here</Link>
                </div>
            </div>
        </div>
    );
};

export default RegisterPage;