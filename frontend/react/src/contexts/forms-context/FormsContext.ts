import { createContext } from "react";
import type { FormContextType } from "./types";

export const defaultCreateRoomData = {
  room: {
    name: "",
    description: "",
    giftExchangeDate: null,
    giftMaximumBudget: "",
  },
  adminUser: {
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
  },
};

export const defaultContext = {
  currentStep: 0,
  onNextStep: () => {},
  onPreviousStep: () => {},
  createRoomData: defaultCreateRoomData,
  setCreateRoomData: () => {},
  getCreateRoomData: () => defaultCreateRoomData,
};

export const FormsContext = createContext<FormContextType>(defaultContext);
