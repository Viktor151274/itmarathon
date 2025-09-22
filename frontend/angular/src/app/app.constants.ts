import { StepLabel } from './app.enum';

import type { SuccessPageData } from './app.models';

export const ICONS_SPRITE_PATH = '/assets/icons/icons-sprite.svg';

export const IMAGES_SPRITE_PATH = '/assets/images/images-sprite.svg';

export const PRIVACY_POLICY_PATH = '/assets/pdfs/privacy-policy.pdf';

export const PRIVACY_NOTICE_PATH = '/assets/pdfs/privacy-notice.pdf';

export const MESSAGE_DURATION_MS = 3000;

export const CREATE_ROOM_STEPPER_LABELS: StepLabel[] = [
  StepLabel.CreateRoom,
  StepLabel.AddParticipantInfo,
  StepLabel.AddWishlist,
];

export const SUCCESS_PAGE_DATA_DEFAULT: SuccessPageData = {
  userCode: '',
  invitationCode: '',
  invitationNote: '',
};
