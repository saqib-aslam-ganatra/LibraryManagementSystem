import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { map, tap } from 'rxjs/operators';
import { LoginRequest, LoginResponse } from '../models/auth.model';
import { User } from '../models/user.model';
import { environment } from '../../../environments/environment';

const TOKEN_KEY = 'library_token';
const USER_KEY = 'library_user';

@Injectable({ providedIn: 'root' })
export class AuthService {
  private readonly http = inject(HttpClient);
  private readonly state = new BehaviorSubject<User | null>(this.restoreUser());

  readonly user$ = this.state.asObservable();

  login(request: LoginRequest): Observable<User> {
    return this.http.post<LoginResponse>(`${environment.apiUrl}/auth/login`, request)
      .pipe(
        tap(response => this.persistAuth(response)),
        map(response => ({
          id: response.user.id,
          fullName: response.user.fullName,
          email: response.user.email,
          token: response.token
        })),
        tap(user => this.state.next(user))
      );
  }

  logout(): void {
    localStorage.removeItem(TOKEN_KEY);
    localStorage.removeItem(USER_KEY);
    this.state.next(null);
  }

  get token(): string | null {
    return localStorage.getItem(TOKEN_KEY);
  }

  isLoggedIn(): boolean {
    return !!this.state.value;
  }

  private persistAuth(response: LoginResponse): void {
    localStorage.setItem(TOKEN_KEY, response.token);
    localStorage.setItem(USER_KEY, JSON.stringify({
      id: response.user.id,
      fullName: response.user.fullName,
      email: response.user.email,
      token: response.token
    }));
  }

  private restoreUser(): User | null {
    const raw = localStorage.getItem(USER_KEY);
    if (!raw) {
      return null;
    }

    try {
      return JSON.parse(raw) as User;
    } catch {
      return null;
    }
  }
}
