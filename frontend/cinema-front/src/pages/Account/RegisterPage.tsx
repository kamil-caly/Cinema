import config from '../../app_config.json';

const RegisterPage: React.FC = () => {
    const API_URL = config.API_URL;

    return (
        <div className="flex justify-center items-center min-h-screen bg-cinemaBgPrimary p-8 pt-pageTopPadding">
            <div className='w-accountForm bg-cinemaBgViolet border-[1px] border-cinemaBorderSecondary rounded-lg p-8'>
                <label className="block text-3xl text-cinemaTextPrimary font-medium mb-8">Create an account</label>
                <label className="block mb-2 text-small text-cinemaTextPrimary font-semibold">Your email</label>
                <input type="text" placeholder='name@gmail.com' onChange={t => { }} className="w-full rounded-lg h-12 px-2 mb-8 text-cinemaTextPrimary bg-cinemaBgSecondary border-[1px] border-cinemaBorderSecondary focus:border-cinemaBtnViolet focus:border-2 focus:outline-none" />
                <label className="block mb-2 text-small text-cinemaTextPrimary font-semibold">Password</label>
                <input type="password" placeholder='•••••••' onChange={t => { }} className="w-full rounded-lg h-12 px-2 mb-8 text-cinemaTextPrimary bg-cinemaBgSecondary border-[1px] border-cinemaBorderSecondary focus:border-cinemaBtnViolet focus:border-2 focus:outline-none" />
                <label className="block mb-2 text-small text-cinemaTextPrimary font-semibold">Confirm Password</label>
                <input type="password" placeholder='•••••••' onChange={t => { }} className="w-full rounded-lg h-12 px-2 mb-12 text-cinemaTextPrimary bg-cinemaBgSecondary border-[1px] border-cinemaBorderSecondary focus:border-cinemaBtnViolet focus:border-2 focus:outline-none" />
                <button className="w-full rounded-lg h-12 mb-4 bg-cinemaBtnViolet text-cinemaTextPrimary font-semibold hover:bg-cinemaBtnVioletHover">Create an account</button>
                <div>
                    <span className='text-cinemaTextGrayStrong'>Already have an account?</span>
                    <a href='/login' className='text-cinemaTextViolet ms-2 font-semibold cursor-pointer hover:underline'>Login here</a>
                </div>
            </div>
        </div>
    );
};

export default RegisterPage;