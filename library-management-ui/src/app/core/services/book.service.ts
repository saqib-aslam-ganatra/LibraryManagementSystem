import { Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiService } from './api.service';
import { Author } from '../models/author.model';
import { Book, BookPayload } from '../models/book.model';

@Injectable({ providedIn: 'root' })
export class BookService {
  private readonly api = inject(ApiService);

  list(): Observable<Book[]> {
    return this.api.get<Book[]>('books');
  }

  get(id: number): Observable<Book> {
    return this.api.get<Book>(`books/${id}`);
  }

  create(payload: BookPayload): Observable<Book> {
    return this.api.post<Book>('books', payload);
  }

  update(id: number, payload: BookPayload): Observable<void> {
    return this.api.put<void>(`books/${id}`, { ...payload, id });
  }

  delete(id: number): Observable<void> {
    return this.api.delete<void>(`books/${id}`);
  }

  authors(): Observable<Author[]> {
    return this.api.get<Author[]>('authors');
  }
}
