import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

import { MatSidenavModule } from '@angular/material/sidenav';

import { CoreRoutingModule } from './core-routing.module';
import { CoreComponent } from './core.component';
import { SharedModule } from '../shared/shared.module';
import { SharedService } from '../shared/shared.service';
import { CustomerModule } from '../modules/customer/customer.module';
import { HomeModule } from '../modules/home/home.module';
import { SecurityModule } from '../modules/security/security.module';

@NgModule({
  declarations: [
    CoreComponent
  ],
  imports: [
    CommonModule,
    RouterModule,

    MatSidenavModule,

    CoreRoutingModule,
    SharedModule,
    CustomerModule,
    HomeModule,
    SecurityModule
  ],
  providers: [
    SharedService
  ]
})
export class CoreModule { }
