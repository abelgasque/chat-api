import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { environment } from 'src/environments/environment';
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
    return this.http.post<any>(`${this.baseUrl}`, user);
  }
}
