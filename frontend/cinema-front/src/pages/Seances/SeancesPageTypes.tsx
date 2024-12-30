
export type SeanceDto = {
    seanceDate: Date,
    movieTitle?: string,
    movieDescription?: number,
    movieDurationInMin?: number,
    movieImageUrl?: string,
    hallName?: string,
    hallCapacity?: number
}

export type SeanceDtoFlat = {
    seanceDates: Date[],
    movieTitle?: string,
    movieDescription?: number,
    movieDurationInMin?: number,
    movieImageUrl?: string,
    hallName?: string,
    hallCapacity?: number
}