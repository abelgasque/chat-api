import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { environment } from 'src/environments/environment';
import { UserDTO } from '../models/userDTO.interface';
import { RefreshTokenDTO } from '../models/refreshTokenDTO.interface';

@Injectable({
  providedIn: 'root'
})
export class TokenService {
  private baseUrl: string;

  constructor(
    private http: HttpClient,
  ) {
    this.baseUrl = `${environment.baseUrlApi}/v1/api/token`;
  }

  signIn(user: UserDTO) {
    return this.http.post<any>(`${this.baseUrl}`, user);
  }

  refresh(refreshToken: RefreshTokenDTO) {
    return this.http.post<any>(`${this.baseUrl}/refresh`, refreshToken);
  }
}
