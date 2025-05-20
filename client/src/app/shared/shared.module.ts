import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

import { MatButtonModule } from '@angular/material/button';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
import { MatListModule } from '@angular/material/list';

import { ProgressSpinnerModule } from 'primeng/progressspinner';
import { ToastModule } from 'primeng/toast';
import { MessageService } from 'primeng/api';

import { FooterComponent } from './components/footer/footer.component';
import { NavbarComponent } from './components/navbar/navbar.component';
import { SidebarComponent } from './widgets/sidebar/sidebar.component';
import { SpinnerComponent } from './components/spinner/spinner.component';
import { MessageComponent } from './components/message/message.component';

let components = [
  FooterComponent,
  NavbarComponent,
  SidebarComponent,
  SpinnerComponent,
  MessageComponent,
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
    ToastModule,
  ],
  providers: [
    MessageService,
  ]
})
export class SharedModule { }
