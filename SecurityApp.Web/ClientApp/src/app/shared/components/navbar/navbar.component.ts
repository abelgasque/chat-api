import { Component } from '@angular/core';
import { Router } from '@angular/router';

import { SharedService } from 'src/app/shared/services/shared.service';
import { CoreService } from 'src/app/shared/services/core.service';
import { SecurityService } from 'src/app/shared/services/security.service';

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
    public securityService: SecurityService,
  ) { }
}
