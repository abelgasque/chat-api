import { Component, OnInit } from '@angular/core';

import { LazyLoadEvent } from 'primeng/api';

import { CustomerService } from 'src/app/shared/services/customer.service';
import { MessagesService } from 'src/app/shared/services/messages.service';

@Component({
  selector: 'app-customer',
  templateUrl: './customer.component.html',
  styleUrls: ['./customer.component.scss']
})
export class CustomerComponent implements OnInit {

  data: any[] = [];
  totalRecords: number = 25;
  page: number = 1;
  size: number = 25;
  loading: boolean = true;

  constructor(
    private customerService: CustomerService,
    private messagesService: MessagesService
  ) { }

  ngOnInit(): void { }

  pagination(event: LazyLoadEvent) {
    let first = event.first ?? 25;
    this.size = event.rows ?? 25;
    this.page = (Math.floor(first / this.size + 1)) ?? 1;
    this.read();
  }

  read() {
    this.loading = true;

    let filter = {
      size: this.size,
      page: this.page
    };

    this.customerService.readAsync(filter).subscribe({
      next: (resp: any) => {
        this.data = resp.data;
        this.totalRecords = resp.total;
        this.size = resp.size;
        this.loading = false;
      },
      error: (error: any) => {
        this.messagesService.errorHandler(error);
        this.loading = false;
      }
    })
  }
}
