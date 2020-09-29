import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthService } from "../_services/auth.service";
import { ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  /**
   * Checks if user is in a given role
   */
  constructor(private authService: AuthService, private router: Router, private toastr: ToastrService) { }
  canActivate(
    route: ActivatedRouteSnapshot, 
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    let roles = route.data.roles as Array<string>;
    let role = this.authService.getUserRole();

    if(roles.includes(role)){
      return true;
    }

    this.toastr.error('You don\'t have permissoin to view this');
    this.authService.logOut();
    return false;
  }
  
}
