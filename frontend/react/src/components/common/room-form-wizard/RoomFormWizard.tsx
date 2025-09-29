import { useContext } from "react";
import { useLocation } from "react-router";

import { FormsContext } from "../../../contexts/forms-context/FormsContext";
import Stepper from "@components/common/stepper/Stepper";
import CreateRoomForm from "@components/create-room-page/create-room-form/CreateRoomForm";
import WishesForm from "@components/common/wishes-form/WishesForm";
import DetailsForm from "@components/common/details-form/DetailsForm";
import Loader from "@components/common/loader/Loader";

import { useFetch } from "@hooks/useFetch";

import { JOIN_ROOM_DETAILS_STEP_NAMES } from "@components/join-room-details-page/utils";
import { CREATE_ROOM_STEP_NAMES } from "@components/create-room-page/utils";
import type { RoomFormWizardProps } from "./types";

const RoomFormWizard = <TResponse, TRequest>({
  isAdmin = false,
  fetchConfig,
}: RoomFormWizardProps<TResponse>) => {
  const { currentStep, onPreviousStep, roomData, getCreateRoomData } =
    useContext(FormsContext);

  const location = useLocation();
  const state = location?.state as { giftBudget: number };
  const giftMaxBudget = state?.giftBudget;

  const { isLoading, fetchData } = useFetch<TResponse, TRequest>(
    fetchConfig,
    false,
  );

  const handleCreateRoom = () => {
    const formData = getCreateRoomData();

    if (!formData) return;

    const wantSurprise = formData?.adminUser?.wantSurprise;

    const preparedData = {
      ...formData,
      room: {
        ...formData.room,
        giftExchangeDate:
          formData?.room?.giftExchangeDate?.format("YYYY-MM-DD") ?? null,
        giftMaximumBudget: +formData?.room?.giftMaximumBudget,
      },
      adminUser: {
        ...formData.adminUser,
        phone: `+380${formData?.adminUser?.phone}`,
        interests: wantSurprise ? formData?.adminUser?.interests : undefined,
        wishList: !wantSurprise ? formData?.adminUser?.wishList : undefined,
      },
    } as unknown as TRequest;

    fetchData(preparedData);
  };

  const createRoomSteps = [
    <CreateRoomForm key="create-room-form" />,
    <DetailsForm key="details-form" onBack={onPreviousStep} />,
    <WishesForm
      key="wishes-form"
      budget={roomData?.room.giftMaximumBudget}
      onBack={onPreviousStep}
      onComplete={handleCreateRoom}
    />,
  ];

  const joinRoomDetailsSteps = [
    <DetailsForm key="details-form" />,
    <WishesForm
      key="wishes-form"
      budget={giftMaxBudget ? String(giftMaxBudget) : undefined}
      onBack={onPreviousStep}
      onComplete={() => {}}
    />,
  ];

  return (
    <>
      {isLoading ? <Loader /> : null}

      {isAdmin ? (
        <>
          <Stepper
            steps={CREATE_ROOM_STEP_NAMES}
            currentStepIndex={currentStep}
            width={626}
          />
          {createRoomSteps[currentStep]}
        </>
      ) : null}

      {!isAdmin ? (
        <>
          <Stepper
            steps={JOIN_ROOM_DETAILS_STEP_NAMES}
            currentStepIndex={currentStep}
            width={464}
          />
          {joinRoomDetailsSteps[currentStep]}
        </>
      ) : null}
    </>
  );
};

export default RoomFormWizard;
