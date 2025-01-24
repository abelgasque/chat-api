import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-page-redirect',
  templateUrl: './page-redirect.component.html',
  styleUrls: ['./page-redirect.component.scss']
})
export class PageRedirectComponent implements OnInit {

  title: string;
  statusCode: string;

  constructor(public router: Router) {
    switch (true) {
      case this.router.url.includes('page-not-authorized'): {
        this.title = 'Page not authorized';
        this.statusCode = '401';
        break;
      }
      default: {
        this.title = 'Page not found';
        this.statusCode = '404';
        break;
      }
    }
  }

  ngOnInit(): void {
  }
}
