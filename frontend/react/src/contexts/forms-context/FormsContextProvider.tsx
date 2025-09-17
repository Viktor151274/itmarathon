import { useState, type ReactNode } from "react";
import { type CreateRoomData } from "./types";
import { defaultCreateRoomData, FormsContext } from "./FormsContext";

export const FormsContextProvider = ({ children }: { children: ReactNode }) => {
  const [currentStep, setCurrentStep] = useState(0);
  const [createRoomData, setCreateRoomData] = useState<CreateRoomData>(
    defaultCreateRoomData,
  );

  const getCreateRoomData = () => {
    return {
      ...createRoomData,
      adminUser: {
        ...createRoomData.adminUser,

        wishList: createRoomData.adminUser.wishList.map(({ id, ...rest }) => {
          void id;
          return rest;
        }),
      },
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
        createRoomData,
        setCreateRoomData,
        getCreateRoomData,
      }}
    >
      {children}
    </FormsContext.Provider>
  );
};
