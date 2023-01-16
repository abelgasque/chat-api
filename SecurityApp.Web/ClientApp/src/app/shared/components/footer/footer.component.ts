import { Component, OnInit } from '@angular/core';

import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-footer',
  templateUrl: './footer.component.html',
  styleUrls: ['./footer.component.scss']
})
export class FooterComponent implements OnInit {

  version: string;
  yearNow: string;
  linkSwagger: string;

  constructor() {
    this.version = environment.version;
    this.yearNow = new Date().getFullYear().toString();
    this.linkSwagger =  `${environment.baseUrlApi}/swagger`;
  }

  ngOnInit(): void {
  }

}
