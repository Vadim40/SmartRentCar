import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import {jwtDecode} from 'jwt-decode';
import { Observable, tap, map } from 'rxjs';

interface JwtPayload {
  role: string;
}
@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private tokenKey = 'auth_token'; // ключ в localStorage

  constructor(private http: HttpClient)
  {}
  getToken(): string | null {
    return localStorage.getItem(this.tokenKey);
  }

  saveToken(token: string): void {
    localStorage.setItem(this.tokenKey, token);
  }

  clearToken(): void {
    localStorage.removeItem(this.tokenKey);
  }

  login(username: string, password: string): Observable<void> {
    return this.http.post<{ token: string }>('/api/login', { username, password }).pipe(
      tap(response => this.saveToken(response.token)),
      map(() => {}) 
    );
  }

  
  getUserRole(): string | null {
    const token = this.getToken();
    if (!token) return null;

    try {
      const decoded = jwtDecode<JwtPayload>(token);
      return decoded.role;
    } catch (e) {
      return null;
    }
  }
}
