export type SeatDto = {
    row: number,
    num: number,
    vip: boolean
}

export type HallDto = {
    capacity: number,
    hallName: string,
    movieTitle: string,
    movieDurationInMin: number,
    movieImageUrl: string,
    seanceDate: Date,
    seatDtos: SeatDto[]
}

export type CreateTicketDto = {
    seanceDate: string,
    seatDtos: SeatDto[]
}