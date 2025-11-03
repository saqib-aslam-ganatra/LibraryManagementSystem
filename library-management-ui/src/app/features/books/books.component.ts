import { Component, OnInit, inject } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { AsyncPipe, NgFor, NgIf } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatTableModule } from '@angular/material/table';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatSelectModule } from '@angular/material/select';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { MatIconModule } from '@angular/material/icon';
import { Observable, tap } from 'rxjs';
import { Book, BookPayload } from '../../core/models/book.model';
import { Author } from '../../core/models/author.model';
import { BookService } from '../../core/services/book.service';
import { AuthorService } from '../../core/services/author.service';

@Component({
  selector: 'app-books',
  standalone: true,
  imports: [
    AsyncPipe,
    NgFor,
    NgIf,
    ReactiveFormsModule,
    MatCardModule,
    MatTableModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatSelectModule,
    MatSlideToggleModule,
    MatIconModule
  ],
  templateUrl: './books.component.html',
  styleUrls: ['./books.component.scss']
})
export class BooksComponent implements OnInit {
  private readonly fb = inject(FormBuilder);
  private readonly bookService = inject(BookService);
  private readonly authorService = inject(AuthorService);

  books$!: Observable<Book[]>;
  authors$!: Observable<Author[]>;
  displayedColumns = ['title', 'isbn', 'author', 'availableCopies', 'actions'];
  editingId: number | null = null;

  readonly form = this.fb.nonNullable.group({
    title: ['', Validators.required],
    isbn: ['', Validators.required],
    authorId: [0, Validators.required],
    description: [''],
    totalCopies: [1, [Validators.required, Validators.min(1)]],
    availableCopies: [1, [Validators.required, Validators.min(0)]],
    replacementCost: [0, [Validators.required, Validators.min(0)]],
    isAvailable: [true]
  });

  ngOnInit(): void {
    this.loadData();
  }

  loadData(): void {
    this.books$ = this.bookService.list();
    this.authors$ = this.authorService.list();
  }

  edit(book: Book): void {
    this.editingId = book.id;
    this.form.patchValue({
      title: book.title,
      isbn: book.isbn,
      authorId: book.authorId,
      description: book.description ?? '',
      totalCopies: book.totalCopies,
      availableCopies: book.availableCopies,
      replacementCost: book.replacementCost,
      isAvailable: book.isAvailable
    });
  }

  reset(): void {
    this.editingId = null;
    this.form.reset({
      title: '',
      isbn: '',
      authorId: 0,
      description: '',
      totalCopies: 1,
      availableCopies: 1,
      replacementCost: 0,
      isAvailable: true
    });
  }

  save(): void {
    if (this.form.invalid) {
      return;
    }

    const payload = this.form.getRawValue() as BookPayload;
    const operation = this.editingId
      ? this.bookService.update(this.editingId, payload)
      : this.bookService.create(payload);

    operation.pipe(tap(() => this.reset())).subscribe(() => this.loadData());
  }

  delete(book: Book): void {
    if (!confirm(`Delete book ${book.title}?`)) {
      return;
    }

    this.bookService.delete(book.id).subscribe(() => this.loadData());
  }
}
