import { Component, Input } from '@angular/core';
import { AuthService } from '../service/auth.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent {
  @Input() isNavbarVisible: boolean = true;
  
  isLoggedIn: boolean = false;

  constructor(private authService: AuthService) {
    // Check if the user is logged in (replace with your actual auth check)
    this.isLoggedIn = this.authService.isAuthenticated();
  }
}