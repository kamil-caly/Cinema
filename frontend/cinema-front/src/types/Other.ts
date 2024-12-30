export type DatePickerDateValue = {
    shortDay: string
    shortMonth: string
}

export type UserDataDto = {
    email: string,
    firstName: string,
    lastName: string,
    role: 'Admin' | 'Ticketer' | 'Viewer'
}