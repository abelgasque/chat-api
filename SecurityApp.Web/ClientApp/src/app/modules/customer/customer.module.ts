import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { TableModule } from 'primeng/table';

import { CustomerRoutingModule } from './customer-routing.module';
import { CustomerComponent } from './customer.component';
import { AuthGuard } from 'src/app/shared/guards/auth.guard';
import { MessagesService } from 'src/app/shared/services/messages.service';


@NgModule({
  declarations: [
    CustomerComponent
  ],
  imports: [
    CommonModule,
    CustomerRoutingModule,

    TableModule,
  ],
  providers: [
    AuthGuard,
    MessagesService
  ]
})
export class CustomerModule { }
