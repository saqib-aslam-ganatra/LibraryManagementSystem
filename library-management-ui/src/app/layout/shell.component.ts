import { Component, inject, signal } from '@angular/core';
import { Router, RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';
import { AsyncPipe, NgIf } from '@angular/common';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatListModule } from '@angular/material/list';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { AuthService } from '../core/services/auth.service';

@Component({
  selector: 'app-shell',
  standalone: true,
  imports: [
    RouterOutlet,
    RouterLink,
    RouterLinkActive,
    MatToolbarModule,
    MatSidenavModule,
    MatListModule,
    MatIconModule,
    MatButtonModule,
    NgIf,
    AsyncPipe
  ],
  templateUrl: './shell.component.html',
  styleUrls: ['./shell.component.scss']
})
export class ShellComponent {
  private readonly authService = inject(AuthService);
  private readonly router = inject(Router);
  readonly drawerOpened = signal(true);

  readonly navigationItems = [
    { icon: 'dashboard', label: 'Dashboard', route: '/dashboard' },
    { icon: 'library_books', label: 'Books', route: '/books' },
    { icon: 'people', label: 'Members', route: '/members' },
    { icon: 'person', label: 'Authors', route: '/authors' },
    { icon: 'assignment', label: 'Loans', route: '/loans' }
  ];

  user$ = this.authService.user$;

  toggleDrawer(): void {
    this.drawerOpened.update(opened => !opened);
  }

  async signOut(): Promise<void> {
    this.authService.logout();
    await this.router.navigate(['/login']);
  }
}
