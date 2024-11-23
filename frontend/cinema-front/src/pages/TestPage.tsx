import { useEffect, useRef, useState } from "react";

const TestPage: React.FC = () => {
    const [theme, setTheme] = useState<'light' | 'dark'>('light');
    var counter = 0;
    const counterRef = useRef<number>(0);

    // useEffect(() => {
    //     console.log('counter: ', counter)
    //     console.log('counterRef.current: ', counterRef.current)
    // }, [counterRef.current, counter, theme])

    return (
        <div className="p-20">
            <div>{theme}</div>
            <button className="bg-red-500 border border-black hover:bg-red-200 transform duration-500"
                onClick={() => setTheme(theme === 'light' ? 'dark' : 'light')}>changeTheme</button>
            <button className="bg-red-500 border border-black hover:bg-red-200 transform duration-500 ms-6"
                onClick={() => { counter += 1; counterRef.current += 1; }}>increment</button>
            <div>Let variable: {counter}</div>
            <div>useRef variable: {counterRef.current}</div>
            <button onClick={() => { console.log('counter: ', counter); console.log('counterRef.current: ', counterRef.current) }}>test</button>
        </div >
    );
};

export default TestPage;