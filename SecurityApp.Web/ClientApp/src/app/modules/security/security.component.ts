import { Component } from '@angular/core';
import { Router } from '@angular/router';

import { SharedService } from 'src/app/shared/shared.service';

@Component({
  selector: 'app-security',
  templateUrl: './security.component.html',
  styleUrls: ['./security.component.scss']
})
export class SecurityComponent {

  activeLink: string;

  constructor(
    public router: Router,
    private sharedService: SharedService
  ) {
    this.sharedService.openedSidebar = false;
    this.activeLink = this.router.url.slice(10, this.router.url.length);
  }
}
