import { SeatDto } from "../Seats/SeatsPageTypes"

export type UserTicketDto = {
    reservationCode: string,
    status: 'Valid' | 'Invalid' | 'Used' | 'NotExist',
    purchaseDate: Date,
    userEmail: string,
    hallName: string,
    movieTitle: string,
    seanceDate: Date,
    seatDtos: SeatDto[]
}