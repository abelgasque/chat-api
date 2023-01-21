import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { environment } from 'src/environments/environment';
import { CustomerLeadDTO } from '../models/customerLeadDTO.interface';

@Injectable({
  providedIn: 'root'
})
export class CustomerService {
  private baseUrl: string;

  constructor(private http: HttpClient) {
    this.baseUrl = `${environment.baseUrlApi}/v1/api/customer`;
  }

  createLeadAsync(customer: CustomerLeadDTO) {
    return this.http.post<any>(`${this.baseUrl}/lead`, customer);
  }

  readAsync(filtro: any) {
    let params = new HttpParams({
      fromObject: {
        page: filtro.page.toString(),
        size: filtro.size.toString()
      }
    });

    return this.http.get<any>(`${this.baseUrl}`, { params });
  }
}
