import { Injectable } from '@angular/core';
import { Router } from '@angular/router';

import { SharedService } from './shared.service';
import { CoreService } from './core.service';

@Injectable({
  providedIn: 'root'
})
export class SecurityService {

  constructor(
    public router: Router,
    private coreService: CoreService,
    public sharedService: SharedService,
  ) { }

  signOut() {
    this.sharedService.openedSidebar = false;
    this.coreService.setTokenLocalStorage('');
    this.coreService.setCustomerLocalStorage({});
    this.router.navigate(['/']);
  }
}
