import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { CanDeactivate } from '@angular/router';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root',
})
export class NotAuthenticatedGuard implements CanActivate {
  constructor(private authService: AuthService, private router: Router) {}

  canActivate(): boolean {
    if (this.authService.autoLogin()) {
      return true;
    }
    this.router.navigate(['/login']);
    return false;
  }
}
