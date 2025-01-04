import React, { useEffect, useState } from 'react';
import DatePicker from '../../components/DatePicker';
import config from '../../app_config.json';
import { FetchError, Get } from '../../services/BaseApi';
import moment from 'moment';
import SeanceCard from '../../components/SeanceCard';
import { useLocation } from 'react-router-dom';
import { SeanceDto, SeanceDtoFlat } from './SeancesPageTypes';
import { toast } from 'react-toastify';

const SeancesPage: React.FC = () => {
    const location = useLocation();
    const params = new URLSearchParams(location.search);
    const paramsMovieTitle = params.get('title');
    const API_URL = config.API_URL;
    const [seances, setSeances] = useState<SeanceDtoFlat[]>([]);
    const [currentDate, setCurrentDate] = useState<Date>(new Date());
    const [searchTxt, setSearchTxt] = useState<string>(paramsMovieTitle ?? '');

    useEffect(() => {
        const fetchSeances = async () => {
            if (API_URL) {
                try {
                    const data = await Get<SeanceDto[]>(API_URL, '/seance/getAllForGivenDate', {
                        params: {
                            dateTime: moment(currentDate).format('YYYY-MM-DD'),
                            movieTitle: searchTxt
                        }
                    });

                    let dataFlat: SeanceDtoFlat[] = [];
                    data.forEach(d => {
                        const existingSeance = dataFlat.find(f => f.movieTitle === d.movieTitle);
                        if (existingSeance) {
                            existingSeance.seanceDates.push(d.seanceDate);
                        } else {
                            const newElem: SeanceDtoFlat = {
                                seanceDates: [d.seanceDate],
                                movieTitle: d.movieTitle,
                                movieDescription: d.movieDescription,
                                movieDurationInMin: d.movieDurationInMin,
                                movieImageUrl: d.movieImageUrl,
                                hallName: d.hallName,
                                hallCapacity: d.hallCapacity
                            }
                            dataFlat.push(newElem);
                        }
                    });

                    setSeances(dataFlat);
                    console.log('seance: ', dataFlat);
                } catch (error) {
                    const fetchError = error as FetchError;
                    toast.error('Fetch error occurred: ' + fetchError.body);
                }
            }
        };

        fetchSeances();
    }, [currentDate, searchTxt]);

    const dateChange = (date: Date) => {
        setCurrentDate(date);
    }

    return (
        <div className="flex flex-col items-center min-h-screen bg-cinemaBgPrimary text-cinemaTextPrimary p-8 pt-pageTopPadding">
            <div className="mb-6">
                <div className="mb-6">
                    <div className='flex'>
                        <input type="text" id="large-input" placeholder='🔍 Search' value={searchTxt} onChange={t => setSearchTxt(t.target.value)} className="min-w-96 h-10 block p-4 bg-cinemaBgSecondary border border-none rounded-full" />
                        <button onClick={() => setSearchTxt('')} className='ms-4 text-2xl font-bold hover: text-red-700 hover:text-red-500'>
                            &#x2715;
                        </button>
                    </div>
                </div>
                <DatePicker onDateChange={dateChange} />
                {seances.length > 0
                    ? <div className="mt-6 text-cinemaTextPrimary bg-cinemaBgSecondary rounded-lg transition-all duration-300 transform w-fit"
                        onClick={(e) => console.log(e.target)}>
                        {seances.map(seance => (
                            <SeanceCard key={seance.movieTitle} seance={seance} />
                        ))
                        }
                    </div>
                    : <span className='flex justify-center text-4xl font-bold mt-4'>No sessions found for given criteria</span>
                }
            </div>
        </div>
    );
};

export default SeancesPage;
