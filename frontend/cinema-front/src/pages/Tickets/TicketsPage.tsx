import { useEffect, useReducer, useState } from "react";
import { FetchError, Get, Put } from "../../services/BaseApi";
import { UserTicketDto } from "./TicketsPageTypes";
import { toast } from "react-toastify";
import { useNavigate } from "react-router-dom";
import { useAuthContext } from "../../contexts/AuthContext";
import { convertToLocalDate } from "../../utils/DateConverter";


const TicketPage = () => {
    const API_URL = process.env.REACT_APP_API_URL ?? '';
    const [tickets, setTickets] = useState<UserTicketDto[]>([]);
    const navigate = useNavigate();
    const { state, dispatch } = useAuthContext();

    useEffect(() => {
        if (!state.isLogged) {
            toast.error("Access only for logged users");
            navigate('/login');
            return;
        }

        fetchTickets();
    }, []);

    const fetchTickets = async () => {
        if (API_URL) {
            try {
                const data = await Get<UserTicketDto[]>(API_URL, '/ticket/getTickets', { params: { userRequest: (state.userDto?.role === 'Viewer').toString() } });
                setTickets(data);
            } catch (error) {
                const fetchError = error as FetchError;
                toast.error('Fetch error occurred: ' + fetchError.body);
            }
        }
    };

    const changeTicketState = async (ticket: UserTicketDto) => {
        if (state.userDto?.role === 'Viewer' || ticket.status !== "Valid") return;

        let isUpdated: boolean = false;
        try {
            isUpdated = await Put<boolean>(API_URL, '/ticket/changeState', { params: { reservationCode: ticket.reservationCode } });
        } catch (error) {
            const fetchError = error as FetchError;
            toast.error('Fetch error occurred: ' + fetchError.body);
        }

        if (isUpdated) {
            toast.success('Ticket state updated successfully');
            fetchTickets();
        } else {
            toast.error('Ticket status has not been updated');
        }
    }

    const renderTickets = () => {
        if (tickets.length === 0) {
            return <p className="text-cinemaTextGrayLight text-lg">No tickets available.</p>;
        }

        const groupedTickets = tickets.reduce((acc, ticket) => {
            const groupKey = `${ticket.seanceDate}-${ticket.hallName}`;
            if (!acc[groupKey]) {
                acc[groupKey] = [];
            }
            acc[groupKey].push(ticket);
            return acc;
        }, {} as Record<string, UserTicketDto[]>);

        return Object.entries(groupedTickets).map(([key, group]) => {
            const { seanceDate, hallName } = group[0];
            return (
                <div key={key} className="mb-6">
                    {/* Nagłówek grupy */}
                    <h1 className="text-3xl font-bold text-cinemaTextPrimary mb-4">
                        {`Seance Date: ${convertToLocalDate(new Date(seanceDate))}`} <br />
                        {`Hall: ${hallName}`}
                    </h1>
                    {/* Bilety w grupie */}
                    <div className="grid gap-6 sm:grid-cols-1 md:grid-cols-2 lg:grid-cols-3">
                        {group.map(ticket => (
                            <div
                                key={ticket.reservationCode}
                                className="bg-cinemaBgSecondary p-6 rounded-lg shadow-md h-fit"
                            >
                                <h2 className="text-3xl font-semibold mb-2">{ticket.movieTitle}</h2>
                                <p className="text-cinemaTextGrayLight text-lg mb-2 font-bold">User: {ticket.userEmail}</p>
                                <p className="text-cinemaTextGrayStrong text-lg mb-2">Seats: {ticket.seatDtos.map(seat => `${seat.row}-${seat.num}`).join(', ')}</p>
                                <p className={`text-lg font-semibold ${ticket.status === 'Valid' ? 'text-cinemaTextViolet' :
                                    ticket.status === 'Used' ? 'text-cinemaTextGreen' :
                                        'text-cinemaTextRed'
                                    }`}>
                                    Ticket Status: {ticket.status}
                                </p>
                                <p className="text-cinemaTextGrayLight text-lg mt-2">Purchased: {convertToLocalDate(new Date(ticket.purchaseDate))}</p>
                                <p className="text-cinemaTextGrayLight text-lg mt-2">Reservation Code: {ticket.reservationCode}</p>
                                {state.userDto?.role !== 'Viewer' ?
                                    <button
                                        onClick={() => changeTicketState(ticket)}
                                        className={`mt-8 rounded-lg w-full h-12 bg-cinemaBtnViolet text-cinemaTextPrimary 
                                        font-semibold ${ticket.status !== 'Valid' ? 'opacity-50 cursor-default' : 'hover:bg-cinemaBtnVioletHover'}`}
                                    >
                                        Change state
                                    </button>
                                    : null}
                            </div>
                        ))}
                    </div>
                </div >
            );
        });
    };


    return (
        <div className="flex flex-wrap content-start min-h-screen gap-6 bg-cinemaBgPrimary text-cinemaTextPrimary p-8 pt-pageTopPadding">
            {renderTickets()}
        </div>
    );
}

export default TicketPage;