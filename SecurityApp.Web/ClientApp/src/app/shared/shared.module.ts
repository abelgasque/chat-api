import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';

import { MatButtonModule } from '@angular/material/button';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';

import { ProgressSpinnerModule } from 'primeng/progressspinner';

import { SharedService } from './shared.service';
import { FooterComponent } from './components/footer/footer.component';
import { NavbarComponent } from './components/navbar/navbar.component';
import { SidebarComponent } from './components/sidebar/sidebar.component';
import { SpinnerComponent } from './components/spinner/spinner.component';

let components = [
  FooterComponent,
  NavbarComponent,
  SidebarComponent,
  SpinnerComponent,
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
    MatIconModule,

    ProgressSpinnerModule,
  ],
  providers: [
    SharedService
  ]
})
export class SharedModule { }
