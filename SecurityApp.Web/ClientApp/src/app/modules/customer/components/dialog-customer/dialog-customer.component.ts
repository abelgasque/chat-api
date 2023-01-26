import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

import { Customer } from 'src/app/shared/models/customer.interface';
import { CustomerService } from 'src/app/shared/services/customer.service';
import { MessagesService } from 'src/app/shared/services/messages.service';

@Component({
  selector: 'app-dialog-customer',
  templateUrl: './dialog-customer.component.html',
  styleUrls: ['./dialog-customer.component.scss']
})
export class DialogCustomerComponent implements OnInit {

  form: FormGroup;
  hasData: boolean = false;
  guidEmpty: string = '00000000-0000-0000-0000-000000000000';
  loading: boolean = false;

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: Customer,
    private fb: FormBuilder,
    private dialogRef: MatDialogRef<DialogCustomerComponent>,
    private customerService: CustomerService,
    private messagesService: MessagesService
  ) {
    this.form = this.fb.group({
      id: [this.guidEmpty],
      creationDate: [null],
      updateDate: [null],
      firstName: ['', [Validators.required, Validators.maxLength(50)]],
      lastName: ['', [Validators.required, Validators.maxLength(250)]],
      mail: ['', [Validators.required, Validators.email, Validators.maxLength(250)]],
      password: ['', [Validators.required, Validators.maxLength(50)]],
      authAttempts: [0, [Validators.required]],
      active: [true, [Validators.required]],
      block: [false, [Validators.required]],
    });

    if (data) {
      this.hasData = true;
      this.form.setValue(data);
    }
  }

  ngOnInit(): void {
  }

  create() {
    this.loading = true;
    this.customerService.createAsync(this.form.value).subscribe({
      next: (resp: Customer) => {
        this.dialogRef.close(true);
        this.loading = false;
        this.messagesService.success('Success', 'Customer created');
      },
      error: (error: any) => {
        this.loading = false;
        this.messagesService.errorHandler(error);
      }
    })
  }

  update() {
    this.loading = true;
    this.customerService.updateAsync(this.form.value).subscribe({
      next: (resp: Customer) => {
        this.dialogRef.close(true);
        this.loading = false;
        this.messagesService.success('Success', 'Customer update');
      },
      error: (error: any) => {
        this.loading = false;
        this.messagesService.errorHandler(error);
      }
    })
  }

  ngSubmit() {
    let data = this.form.value;
    (data.id !== this.guidEmpty) ? this.update() : this.create();
  }

  clear() {
    this.form.reset();
    this.dialogRef.close(false);
  }
}
