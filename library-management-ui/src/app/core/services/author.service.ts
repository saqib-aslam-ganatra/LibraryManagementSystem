import { Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiService } from './api.service';
import { Author, AuthorPayload } from '../models/author.model';

@Injectable({ providedIn: 'root' })
export class AuthorService {
  private readonly api = inject(ApiService);

  list(): Observable<Author[]> {
    return this.api.get<Author[]>('authors');
  }

  create(payload: AuthorPayload): Observable<Author> {
    return this.api.post<Author>('authors', payload);
  }

  update(id: number, payload: AuthorPayload): Observable<void> {
    return this.api.put<void>(`authors/${id}`, { ...payload, id });
  }

  delete(id: number): Observable<void> {
    return this.api.delete<void>(`authors/${id}`);
  }
}
