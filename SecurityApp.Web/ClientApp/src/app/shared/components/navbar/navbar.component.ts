import { Component } from '@angular/core';
import { Router } from '@angular/router';

import { SharedService } from '../../shared.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent {

  constructor(
    public router: Router,
    public sharedService: SharedService,
  ) { }

  toggleSidebar() {
    this.sharedService.openedSidebar = !this.sharedService.openedSidebar;
  }
}
