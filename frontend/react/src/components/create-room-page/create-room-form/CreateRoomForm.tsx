import { useState, type ChangeEvent, type KeyboardEvent } from "react";
import { DatePicker, type DatePickerProps } from "antd";

import FormWrapper from "../../common/form-wrapper/FormWrapper";
import Input from "../../common/input/Input";
import { InputNames, type FormData } from "./types";
import {
  LABEL_DATE_PICKER,
  INPUT_ID_DATE_PICKER,
  isValidInputField,
  requiredFields,
} from "./utils";
import {
  blockInvalidNumberKeys,
  isRequiredFieldsFilled,
} from "../../../utils/validation";

import styles from "../../common/input/Input.module.scss";
import "./CreateRoomForm.scss";

const CreateRoomForm = () => {
  const [formData, setFormData] = useState<FormData>({
    roomName: "",
    roomDescription: "",
    giftBudget: "",
    giftExchangeDate: null,
  });

  const handleChange = (
    e: ChangeEvent<HTMLInputElement | HTMLTextAreaElement>,
  ) => {
    const { name, value } = e.target;

    if (!isValidInputField(name, value)) return;

    setFormData((prev) => ({ ...prev, [name]: value }));
  };

  const handleDatePickerChange: DatePickerProps["onChange"] = (date) => {
    setFormData((prev) => ({ ...prev, giftExchangeDate: date }));
  };

  const handleBudgetKeyDown = (
    e: KeyboardEvent<HTMLInputElement | HTMLTextAreaElement>,
  ) => {
    blockInvalidNumberKeys(e);
  };

  const isFormValid = isRequiredFieldsFilled<FormData>(
    formData,
    requiredFields,
  );

  return (
    <FormWrapper
      formKey="CREATE_ROOM"
      iconName="star"
      buttonProps={{
        children: "Continue",
        disabled: !isFormValid,
      }}
    >
      <div className="content">
        <Input
          placeholder="Enter your room name"
          label="Room name"
          name={InputNames.ROOM_NAME}
          required
          value={formData.roomName}
          onChange={handleChange}
        />

        <Input
          placeholder="Enter your message here..."
          label="Room description"
          name={InputNames.ROOM_DESCRIPTION}
          multiline
          maxLength={200}
          required
          value={formData.roomDescription}
          onChange={handleChange}
        />

        <div className="content__group">
          <div className={styles.inputWrapper}>
            <label
              htmlFor={INPUT_ID_DATE_PICKER}
              className={`${styles["inputWrapper__label"]} ${styles["inputWrapper__label--required"]}`}
            >
              {LABEL_DATE_PICKER}
            </label>
            <DatePicker
              id={INPUT_ID_DATE_PICKER}
              format="DD-MM-YYYY"
              style={{
                width: "338px",
              }}
              onChange={handleDatePickerChange}
              value={formData.giftExchangeDate}
              className={formData.giftExchangeDate ? "ant-picker--filled" : ""}
            />
          </div>

          <Input
            type="number"
            placeholder="Type in your budget"
            label="Gift budget"
            name={InputNames.GIFT_BUDGET}
            width="338px"
            required
            withoutCounter
            withSuffix
            value={formData.giftBudget}
            onChange={handleChange}
            onKeyDown={handleBudgetKeyDown}
          />
        </div>
      </div>
    </FormWrapper>
  );
};

export default CreateRoomForm;
