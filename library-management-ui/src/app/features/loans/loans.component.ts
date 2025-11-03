import { Component, OnInit, inject } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { AsyncPipe, NgFor } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatTableModule } from '@angular/material/table';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatSelectModule } from '@angular/material/select';
import { MatIconModule } from '@angular/material/icon';
import { Observable, tap } from 'rxjs';
import { Loan, LoanPayload, LoanStatus } from '../../core/models/loan.model';
import { LoanService } from '../../core/services/loan.service';
import { Book } from '../../core/models/book.model';
import { Member } from '../../core/models/member.model';
import { BookService } from '../../core/services/book.service';
import { MemberService } from '../../core/services/member.service';

@Component({
  selector: 'app-loans',
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
    MatSelectModule,
    MatIconModule
  ],
  templateUrl: './loans.component.html',
  styleUrls: ['./loans.component.scss']
})
export class LoansComponent implements OnInit {
  private readonly fb = inject(FormBuilder);
  private readonly loanService = inject(LoanService);
  private readonly bookService = inject(BookService);
  private readonly memberService = inject(MemberService);

  loans$!: Observable<Loan[]>;
  books$!: Observable<Book[]>;
  members$!: Observable<Member[]>;
  displayedColumns = ['book', 'member', 'loanDate', 'dueDate', 'status', 'actions'];
  editingId: number | null = null;
  readonly LoanStatus = LoanStatus;
  readonly statusOptions = [LoanStatus.Borrowed, LoanStatus.Returned, LoanStatus.Overdue];

  readonly form = this.fb.nonNullable.group({
    bookId: [0, Validators.required],
    memberId: [0, Validators.required],
    loanDate: ['', Validators.required],
    dueDate: [''],
    returnDate: [''],
    status: [LoanStatus.Borrowed, Validators.required]
  });

  ngOnInit(): void {
    this.loadData();
  }

  loadData(): void {
    this.loans$ = this.loanService.list();
    this.books$ = this.bookService.list();
    this.members$ = this.memberService.list();
  }

  edit(loan: Loan): void {
    this.editingId = loan.id;
    this.form.patchValue({
      bookId: loan.bookId,
      memberId: loan.memberId,
      loanDate: loan.loanDate?.slice(0, 10),
      dueDate: loan.dueDate ? loan.dueDate.slice(0, 10) : '',
      returnDate: loan.returnDate ? loan.returnDate.slice(0, 10) : '',
      status: loan.status
    });
  }

  reset(): void {
    this.editingId = null;
    this.form.reset({
      bookId: 0,
      memberId: 0,
      loanDate: '',
      dueDate: '',
      returnDate: '',
      status: LoanStatus.Borrowed
    });
  }

  save(): void {
    if (this.form.invalid) {
      return;
    }

    const payload = this.form.getRawValue() as LoanPayload;
    const operation = this.editingId
      ? this.loanService.update(this.editingId, payload)
      : this.loanService.create(payload);

    operation.pipe(tap(() => this.reset())).subscribe(() => this.loadData());
  }

  delete(loan: Loan): void {
    if (!confirm('Delete this loan?')) {
      return;
    }

    this.loanService.delete(loan.id).subscribe(() => this.loadData());
  }
}
