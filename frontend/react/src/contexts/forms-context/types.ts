import { type Dispatch, type SetStateAction } from "react";
import type { WishesFormData } from "@components/common/wishes-form/types";
import type { FormData as RoomData } from "@components/create-room-page/create-room-form/types";
import type { FormData as DetailsData } from "@components/common/details-form/types";

type AdminUserData = DetailsData & WishesFormData;

export interface CreateRoomData {
  room: RoomData;
  adminUser: AdminUserData;
}

export type CreateRoomSubmitData = {
  room: CreateRoomData["room"];
  adminUser: Omit<CreateRoomData["adminUser"], "wishList"> & {
    wishList: Array<
      Omit<CreateRoomData["adminUser"]["wishList"][number], "id">
    >;
  };
};

export type FormContextType = {
  currentStep: number;
  onNextStep: () => void;
  onPreviousStep: () => void;
  createRoomData: CreateRoomData;
  setCreateRoomData: Dispatch<SetStateAction<CreateRoomData>>;
  getCreateRoomData: () => CreateRoomSubmitData;
};
