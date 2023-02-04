import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { PageEvent } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';

import { Customer } from 'src/app/shared/models/customer.interface';
import { CustomerService } from 'src/app/shared/services/customer.service';
import { MessagesService } from 'src/app/shared/services/messages.service';
import { SharedService } from 'src/app/shared/services/shared.service';
import { DialogCustomerComponent } from './components/dialog-customer/dialog-customer.component';

@Component({
  selector: 'app-customer',
  templateUrl: './customer.component.html',
  styleUrls: ['./customer.component.scss']
})
export class CustomerComponent implements OnInit {

  data = new MatTableDataSource();
  displayedColumns: string[];
  totalRecords: number = 10;
  page: number = 1;
  size: number = 10;
  form: FormGroup;

  constructor(
    private fb: FormBuilder,
    private dialog: MatDialog,
    private customerService: CustomerService,
    private sharedService: SharedService,
    private messagesService: MessagesService
  ) {
    this.displayedColumns = [
      'fisrtName', 'lastName', 'mail', 'dtCreation', 'dtUpdate', 'active', 'block', 'actions'
    ];
    this.form = this.fb.group({
      creationDateStart: [''],
      creationDateEnd: [''],
      updateDateStart: [''],
      updateDateEnd: [''],
      firstName: ['', [Validators.maxLength(50)]],
      lastName: ['', [Validators.maxLength(250)]],
      mail: ['', [Validators.maxLength(250)]],
      active: [''],
      block: [''],
    });
  }

  ngOnInit(): void {
    this.read();
  }

  read() {
    this.sharedService.openSpinner();
    let filter = Object.assign({}, { size: this.size, page: this.page }, this.form.value);
    this.customerService.readAsync(filter).subscribe({
      next: (resp: any) => {
        this.data.data = resp.data;
        this.totalRecords = resp.total;
        this.size = resp.size;
        this.sharedService.closeSpinner();
      },
      error: (error: any) => {
        this.messagesService.errorHandler(error);
      }
    })
  }

  readById(id: string) {
    this.sharedService.openSpinner();
    this.customerService.readByIdAsync(id).subscribe({
      next: (resp: Customer) => {
        this.openDialog(resp);
        this.sharedService.closeSpinner();
      },
      error: (error: any) => {
        this.messagesService.errorHandler(error);
      }
    })
  }

  delete(id: string) {
    this.sharedService.openSpinner();
    this.customerService.deleteByIdAsync(id).subscribe({
      next: (resp: any) => {
        this.read();
        this.sharedService.closeSpinner();
      },
      error: (error: any) => {
        this.messagesService.errorHandler(error);
      }
    })
  }

  handlePageEvent(event: PageEvent) {
    this.size = event.pageSize;
    this.page = (event.pageIndex + 1);
    this.read();
  }

  clear() {
    this.form.reset();
  }

  newCustomer() {
    this.openDialog(undefined);
  }

  openDialog(customer?: Customer) {
    let dialogRef = this.dialog.open(DialogCustomerComponent, {
      width: '95vw',
      data: customer
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.read();
      }
    });
  }
}
