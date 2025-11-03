import { Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiService } from './api.service';
import { Loan, LoanPayload } from '../models/loan.model';

@Injectable({ providedIn: 'root' })
export class LoanService {
  private readonly api = inject(ApiService);

  list(): Observable<Loan[]> {
    return this.api.get<Loan[]>('loans');
  }

  create(payload: LoanPayload): Observable<Loan> {
    return this.api.post<Loan>('loans', payload);
  }

  update(id: number, payload: LoanPayload): Observable<void> {
    return this.api.put<void>(`loans/${id}`, { ...payload, id });
  }

  delete(id: number): Observable<void> {
    return this.api.delete<void>(`loans/${id}`);
  }
}
