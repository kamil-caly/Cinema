export type RegisterUserDto = {
    email: string;
    password: string;
    firstName: string;
    lastName: string;
    dateOfBirth?: Date;
    nationality?: string;
}