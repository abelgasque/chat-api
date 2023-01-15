import { CustomerDTO } from "./customerDTO.interface";

export interface TokenDTO {
    accessToken: string,
    expiresIn: number,
    customer: CustomerDTO
}