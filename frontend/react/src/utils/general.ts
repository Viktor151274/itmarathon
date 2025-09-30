import config from "../../config.json";

const { protocol, host } = window.location;

export const BASE_API_URL = config?.environment?.backendApiUrl;
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

export const removeIdFromArray = <T extends { id?: number }>(
  array: T[],
): Omit<T, "id">[] => {
  return array.map(({ id, ...rest }) => {
    void id;
    return rest;
  });
};

export const formatBudget = (budget?: number) => {
  if (budget === undefined || budget === null) return "No data";
  if (budget === 0) return "Unlimited";
  return `${budget} UAH`;
};
