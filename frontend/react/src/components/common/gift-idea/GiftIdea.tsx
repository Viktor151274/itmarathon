import { useState, type ChangeEvent } from "react";
import Input from "../input/Input";
import type { GiftIdeaField, GiftIdeaProps } from "./types";
import "./GiftIdea.scss";

const initialGiftIdea = {
  wish: "",
  link: "",
};

const GiftIdea = ({ isWishRequired = false }: GiftIdeaProps) => {
  const [giftIdea, setGiftIdea] = useState<GiftIdeaField>(initialGiftIdea);

  const handleChange = (
    e: ChangeEvent<HTMLInputElement | HTMLTextAreaElement>,
  ) => {
    const { name, value } = e.target;

    setGiftIdea((prevGiftIdea) => ({
      ...prevGiftIdea,
      [name]: value,
    }));
  };

  return (
    <div className="gift-idea">
      <Input
        placeholder="Enter your wish name"
        value={giftIdea.wish}
        onChange={handleChange}
        label="I wish for"
        required={isWishRequired}
        width="302px"
        name="wish"
      />

      <Input
        placeholder="E.g. https://example.com/item"
        value={giftIdea.link}
        onChange={handleChange}
        label="Add link"
        width="302px"
        maxLength={undefined}
        withoutCounter
        name="link"
      />
    </div>
  );
};

export default GiftIdea;
