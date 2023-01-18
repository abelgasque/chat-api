import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

import { MatButtonModule } from '@angular/material/button';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
import { MatListModule } from '@angular/material/list';

import { ProgressSpinnerModule } from 'primeng/progressspinner';

import { FooterComponent } from './components/footer/footer.component';
import { NavbarComponent } from './components/navbar/navbar.component';
import { SidebarComponent } from './widgets/sidebar/sidebar.component';
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

    MatButtonModule,
    MatToolbarModule,
    MatIconModule,
    MatListModule,
    
    ProgressSpinnerModule,
  ],
  providers: []
})
export class SharedModule { }
