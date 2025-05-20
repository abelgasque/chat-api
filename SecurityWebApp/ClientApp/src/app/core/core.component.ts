import { Component } from '@angular/core';
import { SharedService } from '../shared/services/shared.service';

@Component({
  selector: 'app-core',
  templateUrl: './core.component.html',
  styleUrls: ['./core.component.scss']
})
export class CoreComponent {

  constructor(public sharedService: SharedService) { }
}
