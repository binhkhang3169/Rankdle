import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { HttpHeaders } from '@angular/common/http';
import { AuthService } from './services/auth.service';

export const authInterceptor: HttpInterceptorFn = (req, next) => {
  const authService = inject(AuthService); // Inject AuthService to get the token
  const token = authService.getToken(); // Get the JWT token from your AuthService

  if (token) {
    // Clone the request and add the Authorization header
    req = req.clone({
      setHeaders: {
        Authorization: `Bearer ${token}`,
      },
    });
  }

  // Continue with the modified request
  return next(req);
};
