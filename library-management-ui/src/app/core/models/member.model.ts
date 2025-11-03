export interface Member {
  id: number;
  fullName: string;
  email: string;
  phoneNumber?: string;
  address?: string;
}

export type MemberPayload = Omit<Member, 'id'>;
