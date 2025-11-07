import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, map, tap } from 'rxjs';
import { LoginRequest, LoginResponse } from '../models/auth.model';
import { User } from '../models/user.model';
import { environment } from '../../../environments/environment';

const TOKEN_KEY = 'library_token';
const USER_KEY = 'library_user';

type TokenPayload = Record<string, unknown>;

@Injectable({ providedIn: 'root' })
export class AuthService {
  private readonly http = inject(HttpClient);
  private readonly state = new BehaviorSubject<User | null>(this.restoreUser());

  readonly user$ = this.state.asObservable();

  login(request: LoginRequest): Observable<User> {
    return this.http.post<LoginResponse>(`${environment.apiUrl}/auth/login`, request).pipe(
      map(response => this.createUserFromToken(response.token)),
      tap(user => this.persistAuth(user)),
      tap(user => this.state.next(user))
    );
  }

  logout(): void {
    localStorage.removeItem(TOKEN_KEY);
    localStorage.removeItem(USER_KEY);
    this.state.next(null);
  }

  get token(): string | null {
    return this.state.value?.token ?? localStorage.getItem(TOKEN_KEY);
  }

  isLoggedIn(): boolean {
    return !!this.state.value;
  }

  private persistAuth(user: User): void {
    localStorage.setItem(TOKEN_KEY, user.token);
    localStorage.setItem(USER_KEY, JSON.stringify(user));
  }

  private restoreUser(): User | null {
    const raw = localStorage.getItem(USER_KEY);
    if (!raw) {
      return null;
    }

    try {
      const stored = JSON.parse(raw) as User;
      if (!stored.token) {
        const token = localStorage.getItem(TOKEN_KEY);
        if (!token) {
          return null;
        }

        return { ...stored, token };
      }

      return stored;
    } catch {
      return null;
    }
  }

  private createUserFromToken(token: string): User {
    const payload = this.decodeToken(token);
    const roles = this.extractRoles(payload);
    const id = typeof payload.sub === 'string' ? payload.sub : '';
    const email = typeof payload.email === 'string' ? payload.email : '';
    const displayName =
      typeof payload.unique_name === 'string'
        ? payload.unique_name
        : typeof payload.name === 'string'
        ? payload.name
        : email || 'User';

    return {
      id,
      email,
      displayName,
      roles,
      token
    };
  }

  private decodeToken(token: string): TokenPayload {
    try {
      const payload = token.split('.')[1] ?? '';
      const normalized = payload.replace(/-/g, '+').replace(/_/g, '/');
      const padded = normalized.padEnd(normalized.length + (4 - (normalized.length % 4)) % 4, '=');
      const decoded = atob(padded);
      const json = decodeURIComponent(
        decoded
          .split('')
          .map(char => `%${char.charCodeAt(0).toString(16).padStart(2, '0')}`)
          .join('')
      );

      return JSON.parse(json) as TokenPayload;
    } catch {
      return {};
    }
  }

  private extractRoles(payload: TokenPayload): string[] {
    const role = payload['role'];

    if (!role) {
      return [];
    }

    if (Array.isArray(role)) {
      return role.filter((value): value is string => typeof value === 'string');
    }

    if (typeof role === 'string') {
      return [role];
    }

    return [];
  }
}
