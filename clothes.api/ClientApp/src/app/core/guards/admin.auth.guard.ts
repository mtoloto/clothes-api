import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { Observable } from 'rxjs/Observable';
import { AdminAuthService } from '../services/admin.auth.service';

@Injectable()
export class AdminAuthGuard implements CanActivate {
  constructor(private authService: AdminAuthService, private router: Router) { }
  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean> | Promise<boolean> | boolean {
    if (!this.authService.isLoggedIn()) {
      this.router.navigate(['/admin/login']);
      return false;
    }
    return true;
  }
}