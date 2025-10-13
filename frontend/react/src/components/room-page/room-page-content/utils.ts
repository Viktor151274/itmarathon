import type { Participant } from "@types/api";

export const getCurrentUser = (
  userCode: string,
  participants?: Participant[],
) => participants?.find((user) => user.userCode === userCode);
