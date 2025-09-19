import { useContext } from "react";
import { useNavigate } from "react-router";

import {
  FormsContext,
  defaultCreateRoomData,
} from "../../../contexts/forms-context/FormsContext";
import Stepper from "@components/common/stepper/Stepper";
import CreateRoomForm from "../create-room-form/CreateRoomForm";
import WishesForm from "@components/common/wishes-form/WishesForm";
import DetailsForm from "@components/common/details-form/DetailsForm";
import Loader from "@components/common/loader/Loader";

import { CREATE_ROOM_STEP_NAMES } from "../utils";

import { useFetch } from "@hooks/useFetch";
import useToaster from "@hooks/useToaster";

import { BASE_API_URL } from "@utils/general";
import { type CreateRoomResponse, type CreateRoomRequest } from "../types";

const PageContent = () => {
  const {
    currentStep,
    onPreviousStep,
    createRoomData,
    setCreateRoomData,
    getCreateRoomData,
  } = useContext(FormsContext);

  const navigate = useNavigate();
  const { showToast } = useToaster();

  const { isLoading, fetchData } = useFetch<
    CreateRoomResponse,
    CreateRoomRequest
  >(
    {
      url: `${BASE_API_URL}/api/rooms`,
      method: "POST",
      headers: { "Content-Type": "application/json" },
      onError: () => {
        showToast("Something went wrong. Try again.", "error", "large");
      },
      onSuccess: (response: CreateRoomResponse) => {
        setCreateRoomData(defaultCreateRoomData);
        navigate("/create-room/success", {
          state: {
            roomAndUserData: {
              invitationCode: response?.room?.invitationCode,
              invitationNote: response?.room?.invitationNote,
              userCode: response?.userCode,
            },
          },
        });
      },
    },
    false,
  );

  const handleComplete = () => {
    const formData = getCreateRoomData();

    if (!formData) return;

    const wantSurprise = formData?.adminUser?.wantSurprise;

    const preparedData: CreateRoomRequest = {
      ...formData,
      room: {
        ...formData.room,
        giftExchangeDate:
          formData?.room?.giftExchangeDate?.format("YYYY-MM-DD") ?? null,
        giftMaximumBudget: +formData?.room?.giftMaximumBudget,
      },
      adminUser: {
        ...formData.adminUser,
        interests: wantSurprise ? formData?.adminUser?.interests : undefined,
        wishList: !wantSurprise ? formData?.adminUser?.wishList : undefined,
      },
    };

    fetchData(preparedData);
  };

  const createRoomSteps = [
    <CreateRoomForm key="create-room-form" />,
    <DetailsForm key="details-form" onBack={onPreviousStep} />,
    <WishesForm
      key="wishes-form"
      budget={createRoomData?.room.giftMaximumBudget}
      onBack={onPreviousStep}
      onComplete={handleComplete}
    />,
  ];

  return (
    <>
      {isLoading ? <Loader /> : null}

      <Stepper
        steps={CREATE_ROOM_STEP_NAMES}
        currentStepIndex={currentStep}
        width={626}
      />
      {createRoomSteps[currentStep]}
    </>
  );
};

export default PageContent;
