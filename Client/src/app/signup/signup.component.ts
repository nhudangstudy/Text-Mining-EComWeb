import { Component } from '@angular/core';
import { Api, CreateUpdateUserRequestModel, RegisterRequestAccountModel } from 'src/myApi';
import { MessageBoxService } from '../service/message-box-service.service';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css'] // Include your styles if needed
})
export class SignupComponent {

  private api = new Api();
  firstName: string = '';
  lastName: string = '';
  dob: string = '';
  email: string = '';
  password: string = '';
  rePassword: string = '';
  user: CreateUpdateUserRequestModel | undefined;
  account: RegisterRequestAccountModel | undefined;

  constructor(private messageBoxService: MessageBoxService) {}

  validateInput(): boolean {
    // Check if all fields are filled
    if (!this.firstName || !this.lastName || !this.dob || !this.email || !this.password || !this.rePassword) {
      this.messageBoxService.error('All fields are required.');
      return false;
    }

    // Validate email format
    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    if (!emailRegex.test(this.email)) {
      this.messageBoxService.warning('Please enter a valid email address.');
      return false;
    }

    // Check password length
    if (this.password.length < 6) {
      this.messageBoxService.warning('Password must be at least 6 characters long.');
      return false;
    }

    // Check if passwords match
    if (this.password !== this.rePassword) {
      this.messageBoxService.error('Passwords do not match!');
      return false;
    }

    // Additional checks, such as password complexity, can be added here if needed

    return true;
  }

  createAccount() {
    // Validate input before proceeding
    if (!this.validateInput()) {
      return;
    }

    // Prepare user model
    this.user = {
      firstName: this.firstName,
      lastName: this.lastName,
      dateOfBirth: this.dob,
    };

    // Prepare account model
    this.account = {
      email: this.email,
      password: this.password,
      repeatPassword: this.rePassword,
    };

    // Create account API call
    this.api.api.accountsCreate(this.account)
      .then(res1 => {
        this.messageBoxService.success('Account created successfully.');
        // Create user API call if account creation succeeds
        this.api.user.userCreate(this.email, this.user!)
          .then(res2 => {
            this.messageBoxService.success('User created successfully.');
          })
          .catch(error => {
            console.error('Error creating user:', error);
            this.messageBoxService.error('Error creating user. Please try again.');
          });
      })
      .catch(error => {
        console.error('Error creating account:', error);
        this.messageBoxService.error('Error creating account. Please check your details and try again.');
      });
  }
}
