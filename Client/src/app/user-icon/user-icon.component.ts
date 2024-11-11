// user-icon.component.ts
import { Component, Input } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../service/auth.service';

@Component({
  selector: 'app-user-icon',
  templateUrl: './user-icon.component.html',
  styleUrls: ['./user-icon.component.css']
})
export class UserIconComponent {
  @Input() isLoggedIn: boolean = false;

  constructor(private router: Router, private authService: AuthService) {}

  navigateToLogin() {
    this.router.navigate(['/login']);
  }

  onProfile() {
    // Navigate to profile page
    this.router.navigate(['/profile']);
  }

  onLogout() {
    this.authService.logout();
    // Implement logout logic
  }
}
