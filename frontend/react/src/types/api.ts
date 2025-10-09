import type { Participant } from "@components/room-page/types";

export type GetRoomResponse = {
  id: number;
  createdOn: string;
  modifiedOn: string;
  closedOn?: string;
  adminId: number;
  invitationCode: string;
  invitationLink: string;
  name: string;
  description: string;
  invitationNote: string;
  giftExchangeDate: string;
  giftMaximumBudget: number;
  isFull: boolean;
};

export type GetParticipantsResponse = Participant[];
