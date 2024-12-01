export interface UserDataDto {
    email: string,
    firstName: string,
    lastName: string,
    role: 'Admin' | 'Ticketer' | 'Viewer'
}