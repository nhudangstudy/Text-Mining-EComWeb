import { Component, Input, NgZone } from '@angular/core';
import { AuthService } from '../service/auth.service';

@Component({
  selector: 'app-header-admin',
  templateUrl: './header-admin.component.html',
  styleUrls: ['./header-admin.component.css']
})
export class HeaderAdminComponent {
  @Input() isNavbarVisible: boolean = true;
  
  isLoggedIn: boolean = false;

  constructor(private authService: AuthService, private ngZone: NgZone) {
    // Check if the user is logged in (replace with your actual auth check)
    this.isLoggedIn = this.authService.isAuthenticated();
  }

  logout() {
    this.authService.logout();
  }

  toggleDropdown(event: Event): void {
    event.preventDefault(); // Prevent default anchor behavior
    // Your logic to toggle the dropdown menu
  }
}
