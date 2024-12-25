import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { JwtHelperService } from '@auth0/angular-jwt';

@Component({
  selector: 'app-register',
  imports: [FormsModule, CommonModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css',
})
export class RegisterComponent {
  username: string = '';
  email: string = '';
  password: string = '';
  confirmPassword: string = '';
  errorMessage: string = '';

  constructor(private authService: AuthService, private router: Router) {}

  // Handle form submission
  onSubmit() {
    if (this.password !== this.confirmPassword) {
      this.errorMessage = 'Passwords do not match';
      return;
    }

    if (this.username && this.email && this.password) {
      // Call signup service
      this.authService.register(this.username, this.email, this.password).subscribe(
        (response: any) => {
          // Store the token in localStorage (or cookies/sessionStorage)
          localStorage.setItem('token', response.token);

          // Redirect to the login page after successful registration
          this.router.navigate(['/login']);
        },
        (error: any) => {
          // Handle signup error
          this.errorMessage = 'Registration failed. Please try again.';
        }
      );
    }
  }
}
