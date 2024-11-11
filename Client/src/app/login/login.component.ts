import { Component } from '@angular/core';
import { AuthService } from '../service/auth.service';
import { Router } from '@angular/router';
import { MessageBoxService } from '../service/message-box-service.service';
import { LoadingService } from '../service/loading.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent {
  email = '';
  password = '';
  errorMessage = '';

  constructor(private authService: AuthService, private router: Router, private messageService: MessageBoxService, private loadingService: LoadingService) {}

  async onLogin() {
    console.log(this.email, this.password);  // Check if values are captured correctly
    this.loadingService.startLoading()
    await this.authService.login(this.email, this.password).subscribe(
      (response) => {
        this.messageService.success("Login Sucessfully");
        if (this.authService.hasRole('Admin')) {
          this.router.navigate(['/admin']);
        } else {
          this.router.navigate(['/landing']);
        }
      },
      (error) => {
        this.messageService.error("Invalid login credentials. Please try again.");
      }
    );
    this.loadingService.stopLoading()
  }
}
