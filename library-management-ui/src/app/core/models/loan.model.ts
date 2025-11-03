export enum LoanStatus {
  Borrowed = 0,
  Returned = 1,
  Overdue = 2
}

export interface Loan {
  id: number;
  bookId: number;
  bookTitle?: string;
  memberId: number;
  memberName?: string;
  borrowedAt?: string;
  loanDate: string;
  dueDate?: string | null;
  returnDate?: string | null;
  status: LoanStatus;
}

export type LoanPayload = Omit<Loan, 'id' | 'bookTitle' | 'memberName'>;
