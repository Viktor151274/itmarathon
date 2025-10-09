import type { GetParticipantsResponse, GetRoomResponse } from "@types/api";

export interface RoomPageContentProps {
  users?: GetParticipantsResponse;
  roomDetails?: GetRoomResponse;
}
