import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';

import { CoreService } from 'src/app/shared/services/core.service';
import { RefreshTokenDTO } from '../models/refreshTokenDTO.interface';
import { MessagesService } from '../services/messages.service';
import { SecurityService } from '../services/security.service';
import { SharedService } from '../services/shared.service';
import { TokenService } from '../services/token.service';
@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  constructor(
    public router: Router,
    private coreService: CoreService,
    private securityService: SecurityService,
    private sharedService: SharedService,
    private tokenService: TokenService,
    private messagesService: MessagesService,
  ) { }

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    console.log(state.url);
    if (this.coreService.token.length <= 0) {
      this.router.navigate(['/page-not-authorized']);
    } else if (!this.coreService.isValidToken()) {
      this.sharedService.openSpinner();

      let refreshTokenDTO: RefreshTokenDTO = {
        accessToken: this.coreService.token,
        id: this.coreService.customer.id
      };

      this.tokenService.refresh(refreshTokenDTO).subscribe({
        next: (resp: any) => {
          this.coreService.setTokenLocalStorage(resp.accessToken);
          this.coreService.setCustomerLocalStorage(resp.customer);
          this.router.navigate([state.url]);
          this.messagesService.success('Success', 'User logged in successfully!');
          this.sharedService.closeSpinner();
          return true;
        },
        error: (error: any) => {
          this.messagesService.errorHandler(error);
          this.securityService.signOut();
          return false;
        }
      });
    } else {
      return true;
    }

    return false;
  }

}
