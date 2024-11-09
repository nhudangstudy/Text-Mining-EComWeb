import { Component, Input, NgZone } from '@angular/core';
import { AuthService } from '../service/auth.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent {
  @Input() isNavbarVisible: boolean = true;
  isLoggedIn: boolean = false;
  isDropdownOpen: boolean = false;

  constructor(private authService: AuthService, private ngZone: NgZone) {
    // Check if the user is logged in
    this.isLoggedIn = this.authService.isAuthenticated();
  }

  logout(): void {
    // Your existing service logic to log the user out
    this.authService.logout(); // Replace with your actual logout logic
  }
  

  toggleDropdown(event: Event): void {
    event.preventDefault(); // Prevent default anchor behavior
    // Your existing logic to toggle the dropdown menu, if applicable
  }
  

  goToProfile() {
    console.log('Navigating to profile...');
    // Implement navigation logic here
  }
}
