export interface CustomerFilter {
    id: string;
    creationDateStart: string;
    creationDateEnd: string;
    updateDateStart: string;
    updateDateEnd: string;
    firstName: string;
    lastName: string;
    mail: string;
    active: boolean;
    block: boolean;
    page: number;
    size: number;
}