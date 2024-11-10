import React from 'react';
import DatePicker from '../../components/DatePicker';

const SeancesPage: React.FC = () => {

    const dateChange = (date: Date) => {
        console.log('new date: ', date);
    }

    return (
        <div className="flex flex-col items-center min-h-screen bg-gradient-radial from-cinemaBg via-purple-950 to-violet-950 text-white p-8 pt-24">
            <div>
                <div className="mb-6">
                    <label htmlFor="large-input" className="block mb-2 text-lg font-medium ps-2">Szukaj</label>
                    <input type="text" id="large-input" className="min-w-96 h-10 block p-4 text-gray-900 border border-gray-300 rounded-lg bg-gray-50 text-base focus:ring-blue-500 focus:border-blue-500 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500" />
                </div>
                <DatePicker onDateChange={dateChange} />
            </div>
        </div>
    );
};

export default SeancesPage;
