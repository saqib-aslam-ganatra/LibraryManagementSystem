import { Routes } from '@angular/router';
import { LoginComponent } from './features/auth/login/login.component';
import { DashboardComponent } from './features/dashboard/dashboard.component';
import { BooksComponent } from './features/books/books.component';
import { AuthorsComponent } from './features/authors/authors.component';
import { MembersComponent } from './features/members/members.component';
import { LoansComponent } from './features/loans/loans.component';
import { ShellComponent } from './layout/shell.component';
import { authGuard } from './core/guards/auth.guard';

export const routes: Routes = [
  { path: 'login', component: LoginComponent },
  {
    path: '',
    component: ShellComponent,
    canActivateChild: [authGuard],
    children: [
      { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
      { path: 'dashboard', component: DashboardComponent },
      { path: 'books', component: BooksComponent },
      { path: 'authors', component: AuthorsComponent },
      { path: 'members', component: MembersComponent },
      { path: 'loans', component: LoansComponent }
    ]
  },
  { path: '**', redirectTo: 'dashboard' }
];
