import { useContext, type ChangeEvent } from "react";

import PhoneNumberInput from "../phone-number-input/PhoneNumberInput";
import FormWrapper from "../form-wrapper/FormWrapper";
import Input from "../input/Input";

import {
  InputNames,
  type FormData,
  type DetailsFormProps,
  requiredFields,
} from "./types";
import { isRequiredFieldsFilled } from "../../../../src/utils/validation";

import "./DetailsForm.scss";
import { FormsContext } from "../../../contexts/forms-context/FormsContext";

const DetailsForm = ({ onBack }: DetailsFormProps) => {
  const { onNextStep, createRoomData, setCreateRoomData } =
    useContext(FormsContext);
  const { firstName, lastName, phone, email, deliveryInfo } =
    createRoomData.adminUser;

  const isValidForm = isRequiredFieldsFilled<FormData>(
    createRoomData?.adminUser,
    requiredFields,
  );

  const handleChange = (
    e: ChangeEvent<HTMLInputElement | HTMLTextAreaElement>,
  ) => {
    const { name, value } = e.target;

    setCreateRoomData((prev) => ({
      ...prev,
      adminUser: {
        ...prev.adminUser,
        [name]: value,
      },
    }));
  };

  return (
    <FormWrapper
      iconName="car"
      formKey="ADD_DETAILS"
      buttonProps={{
        children: "Continue",
        disabled: !isValidForm,
        onClick: onNextStep,
      }}
      isBackButtonVisible
      onBack={onBack}
    >
      <div className="details-form-content">
        <Input
          placeholder="e.g. Nickolas"
          label="First name"
          value={firstName}
          required
          onChange={handleChange}
          width="338px"
          name={InputNames.FIRST_NAME}
        />

        <Input
          placeholder="e.g. Secret"
          label="Last name"
          value={lastName}
          required
          onChange={handleChange}
          width="338px"
          name={InputNames.LAST_NAME}
        />

        <PhoneNumberInput
          value={phone}
          required
          onChange={handleChange}
          name={InputNames.PHONE}
        />

        <Input
          placeholder="nickolas@example.com"
          label="Email"
          value={email}
          onChange={handleChange}
          width="338px"
          name={InputNames.EMAIL}
          withoutCounter
          type="email"
        />

        <Input
          placeholder="Where should St. Nick deliver your gift?"
          label="Your delivery address (no North Pole required!)"
          value={deliveryInfo}
          onChange={handleChange}
          multiline
          maxLength={500}
          required
          name={InputNames.DELIVERY_INFO}
        />
      </div>
    </FormWrapper>
  );
};

export default DetailsForm;
