import { Injectable } from '@angular/core';

import { CustomerDTO } from '../shared/models/customerDTO.interface';

@Injectable({
  providedIn: 'root'
})
export class CoreService {

  token: string;
  customer: CustomerDTO;

  constructor() {
    this.token = this.getTokenLocalStorage();
    this.customer = this.getCustomerLocalStorage();
    console.log(this.customer);
  }

  getTokenLocalStorage() {
    let token = localStorage.getItem('token');
    if (token != null) return token;
    return '';
  }

  setTokenLocalStorage(token: string) {
    this.token = token;
    if (token != '') {
      localStorage.setItem('token', this.token);
    } else {
      localStorage.removeItem('token');
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
}
