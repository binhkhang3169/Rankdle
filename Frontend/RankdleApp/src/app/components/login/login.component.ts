import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { FormsModule } from '@angular/forms'; // Import FormsModule here
import { CommonModule } from '@angular/common'; // Để hỗ trợ *ngIf, *ngFor
import { JwtHelperService } from '@auth0/angular-jwt';

@Component({
  selector: 'app-login',
  imports:[FormsModule,CommonModule],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
  providers:[JwtHelperService]
})
export class LoginComponent implements OnInit {
  username: string = '';
  password: string = '';
  errorMessage: string = '';

  constructor(private authService: AuthService, private router: Router) {}
  ngOnInit(): void {
    this.authService.logout();    
  }

  // Handle form submission
  onSubmit() {
    if (this.username && this.password) {
      // Call login service
      this.authService.login(this.username, this.password).subscribe(
        (response: any) => {
          // Store the token in localStorage (or cookies/sessionStorage)
          localStorage.setItem('token', response.token);

          // Redirect to the dashboard or home page after successful login
          this.router.navigate(['/']);
        },
        (error: any) => {
          // Handle login error (e.g., invalid credentials)
          this.errorMessage = 'Invalid username or password';
        }
      );
    }
  }
}
