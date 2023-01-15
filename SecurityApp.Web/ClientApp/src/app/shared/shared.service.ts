import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class SharedService {

  public openedSidebar: boolean = false;

  constructor() { }

  toggleSidebar() {
    this.openedSidebar = !this.openedSidebar;
  }
}
