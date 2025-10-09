export type WishList = {
  name?: string;
  infoLink?: string;
};

export type Participant = {
  id: number;
  createdOn: string;
  modifiedOn: string;
  roomId: number;
  isAdmin: boolean;
  userCode: string;
  firstName: string;
  lastName: string;
  phone: string;
  email?: string;
  giftToUserId?: number;
  deliveryInfo: string;
  wantSurprise: boolean;
  interests?: string;
  wishList?: WishList[];
};
