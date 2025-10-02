import { StepLabel } from './app.enum';

import type {
  CreateRoomSuccessPageData,
  JoinRoomWelcomePageData,
} from './app.models';

export const ICONS_SPRITE_PATH = '/assets/icons/icons-sprite.svg';

export const IMAGES_SPRITE_PATH = '/assets/images/images-sprite.svg';

export const PRIVACY_POLICY_PATH = '/assets/pdfs/privacy-policy.pdf';

export const PRIVACY_NOTICE_PATH = '/assets/pdfs/privacy-notice.pdf';

export const MESSAGE_DURATION_MS = 3000;

export const JOIN_ROOM_STEPPER_LABELS: StepLabel[] = [
  StepLabel.AddPersonalInfo,
  StepLabel.AddWishlist,
];

export const CREATE_ROOM_STEPPER_LABELS: StepLabel[] = [
  StepLabel.CreateRoom,
  ...JOIN_ROOM_STEPPER_LABELS,
];

export const SUCCESS_PAGE_DATA_DEFAULT: CreateRoomSuccessPageData = {
  userCode: '',
  invitationCode: '',
  invitationNote: '',
};

export const JOIN_ROOM_DATA_DEFAULT: JoinRoomWelcomePageData = {
  giftMaximumBudget: 0,
  invitationCode: '',
  giftExchangeDate: '',
};

export const PHONE_CODE_UA = '+380';
