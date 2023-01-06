import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { NavbarComponent } from './components/navbar/navbar.component';
import { SidebarComponent } from './components/sidebar/sidebar.component';

let components = [
  NavbarComponent,
  SidebarComponent,
]

@NgModule({
  declarations: components,
  exports: components,
  imports: [
    CommonModule
  ],
})
export class SharedModule { }
