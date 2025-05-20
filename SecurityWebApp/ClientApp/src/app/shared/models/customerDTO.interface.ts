export interface CustomerDTO {
    id: string;
    creationDate: Date;
    updateDate: Date;
    firstName: string;
    lastName: string;
    mail: string;
    active: boolean;
    block: boolean;
}