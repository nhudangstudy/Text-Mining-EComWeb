// user-icon.component.ts
import { Component, Input } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-user-icon',
  templateUrl: './user-icon.component.html',
  styleUrls: ['./user-icon.component.css']
})
export class UserIconComponent {
  @Input() isLoggedIn: boolean = false;

  constructor(private router: Router) {}

  navigateToLogin() {
    this.router.navigate(['/login']);
  }

  onProfile() {
    // Navigate to profile page
    this.router.navigate(['/profile']);
  }

  onLogout() {
    // Implement logout logic
    console.log('User logged out');
    this.isLoggedIn = false;
    this.router.navigate(['/login']);
  }
}
