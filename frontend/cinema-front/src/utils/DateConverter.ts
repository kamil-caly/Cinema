import { DatePickerDateValue } from "../types/Other";

const isToday = (dateNow: Date, incomingDate: Date): boolean => {
    return (
        dateNow.getDate() === incomingDate.getDate() &&
        dateNow.getMonth() === incomingDate.getMonth() &&
        dateNow.getFullYear() === incomingDate.getFullYear()
      );
}

const isTomorrow = (dateNow: Date, incomingDate: Date): boolean => {
    return (
        dateNow.getDate() + 1 === incomingDate.getDate() &&
        dateNow.getMonth() === incomingDate.getMonth() &&
        dateNow.getFullYear() === incomingDate.getFullYear()
      );
}

const convertToDatePickerValue = (date: Date): DatePickerDateValue => {
    const dateNow = new Date();
    let result: DatePickerDateValue = {shortDay: '', shortMonth: ''};

    if (isToday(dateNow, date)) {
        result.shortDay = 'Dzisiaj';
    } else if (isTomorrow(dateNow, date)) {
        result.shortDay = 'Jutro';
    } else {
        switch(date.getDay()) {
            case 0:
                result.shortDay = 'Ndz.'
                break;
            case 1:
                result.shortDay = 'Pon.'
                break;
            case 2:
                result.shortDay = 'Wt.'
                break;
            case 3:
                result.shortDay = 'śr.'
                break;
            case 4:
                result.shortDay = 'Czw.'
                break;
            case 5:
                result.shortDay = 'Pt.'
                break;
            case 6:
                result.shortDay = 'Sob.'
                break;
            default:
                break;
        }
    }

    const dayOfMonth: number = date.getDate();
    let month: string = '';

    switch(date.getMonth()) {
        case 0:
            month = 'sty';
            break;
        case 1:
            month = 'lut';
            break;
        case 2:
            month = 'mar';
            break;
        case 3:
            month = 'kwi';
            break;
        case 4:
            month = 'maj';
            break;
        case 5:
            month = 'cze';
            break;
        case 6:
            month = 'lip';
            break;
        case 7:
            month = 'sie';
            break;
        case 8:
            month = 'wrz';
            break;
        case 9:
            month = 'paź';
            break;
        case 10:
            month = 'lis';
            break;
        case 11:
            month = 'gru';
            break;
        default:
            break;
    }

    result.shortMonth = dayOfMonth + ' ' + month;
    return result;
}

export default convertToDatePickerValue;