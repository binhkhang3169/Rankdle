import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { BehaviorSubject, Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Router } from '@angular/router';


@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private apiUrl = `${environment.apiUrl}/api/auth`;
  private currentUserSubject: BehaviorSubject<any>;
  constructor(private http: HttpClient, private jwtHelper: JwtHelperService, private router: Router) {
    const isBrowser = typeof window !== 'undefined' && typeof window.localStorage !== 'undefined';

    // Only access localStorage if we're in the browser
    const storedUser = isBrowser ? localStorage.getItem('user') : null;
    this.currentUserSubject = new BehaviorSubject<any>(storedUser ? JSON.parse(storedUser) : null);
  }

  login(username: string, password: string): Observable<any> {
    return this.http.post(`${this.apiUrl}/login`, { username, password });
  }

  register(username: string, email: string, password: string): Observable<any> {
    return this.http.post(`${this.apiUrl}/register`, { username, email, password });
  }

  forgotPassword(email: string): Observable<any> {
    return this.http.post(`${this.apiUrl}/forgot-password`, { email });
  }

  resetPassword(resetToken: string, newPassword: string): Observable<any> {
    return this.http.post(`${this.apiUrl}/reset-password`, { resetToken, newPassword });
  }

  verifyResetToken(resetToken: string): Observable<any> {
    return this.http.post(`${this.apiUrl}/verify-reset-token`, { resetToken });
  }

  // Store the JWT Token in localStorage and currentUserSubject
  saveToken(token: string): void {
    localStorage.setItem('token', token);
    this.currentUserSubject.next(this.jwtHelper.decodeToken(token));
  }

  // Check if the token is expired
  isAuthenticated(): boolean {
    const token = localStorage.getItem('token');
    return !!token && !this.jwtHelper.isTokenExpired(token);
  }

  getToken(): string | null {
    return localStorage.getItem('token');
  }

  logout(): void {
    localStorage.removeItem('token');
    this.currentUserSubject.next(null);
    this.router.navigate(['/login']);
  }

  get currentUserValue() {
    return this.currentUserSubject.value;
  }

  }
