import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field'
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatTabsModule } from '@angular/material/tabs';

import { SecurityRoutingModule } from './security-routing.module';
import { MessagesService } from 'src/app/shared/services/messages.service';
import { SecurityComponent } from './security.component';
import { SignUpComponent } from './components/sign-up/sign-up.component';
import { SignInComponent } from './components/sign-in/sign-in.component';

@NgModule({
  declarations: [
    SecurityComponent,
    SignUpComponent,
    SignInComponent
  ],
  imports: [
    CommonModule,
    RouterModule,
    FormsModule,
    ReactiveFormsModule,

    MatInputModule,
    MatFormFieldModule,
    MatButtonModule,
    MatIconModule,
    MatTabsModule,

    SecurityRoutingModule
  ],
  providers: [
    MessagesService,
  ]
})
export class SecurityModule { }
