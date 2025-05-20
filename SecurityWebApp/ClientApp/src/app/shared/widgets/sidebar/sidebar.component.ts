import { Component } from '@angular/core';
import { CoreService } from 'src/app/shared/services/core.service';
import { SharedService } from 'src/app/shared/services/shared.service';

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.scss']
})
export class SidebarComponent {

  constructor(
    public coreService: CoreService,
    public sharedService: SharedService
  ) { }
}
