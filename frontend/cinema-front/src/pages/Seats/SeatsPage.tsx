import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import config from '../../app_config.json';
import { FetchError, Get, Post } from "../../services/BaseApi";
import { CreateTicketDto, HallDto, SeatDto } from "./SeatsPageTypes";
import { toast } from "react-toastify";
import { useAuthContext } from "../../contexts/AuthContext";
import CinemaHall from "../../components/CinemaHall";


const SeatsPage = () => {
    const API_URL = config.API_URL;
    const { seanceDate } = useParams();
    const { state, dispatch } = useAuthContext();
    const navigate = useNavigate();
    const [hallData, setHallData] = useState<HallDto>();
    const [bookedSeats, setBookedSeats] = useState<SeatDto[]>([]);

    useEffect(() => {
        if (!state.isLogged || state.userDto?.role === 'Ticketer') {
            toast.error("Access only for logged users in role: 'Admin' or 'Viewer'");
            navigate('/login');
            return;
        }

        fetchHallDto();
    }, []);

    const fetchHallDto = async () => {
        if (API_URL) {
            try {
                const data = await Get<HallDto>(API_URL, '/hall/getHallForGivenSeance', { params: { dateTime: seanceDate ?? '' } });
                setHallData(data);
            } catch (error) {
                const fetchError = error as FetchError;
                toast.error('Fetch error occurred: ' + fetchError.body);
            }
        }
    };

    const handleSeatClick = (seatDto: SeatDto) => {
        setBookedSeats(prev => {
            if (prev.some(s => s.row === seatDto.row && s.num === seatDto.num)) {
                return prev.filter(s => s.row !== seatDto.row || s.num !== seatDto.num);
            }

            return [...prev, seatDto];
        });
    }

    const bookSeatsClick = async () => {
        if (bookedSeats.length <= 0) return;

        const postBody: CreateTicketDto = {
            seanceDate: seanceDate ?? '',
            seatDtos: bookedSeats
        }

        try {
            const result = await Post(API_URL, '/ticket/createTicket', { body: postBody });
            toast.success(`places have been successfully booked with reservation code: ${result}`);
            setBookedSeats([]);
            fetchHallDto();
        } catch (error: any) {
            const fetchError = error as FetchError;
            toast.error('Fetch error occurred: ' + fetchError.body);
        }
    }

    return (
        <div className="flex justify-center min-h-screen bg-cinemaBgPrimary text-cinemaTextPrimary p-8 pt-pageTopPadding">
            <div className="bg-cinemaBgSecondary p-6 rounded-lg shadow-md flex flex-col items-center justify-between">
                <div className="flex flex-col items-center">
                    <img
                        src={hallData?.movieImageUrl}
                        alt={hallData?.movieTitle}
                        className="object-fill rounded-lg w-24"
                    />
                    <h2 className="text-2xl font-semibold my-2">{hallData?.movieTitle}</h2>
                    <p className="text-cinemaTextGrayStrong text-sm mb-2">{hallData?.movieDurationInMin} min</p>
                    <p className="text-cinemaTextGrayLight mb-2">{hallData?.seanceDate.toString().replace('T', ' ').slice(0, -3)}</p>
                </div>
                <button
                    onClick={bookSeatsClick}
                    className={`w-24 rounded-lg h-12 bg-cinemaBtnViolet text-cinemaTextPrimary 
                        font-semibold ${bookedSeats.length <= 0 ? 'opacity-50 cursor-default' : 'hover:bg-cinemaBtnVioletHover'}`}>
                    Book
                </button>
            </div>
            <CinemaHall hallData={hallData} onClick={handleSeatClick} />
        </div>
    );
}

export default SeatsPage;