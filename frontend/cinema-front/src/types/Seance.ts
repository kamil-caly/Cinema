
export interface SeanceDto {
    seanceDate: Date,
    movieTitle?: string,
    movieDescription?: number,
    movieDurationInMin?: number,
    movieImageUrl?: string,
    hallName?: string,
    hallCapacity?: number
}

export interface SeanceDtoFlat {
    seanceDates: Date[],
    movieTitle?: string,
    movieDescription?: number,
    movieDurationInMin?: number,
    movieImageUrl?: string,
    hallName?: string,
    hallCapacity?: number
}