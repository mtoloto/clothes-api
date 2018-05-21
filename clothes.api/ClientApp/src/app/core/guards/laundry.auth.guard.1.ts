import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { Observable } from 'rxjs/Observable';
import { LaundryAuthService } from '../services/laundry.auth.service';

@Injectable()
export class LaundryAuthGuard implements CanActivate {
  constructor(private authService: LaundryAuthService, private router: Router) { }
  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean> | Promise<boolean> | boolean {
    if (!this.authService.isLoggedIn()) {
      this.router.navigate(['/lavanderia/login']);
      return false;
    }
    return true;
  }
}