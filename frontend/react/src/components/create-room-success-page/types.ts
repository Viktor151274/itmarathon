export type RoomAndUserData = {
  invitationCode: string;
  invitationNote: string;
  userCode: string;
};

export interface CreateRoomSuccessPageProps {
  roomAndUserData?: RoomAndUserData;
}
