import { Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiService } from './api.service';
import { Member, MemberPayload } from '../models/member.model';

@Injectable({ providedIn: 'root' })
export class MemberService {
  private readonly api = inject(ApiService);

  list(): Observable<Member[]> {
    return this.api.get<Member[]>('members');
  }

  create(payload: MemberPayload): Observable<Member> {
    return this.api.post<Member>('members', payload);
  }

  update(id: number, payload: MemberPayload): Observable<void> {
    return this.api.put<void>(`members/${id}`, { ...payload, id });
  }

  delete(id: number): Observable<void> {
    return this.api.delete<void>(`members/${id}`);
  }
}
