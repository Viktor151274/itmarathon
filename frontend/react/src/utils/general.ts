const { protocol, host } = window.location;

// TODO: to be defind with DevOps
export const BASE_API_URL = "http://localhost:8080";
export const BASE_FRONTEND_URL = `${protocol}//${host}`;

export const generateRoomLink = (invitationCode: string) => {
  return `${BASE_FRONTEND_URL}/join/${invitationCode}`;
};

export const generateParticipantLink = (userCode: string) => {
  return `${BASE_FRONTEND_URL}/room/${userCode}`;
};

export const generateInvitationNoteContent = (
  invitationNote: string,
  invitationCode: string,
) => {
  const roomLink = generateRoomLink(invitationCode);

  return `${invitationNote}\n${roomLink}`;
};
