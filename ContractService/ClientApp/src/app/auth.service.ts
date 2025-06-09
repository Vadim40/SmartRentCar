import { Injectable } from '@angular/core';
import {jwtDecode} from 'jwt-decode';

interface JwtPayload {
  role: string;
}
@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private tokenKey = 'auth_token'; // ключ в localStorage

  getToken(): string | null {
    return localStorage.getItem(this.tokenKey);
  }
  saveToken(token: string): void {
    localStorage.setItem(this.tokenKey, token);
  }
  
  clearToken(): void {
    localStorage.removeItem(this.tokenKey);
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
