import { useContext } from "react";
import { FormsContext } from "../../../contexts/forms-context/FormsContext";
import Stepper from "@components/common/stepper/Stepper";
import CreateRoomForm from "../create-room-form/CreateRoomForm";
import WishesForm from "@components/common/wishes-form/WishesForm";
import DetailsForm from "@components/common/details-form/DetailsForm";
import { CREATE_ROOM_STEP_NAMES } from "../utils";

const PageContent = () => {
  const { currentStep, onPreviousStep, createRoomData } =
    useContext(FormsContext);

  const createRoomSteps = [
    <CreateRoomForm key="create-room-form" />,
    <DetailsForm key="details-form" onBack={onPreviousStep} />,
    <WishesForm
      key="wishes-form"
      budget={createRoomData?.room.giftMaximumBudget}
      onBack={onPreviousStep}
    />,
  ];

  return (
    <>
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
