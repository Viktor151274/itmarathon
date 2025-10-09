import type { Participant } from "../types";

export const getCurrentUser = (userCode: string, users?: Participant[]) =>
  users?.find((user) => user.userCode === userCode);
