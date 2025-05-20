import { Injectable } from '@angular/core';

import { JwtHelperService } from '@auth0/angular-jwt';

import { CustomerDTO } from '../models/customerDTO.interface';

@Injectable({
  providedIn: 'root'
})
export class CoreService {

  token: string;
  customer: CustomerDTO;

  constructor(
    private jwtHelperService: JwtHelperService,
  ) {
    this.token = this.getTokenLocalStorage();
    this.customer = this.getCustomerLocalStorage();
  }

  getTokenLocalStorage() {
    let token = localStorage.getItem('access_token');
    if (token != null) return token;
    return '';
  }

  setTokenLocalStorage(token: string) {
    this.token = token;
    if (token != '') {
      localStorage.setItem('access_token', this.token);
    } else {
      localStorage.removeItem('access_token');
    }
  }

  getCustomerLocalStorage() {
    let customer = localStorage.getItem('customer');
    if (customer != null) return JSON.parse(customer);
    return {};
  }

  setCustomerLocalStorage(customer: any) {
    this.customer = customer;
    if ('id' in customer) {
      localStorage.setItem('customer', JSON.stringify(customer));
    } else {
      localStorage.removeItem('customer');
    }
  }

  private decodeToken(pToken: string) {
    return this.jwtHelperService.decodeToken(pToken);
  }

  private isTokenExpired(pToken: string) {
    return this.jwtHelperService.isTokenExpired(pToken);
  }

  public isValidToken(): boolean {
    let token: string = this.getTokenLocalStorage();
    return (token != null && token.length > 0) ? (!this.isTokenExpired(token)) : false;
  }
}
