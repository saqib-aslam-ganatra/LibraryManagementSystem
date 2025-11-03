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
import { Author, AuthorPayload } from '../../core/models/author.model';
import { AuthorService } from '../../core/services/author.service';

@Component({
  selector: 'app-authors',
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
  templateUrl: './authors.component.html',
  styleUrls: ['./authors.component.scss']
})
export class AuthorsComponent implements OnInit {
  private readonly fb = inject(FormBuilder);
  private readonly authorService = inject(AuthorService);

  authors$!: Observable<Author[]>;
  displayedColumns = ['name', 'biography', 'actions'];
  editingId: number | null = null;

  readonly form = this.fb.nonNullable.group({
    name: ['', Validators.required],
    biography: ['']
  });

  ngOnInit(): void {
    this.loadData();
  }

  loadData(): void {
    this.authors$ = this.authorService.list();
  }

  edit(author: Author): void {
    this.editingId = author.id;
    this.form.patchValue({
      name: author.name,
      biography: author.biography ?? ''
    });
  }

  reset(): void {
    this.editingId = null;
    this.form.reset({ name: '', biography: '' });
  }

  save(): void {
    if (this.form.invalid) {
      return;
    }

    const payload = this.form.getRawValue() as AuthorPayload;
    const operation = this.editingId
      ? this.authorService.update(this.editingId, payload)
      : this.authorService.create(payload);

    operation.pipe(tap(() => this.reset())).subscribe(() => this.loadData());
  }

  delete(author: Author): void {
    if (!confirm(`Delete author ${author.name}?`)) {
      return;
    }

    this.authorService.delete(author.id).subscribe(() => this.loadData());
  }
}
