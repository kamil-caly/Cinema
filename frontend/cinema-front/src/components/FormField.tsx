interface FormFieldProps {
    label: string;
    type: string;
    name: string;
    placeholder: string;
    value: string;
    error?: string;
    className?: string;
    onChange: (e: React.ChangeEvent<HTMLInputElement>) => void;
}

const FormField: React.FC<FormFieldProps> = ({ label, type, name, placeholder, value, error, className, onChange }) => (
    <div className={`${className ?? ''} ${className?.includes('mb-') ? '' : 'mb-8'}`}>
        <label className="block mb-2 text-small text-cinemaTextPrimary font-semibold">{label}</label>
        <input
            type={type}
            name={name}
            placeholder={placeholder}
            value={value}
            onChange={onChange}
            className={`w-full rounded-lg h-12 px-2 text-cinemaTextPrimary bg-cinemaBgSecondary border-[1px] 
                ${error ? 'border-cinemaTextRed border-2' : 'border-cinemaBorderSecondary'}
                 focus:border-cinemaBtnViolet focus:border-2 focus:outline-none`}
        />
        {error && <label className="text-cinemaTextRed block">{error}</label>}
    </div>
);


export default FormField;