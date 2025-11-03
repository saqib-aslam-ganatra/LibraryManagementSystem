import { LoanStatus } from './loan.model';

export interface LoanSnapshot {
  id: number;
  bookTitle: string;
  memberName: string;
  loanDate: string;
  dueDate?: string | null;
  status: LoanStatus;
}

export interface DashboardSummary {
  totalBooks: number;
  availableBooks: number;
  totalAuthors: number;
  totalMembers: number;
  activeLoans: number;
  overdueLoans: number;
  recentLoans: LoanSnapshot[];
}
