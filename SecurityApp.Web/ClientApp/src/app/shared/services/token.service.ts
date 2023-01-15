import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { environment } from 'src/environments/environment';
import { TokenDTO } from '../models/tokenDTO.interface';
import { UserDTO } from '../models/userDTO.interface';

@Injectable({
  providedIn: 'root'
})
export class TokenService {
  private baseUrl: string;

  constructor(private http: HttpClient) {
    this.baseUrl = `${environment.baseUrlApi}/token`;
  }

  signIn(user: UserDTO) {
    return this.http.post<TokenDTO>(`${this.baseUrl}`, user);
  }
}
