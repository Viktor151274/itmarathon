import { useState, type ChangeEvent } from "react";

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

export const DetailsForm = ({ onBack }: DetailsFormProps) => {
  const [data, setData] = useState<FormData>({
    firstName: "",
    lastName: "",
    address: "",
    phone: "",
    email: "",
  });

  const isValidForm = isRequiredFieldsFilled<FormData>(data, requiredFields);

  const handleChange = (
    e: ChangeEvent<HTMLInputElement | HTMLTextAreaElement>,
  ) => {
    const { name, value } = e.target;
    setData((prev) => ({ ...prev, [name]: value }));
  };

  return (
    <FormWrapper
      iconName="car"
      formKey="ADD_DETAILS"
      buttonProps={{
        children: "Continue",
        disabled: !isValidForm,
      }}
      isBackButtonVisible
      onBack={onBack}
    >
      <div className="details-form-content">
        <Input
          placeholder="e.g. Nickolas"
          label="First name"
          value={data.firstName}
          required
          onChange={handleChange}
          width="338px"
          name={InputNames.FIRST_NAME}
        />

        <Input
          placeholder="e.g. Secret"
          label="Last name"
          value={data.lastName}
          required
          onChange={handleChange}
          width="338px"
          name={InputNames.LAST_NAME}
        />

        <PhoneNumberInput
          value={data.phone}
          required
          onChange={handleChange}
          name={InputNames.PHONE}
        />

        <Input
          placeholder="nickolas@example.com"
          label="Email"
          value={data.email}
          onChange={handleChange}
          width="338px"
          name={InputNames.EMAIL}
          withoutCounter
          type="email"
        />

        <Input
          placeholder="Where should St. Nick deliver your gift?"
          label="Your delivery address (no North Pole required!)"
          value={data.address}
          onChange={handleChange}
          multiline
          maxLength={500}
          required
          name={InputNames.ADDRESS}
        />
      </div>
    </FormWrapper>
  );
};
