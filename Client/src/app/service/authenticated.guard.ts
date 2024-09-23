import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { CanDeactivate } from '@angular/router';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root',
})
export class AuthenticatedGuard implements CanActivate {
  constructor(private authService: AuthService, private router: Router) {}

  canActivate(): boolean {
    if (this.authService.isAuthenticated()) {
        return false;
      }
      return true;
  }
}
