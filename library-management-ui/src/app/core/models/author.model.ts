export interface Author {
  id: number;
  name: string;
  biography?: string;
}

export type AuthorPayload = Omit<Author, 'id'>;
