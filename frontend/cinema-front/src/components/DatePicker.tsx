import React, { useCallback, useState } from 'react';
import arrowBackIcon from '../assets/arrow_back.svg';
import arrowNextIcon from '../assets/arrow_next.svg';
import convertToDatePickerValue from '../utils/DateConverter';
import moment from 'moment';


interface DatePickerProps {
    onDateChange: (selectedDate: Date) => void;
}

const DatePicker: React.FC<DatePickerProps> = ({ onDateChange }) => {
    const [firstDisplayDate, setFirstDisplayDate] = useState<Date>(new Date());
    const [selectedDayInYear, setSelectedDayInYear] = useState<number>(moment(firstDisplayDate).dayOfYear());

    const moveDates = (dir: 'left' | 'right') => {
        if (dir == 'left' && !moment(firstDisplayDate).isSame(new Date(), 'day')) {
            setFirstDisplayDate(moment(firstDisplayDate).subtract(7, 'days').toDate());
        }
        if (dir == 'right' && moment(firstDisplayDate).isBefore(moment(new Date()).add(6, 'month'), 'day')) {
            setFirstDisplayDate(moment(firstDisplayDate).add(7, 'days').toDate());
        }
    }

    const handleDateSelection = (date: moment.Moment) => {
        const dayOfYear = moment(date).dayOfYear();
        if (dayOfYear !== selectedDayInYear) {
            setSelectedDayInYear(dayOfYear);
            onDateChange(date.toDate());
        }
    }

    return (
        <div className='flex flex-col bg-gray-700 border border-gray-600 rounded-lg w-fit h-fit p-4'>
            <div className='text-white mb-4 text-lg font-medium'>Wybierz interesujący Cię dzień</div>
            <div className='flex'>
                <img onClick={() => moveDates('left')}
                    className={`me-4 w-8 h-8 bg-white rounded-full self-center 
                    ${moment(firstDisplayDate).isSame(new Date(), 'day') ? 'cursor-auto opacity-20' : ' cursor-pointer opacity-85 hover:opacity-100'}`}
                    src={arrowBackIcon} alt="Back Arrow" />
                {Array.from({ length: 7 }).map((_, i) => {
                    const date = moment(firstDisplayDate).add(i, 'days');
                    const isActive = selectedDayInYear === moment(firstDisplayDate).add(i, 'days').dayOfYear();
                    return (
                        <button
                            key={date.dayOfYear()}
                            onClick={() => handleDateSelection(date)}
                            className={`me-4 min-w-20 h-20 bg-violet-800 border border-white hover:bg-white hover:text-black rounded-lg transition-all duration-600
                        ${isActive ? 'bg-white text-black' : ''}`}>
                            <div className='flex flex-col'>
                                <span className='font-bold'>{convertToDatePickerValue(moment(firstDisplayDate).add(i, 'days').toDate()).shortDay}</span>
                                <span>{convertToDatePickerValue(moment(firstDisplayDate).add(i, 'days').toDate()).shortMonth}</span>
                            </div>
                        </button>
                    );
                })}
                <img onClick={() => moveDates('right')}
                    className={`me-4 w-8 h-8 bg-white rounded-full self-center 
                    ${!moment(firstDisplayDate).isBefore(moment(new Date()).add(6, 'month'), 'day') ? 'cursor-auto opacity-20' : ' cursor-pointer opacity-85 hover:opacity-100'}`}
                    src={arrowNextIcon} alt="Next Arrow" />
            </div>
        </div>
    );
};

export default DatePicker;