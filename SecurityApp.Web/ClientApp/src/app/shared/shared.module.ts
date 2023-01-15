import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

import { MatButtonModule } from '@angular/material/button';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';

import { NavbarComponent } from './components/navbar/navbar.component';
import { SidebarComponent } from './components/sidebar/sidebar.component';
import { SharedService } from './shared.service';
import { HttpClientModule } from '@angular/common/http';

let components = [
  NavbarComponent,
  SidebarComponent,
]

@NgModule({
  declarations: components,
  exports: components,
  imports: [
    CommonModule,
    RouterModule,
    HttpClientModule,

    MatButtonModule,
    MatToolbarModule,
    MatIconModule
  ],
  providers: [
    SharedService
  ]
})
export class SharedModule { }
