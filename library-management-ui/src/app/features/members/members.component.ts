import { Component, OnInit, inject } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { AsyncPipe, NgFor } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatTableModule } from '@angular/material/table';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { Observable, tap } from 'rxjs';
import { Member, MemberPayload } from '../../core/models/member.model';
import { MemberService } from '../../core/services/member.service';

@Component({
  selector: 'app-members',
  standalone: true,
  imports: [
    AsyncPipe,
    NgFor,
    ReactiveFormsModule,
    MatCardModule,
    MatTableModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatIconModule
  ],
  templateUrl: './members.component.html',
  styleUrls: ['./members.component.scss']
})
export class MembersComponent implements OnInit {
  private readonly fb = inject(FormBuilder);
  private readonly memberService = inject(MemberService);

  members$!: Observable<Member[]>;
  displayedColumns = ['fullName', 'email', 'phoneNumber', 'actions'];
  editingId: number | null = null;

  readonly form = this.fb.nonNullable.group({
    fullName: ['', Validators.required],
    email: ['', [Validators.required, Validators.email]],
    phoneNumber: [''],
    address: ['']
  });

  ngOnInit(): void {
    this.loadData();
  }

  loadData(): void {
    this.members$ = this.memberService.list();
  }

  edit(member: Member): void {
    this.editingId = member.id;
    this.form.patchValue({
      fullName: member.fullName,
      email: member.email,
      phoneNumber: member.phoneNumber ?? '',
      address: member.address ?? ''
    });
  }

  reset(): void {
    this.editingId = null;
    this.form.reset({ fullName: '', email: '', phoneNumber: '', address: '' });
  }

  save(): void {
    if (this.form.invalid) {
      return;
    }

    const payload = this.form.getRawValue() as MemberPayload;
    const operation = this.editingId
      ? this.memberService.update(this.editingId, payload)
      : this.memberService.create(payload);

    operation.pipe(tap(() => this.reset())).subscribe(() => this.loadData());
  }

  delete(member: Member): void {
    if (!confirm(`Delete member ${member.fullName}?`)) {
      return;
    }

    this.memberService.delete(member.id).subscribe(() => this.loadData());
  }
}
