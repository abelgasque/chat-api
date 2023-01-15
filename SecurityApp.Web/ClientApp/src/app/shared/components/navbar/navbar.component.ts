import { Component } from '@angular/core';
import { Router } from '@angular/router';

import { CoreService } from 'src/app/core/core.service';
import { SharedService } from 'src/app/shared/shared.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent {

  constructor(
    public router: Router,
    public coreService: CoreService,
    public sharedService: SharedService,
  ) { }

  toggleSidebar() {
    this.sharedService.openedSidebar = !this.sharedService.openedSidebar;
  }

  signOut() {
    this.sharedService.openedSidebar = false;
    this.coreService.setTokenLocalStorage('');
    this.coreService.setCustomerLocalStorage({});
    this.router.navigate(['/']);
  }
}
