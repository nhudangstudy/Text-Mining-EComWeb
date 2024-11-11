import { Component } from '@angular/core';
import { AuthService } from '../service/auth.service';
import { MessageBoxService } from '../service/message-box-service.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-custom-dropdown',
  templateUrl: './custom-dropdown.component.html',
  styleUrls: ['./custom-dropdown.component.css']
})
export class CustomDropdownComponent {
  isOpen = false;

  constructor(private authService: AuthService, private messageBoxService: MessageBoxService, private router: Router) {}

  toggleDropdown(): void {
    this.isOpen = !this.isOpen;
  }

  closeDropdown(): void {
    this.isOpen = false;
  }

  logout() {
    this.authService.logout();
  }
}
