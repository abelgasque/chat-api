import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class SharedService {

  public openedSidebar: boolean = false;
  public openedSpinner: boolean = false;

  constructor() { }

  toggleSidebar() {
    this.openedSidebar = !this.openedSidebar;
  }

  openSpinner() {
    this.openedSpinner = true;
  }

  closeSpinner() {
    this.openedSpinner = false;
  }
}
