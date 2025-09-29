import { createContext } from "react";
import type { FormContextType } from "./types";

const roomData = {
  name: "",
  description: "",
  giftExchangeDate: null,
  giftMaximumBudget: "",
};

const defaultUserData = {
  firstName: "",
  lastName: "",
  phone: "",
  email: "",
  deliveryInfo: "",
  wantSurprise: false,
  interests: "",
  wishList: [
    {
      id: Date.now(),
      name: "",
      infoLink: "",
    },
  ],
};

export const defaultRoomData = {
  room: {
    ...roomData,
  },
  user: {
    ...defaultUserData,
  },
};

export const defaultCreateRoomData = {
  room: {
    ...roomData,
  },
  adminUser: {
    ...defaultUserData,
  },
};

export const defaultContext = {
  currentStep: 0,
  onNextStep: () => {},
  onPreviousStep: () => {},
  roomData: defaultRoomData,
  setRoomData: () => {},
  getCreateRoomData: () => defaultCreateRoomData,
  getJoinRoomDetailsData: () => defaultUserData,
};

export const FormsContext = createContext<FormContextType>(defaultContext);
