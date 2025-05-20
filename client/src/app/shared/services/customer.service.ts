import { formatDate } from '@angular/common';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { environment } from 'src/environments/environment';
import { Customer } from '../models/customer.interface';
import { CustomerFilter } from '../models/customerFilter.interface';
import { CustomerLeadDTO } from '../models/customerLeadDTO.interface';

@Injectable({
  providedIn: 'root'
})
export class CustomerService {
  private baseUrl: string;

  constructor(private http: HttpClient) {
    this.baseUrl = `${environment.baseUrlApi}/v1/api/customer`;
  }

  createAsync(customer: Customer) {
    return this.http.post<any>(`${this.baseUrl}`, customer);
  }

  createLeadAsync(customer: CustomerLeadDTO) {
    return this.http.post<any>(`${this.baseUrl}/lead`, customer);
  }

  readAsync(filtro: CustomerFilter) {
    let params = new HttpParams({
      fromObject: {
        page: filtro.page.toString(),
        size: filtro.size.toString()
      }
    });

    if (filtro.firstName) {
      params = params.append('firstName', filtro.firstName);
    }

    if (filtro.lastName) {
      params = params.append('lastName', filtro.lastName);
    }

    if (filtro.mail) {
      params = params.append('mail', filtro.mail);
    }

    if (filtro.active) {
      params = params.append('active', filtro.active.toString());
    }

    if (filtro.block) {
      params = params.append('block', filtro.block.toString());
    }
    
    if (filtro.creationDateStart && filtro.creationDateEnd) {
      params = params.append('creationDateStart', formatDate(filtro.creationDateStart, 'yyyy-MM-dd', 'en').toString());
      params = params.append('creationDateEnd', formatDate(filtro.creationDateEnd, 'yyyy-MM-dd', 'en').toString());
    }

    if (filtro.updateDateStart && filtro.updateDateEnd) {
      params = params.append('updateDateStart', formatDate(filtro.updateDateStart, 'yyyy-MM-dd', 'en').toString());
      params = params.append('updateDateEnd', formatDate(filtro.updateDateEnd, 'yyyy-MM-dd', 'en').toString());
    }

    return this.http.get<any>(`${this.baseUrl}`, { params });
  }

  readByIdAsync(id: string) {
    return this.http.get<any>(`${this.baseUrl}/${id}`);
  }

  updateAsync(customer: Customer) {
    return this.http.put<any>(`${this.baseUrl}`, customer);
  }

  deleteByIdAsync(id: string) {
    return this.http.delete<any>(`${this.baseUrl}/${id}`);
  }
}
