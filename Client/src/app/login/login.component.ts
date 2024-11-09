import { Component } from '@angular/core';
import { AuthService } from '../service/auth.service';
import { Router } from '@angular/router';
import { MessageBoxService } from '../service/message-box-service.service';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent {
  email = '';
  password = '';
  errorMessage = '';

  constructor(private authService: AuthService, private router: Router, private messageService: MessageBoxService) {}

  onLogin() {
    console.log(this.email, this.password);  // Check if values are captured correctly

    this.authService.login(this.email, this.password).subscribe(
      (response) => {
        this.messageService.success("Login Sucessfully");
        this.router.navigate(['/landing']);
      },
      (error) => {
        this.messageService.error("Invalid login credentials. Please try again.");
      }
    );
  }
}
