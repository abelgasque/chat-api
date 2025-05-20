export interface Customer {
    id: string;
    creationDate: Date;
    updateDate: Date;
    firstName: string;
    lastName: string;
    mail: string;
    password: string;
    authAttempts: number;
    active: boolean;
    block: boolean;
}