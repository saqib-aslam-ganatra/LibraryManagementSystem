export interface Book {
  id: number;
  title: string;
  isbn: string;
  authorId: number;
  authorName?: string;
  description?: string;
  totalCopies: number;
  availableCopies: number;
  replacementCost: number;
  isAvailable: boolean;
}

export type BookPayload = Omit<Book, 'id' | 'authorName'>;
