export interface GiftIdeaProps {
  isWishRequired?: boolean;
  giftItem: GiftIdeaField;
  onChange: (field: keyof GiftIdeaField, value: string) => void;
}

export interface GiftIdeaField {
  wish: string;
  link: string;
}
