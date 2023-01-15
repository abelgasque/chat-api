import { Component } from '@angular/core';

import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent {

  linkSwagger: string;

  constructor() {
    this.linkSwagger =  `${environment.baseUrlApi}/swagger`;
  }

}
