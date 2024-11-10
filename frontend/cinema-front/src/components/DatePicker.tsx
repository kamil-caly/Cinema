import React, { useState } from 'react';
import arrowBackIcon from '../assets/arrow_back.svg';
import arrowNextIcon from '../assets/arrow_next.svg';
import convertToDatePickerValue from '../utils/DateConverter';
import moment from 'moment';

const DatePicker: React.FC = () => {
    const [firstDisplayDate, setFirstDisplayDate] = useState<Date>(new Date());

    console.log(firstDisplayDate)

    return (
        <div className='flex flex-col dark:bg-gray-700 border border-gray-600 rounded-lg w-fit p-4'>
            <div className='text-white mb-4 text-lg font-medium'>Wybierz interesujący Cię dzień</div>
            <div className='flex'>
                {!moment(firstDisplayDate).isSame(new Date(), 'day') && <img className='me-4 w-8 h-8 bg-white rounded-full self-center cursor-pointer opacity-85 hover:opacity-100' src={arrowBackIcon} alt="Back Arrow" />}
                {Array.from({ length: 7 }).map((_, i) => (
                    <button className='me-4 min-w-20 h-20 bg-violet-800 border border-white hover:bg-white hover:text-black rounded-lg transition-all duration-600'>
                        <div className='flex flex-col'>
                            <span className='font-bold'>{convertToDatePickerValue(moment(firstDisplayDate).add(i, 'days').toDate()).shortDay}</span>
                            <span>{convertToDatePickerValue(moment(firstDisplayDate).add(i, 'days').toDate()).shortMonth}</span>
                        </div>
                    </button>
                ))}
                {moment(firstDisplayDate).isBefore(moment(new Date()).add(6, 'month'), 'day') && <img className='w-8 h-8 bg-white rounded-full self-center cursor-pointer opacity-85 hover:opacity-100' src={arrowNextIcon} alt="Next Arrow" />}
            </div>
        </div>
    );
};

export default DatePicker;