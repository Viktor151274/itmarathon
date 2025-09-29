import { useState, type ReactNode } from "react";
import { defaultRoomData, FormsContext } from "./FormsContext";
import { removeIdFromArray } from "@utils/general";
import { type RoomData } from "./types";

export const FormsContextProvider = ({ children }: { children: ReactNode }) => {
  const [currentStep, setCurrentStep] = useState(0);
  const [roomData, setRoomData] = useState<RoomData>(defaultRoomData);

  const getCreateRoomData = () => {
    return {
      room: {
        ...roomData.room,
      },
      adminUser: {
        ...roomData.user,
        wishList: removeIdFromArray(roomData.user.wishList),
      },
    };
  };

  const getJoinRoomDetailsData = () => {
    return {
      ...roomData.user,
      wishList: removeIdFromArray(roomData.user.wishList),
    };
  };

  const onNextStep = () => {
    setCurrentStep((prevCurrentStep) => prevCurrentStep + 1);
  };

  const onPreviousStep = () => {
    setCurrentStep((prevCurrentStep) => prevCurrentStep - 1);
  };

  return (
    <FormsContext.Provider
      value={{
        currentStep,
        onNextStep,
        onPreviousStep,
        roomData,
        setRoomData,
        getCreateRoomData,
        getJoinRoomDetailsData,
      }}
    >
      {children}
    </FormsContext.Provider>
  );
};
