import { useState } from "react";
import Button from "../button/Button";
import FormWrapper from "../form-wrapper/FormWrapper";
import GiftIdea from "../gift-idea/GiftIdea";
import Input from "../input/Input";

import type { GiftIdeaField } from "../gift-idea/types";
import { RadioButtonItem } from "../radio-button-item/RadioButtonItem";
import "./WishesForm.scss";
import { checkValidation } from "./utils";
import type { InputChangeEvent } from "../../../types/general";

import {
  GiftTypeValue,
  type GiftIdeaType,
  type GiftType,
  type WishesFormProps,
} from "./types";

const initialValues = {
  initialIdeas: [{ id: Date.now(), wish: "", link: "" }],
};

const WishesForm = ({ budget }: WishesFormProps) => {
  const [ideas, setIdeas] = useState<GiftIdeaType[]>(
    initialValues.initialIdeas,
  );
  const [giftType, setGiftType] = useState<GiftType>(GiftTypeValue.IDEAS);
  const [surpriseText, setSurpriseText] = useState("");

  const isFormValid = checkValidation(giftType, surpriseText, ideas);

  const handleChangeValue = (
    id: number,
    field: keyof GiftIdeaField,
    value: string,
  ) => {
    setIdeas((prev) =>
      prev.map((idea) => (idea.id === id ? { ...idea, [field]: value } : idea)),
    );
  };

  const handleFormAddWish = () => {
    const id = Date.now();
    setIdeas((prev) => [...prev, { id, wish: "", link: "" }]);
  };

  return (
    <FormWrapper
      formKey="ADD_WISHES"
      iconName="presents"
      subDescription={budget && `Gift Budget: ${budget} UAH`}
      buttonProps={{
        children: "Complete",
        disabled: !isFormValid,
      }}
      isBackButtonVisible
    >
      <div className="wishes-form-content">
        <RadioButtonItem
          name="giftType"
          text="I have gift ideas! (add up to 5 gift ideas)"
          selected={giftType === GiftTypeValue.IDEAS}
          onChange={() => setGiftType(GiftTypeValue.IDEAS)}
        >
          {giftType === GiftTypeValue.IDEAS ? (
            <div className="wishes-form-content__gifts">
              {ideas.map((idea, index) => (
                <GiftIdea
                  key={idea.id}
                  isWishRequired={index === 0}
                  giftItem={idea}
                  onChange={(field, value) =>
                    handleChangeValue(idea.id, field, value)
                  }
                />
              ))}

              {ideas.length < 5 ? (
                <div className="wishes-form-content__button-place">
                  <Button
                    type="button"
                    variant="tertiary"
                    size="small"
                    iconName="plus-outlined"
                    onClick={handleFormAddWish}
                    width={154}
                  >
                    Add Wish
                  </Button>
                </div>
              ) : null}
            </div>
          ) : null}
        </RadioButtonItem>

        <RadioButtonItem
          name="giftType"
          text="I want a surprise gift"
          selected={giftType === GiftTypeValue.SURPRISE}
          onChange={() => setGiftType(GiftTypeValue.SURPRISE)}
        >
          {giftType === GiftTypeValue.SURPRISE ? (
            <div className="wishes-form-content__surprise">
              <Input
                value={surpriseText}
                onChange={(e: InputChangeEvent) =>
                  setSurpriseText(e.target.value)
                }
                placeholder="e.g. reading, coffee, cozy socks"
                label="Add your interests"
                maxLength={1000}
                required
                multiline
              />
            </div>
          ) : null}
        </RadioButtonItem>
      </div>
    </FormWrapper>
  );
};

export default WishesForm;
