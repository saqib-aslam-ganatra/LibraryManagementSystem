import { Component, OnInit, inject } from '@angular/core';
import { AsyncPipe, DatePipe, NgFor, NgIf } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatTableModule } from '@angular/material/table';
import { DashboardService } from '../../core/services/dashboard.service';
import { ReportService } from '../../core/services/report.service';
import { DashboardSummary } from '../../core/models/dashboard.model';
import { LoanStatus } from '../../core/models/loan.model';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [
    NgIf,
    NgFor,
    AsyncPipe,
    DatePipe,
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatTableModule
  ],
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {
  private readonly dashboardService = inject(DashboardService);
  private readonly reportService = inject(ReportService);

  summary$!: Observable<DashboardSummary>;
  readonly displayedColumns = ['bookTitle', 'memberName', 'loanDate', 'dueDate', 'status'];
  readonly LoanStatus = LoanStatus;

  ngOnInit(): void {
    this.summary$ = this.dashboardService.getSummary();
  }

  async download(entity: string, format: 'csv' | 'pdf' | 'xlsx'): Promise<void> {
    await this.reportService.download(entity, format);
  }
}
